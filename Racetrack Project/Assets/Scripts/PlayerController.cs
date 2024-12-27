using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const String Horizontal = "Horizontal";
    private const String Vertical = "Vertical";
    
    private float _horizontalInput;
    private float _verticalInput;
    private float _currentSteerAngle;
    private float _currentBrakeForce;
    private bool _isBraking;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;
    
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis(Horizontal);
        _verticalInput = Input.GetAxis(Vertical);
        _isBraking = Input.GetKey(KeyCode.Space);
    }
    
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = _verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = _verticalInput * motorForce;
        brakeForce = _isBraking ? brakeForce : 0f;

        if (_isBraking) ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = _currentBrakeForce;
        frontRightWheelCollider.brakeTorque = _currentBrakeForce;
        backLeftWheelCollider.brakeTorque = _currentBrakeForce;
        backRightWheelCollider.brakeTorque = _currentBrakeForce;
    }

    private void HandleSteering()
    {
        _currentSteerAngle = maxSteerAngle * _horizontalInput;
        frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
