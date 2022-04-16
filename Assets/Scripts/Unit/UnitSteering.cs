using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSteering : MonoBehaviour
{
    //Path determination
    //Calculate a force direction (steering direction)
    //Literature: Steering Behaviors For Autonomous Characters, Craig W. Reynolds, 1999
   
    public Vector3 _desiredVelocity;
    private Rigidbody _rb;
    private Unit _unit;

    public Vector3 _desiredLookDirection;
    public Vector3 _desiredRunDirection;

    public void SetRunDirection(Vector3 displacement)
    {
        Vector3 direction = displacement.normalized;
        _desiredLookDirection = direction;
        _desiredRunDirection = direction;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _unit = GetComponent<Unit>();
    }

    private void FixedUpdate()
    {
        Vector3 force = _desiredRunDirection * _unit.stats.runSpeed;
        if(GetCurrentVelocity().sqrMagnitude >= _unit.stats.maxSpeed * _unit.stats.maxSpeed)
        {
            force = Vector3.zero;
        }
        _unit.locomotion.SetSteerForce(force);
    }

    private Vector3 GetCurrentVelocity()
    {
        return _rb.velocity;
    }

    private Vector3 GetCurrentAngularVelocity()
    {
        return _rb.angularVelocity;
    }

    private Vector3 GetCurrentGlobalPosition()
    {
        return _rb.position;
    }
}
