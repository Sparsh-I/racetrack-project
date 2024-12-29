using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;
    
    public Vector3 moveOffset;
    public Vector3 rotOffset;
    
    public Transform target;
    
    public float minHeightAboveTarget = 2f;

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }
    
    private void HandleMovement()
    {
        Vector3 targetPos = target.TransformPoint(moveOffset);
        targetPos.y = Mathf.Max(targetPos.y, target.position.y + minHeightAboveTarget);
        
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}
