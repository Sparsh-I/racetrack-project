using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;
    private GameObject _child;
    [SerializeField] private float speed;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _child = _player.transform.Find("CameraConstraint").gameObject;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, _child.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(_player.gameObject.transform);
    }
}
