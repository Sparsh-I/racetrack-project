using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    public float maxAcceleration = 30f;
    public float brakeAcceleration = 50f;
    
    public float turnSensitivity = 1f;
    public float maxSteerAngle = 30f;

    public Vector3 centreOfMass;
    
    public List<Wheel> wheels;

    private float _moveInput;
    private float _steerInput;

    private Rigidbody _carRb;

    private void Start()
    {
        _carRb = GetComponent<Rigidbody>();
        _carRb.centerOfMass = centreOfMass;
    }

    private void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    private void GetInputs()
    {
        _moveInput = Input.GetAxis("Vertical");
        _steerInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = _moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    private void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var steerAngle = _steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, steerAngle, 0.6f);
            }
        }
    }

    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}
