using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSteering : MonoBehaviour
{
    //Path determination
    //Calculate a force direction (steering direction)
    //Literature: Steering Behaviors For Autonomous Characters, Craig W. Reynolds, 1999
    
    public float maxSteerForce = 0.1f;

    public float maxSteerVelocity
    {
        get
        {
            return GetRunSpeed();
        }
    }

    public Vector3 _desiredVelocity = default;
    public Vector3 _desiredAngularVelocity = default;
    private Rigidbody _rb;
    private Unit _unit;

    [Header("Seek Target Steering Behaviour")]
    public Transform _seekTarget;

    [Header("Align With Target Steering Behaviour")]
    public Vector3 _desiredLookDirection;
    public Vector3 _desiredRunDirection;
    public Vector3 _constantDrivingTorque;

    public Vector3 SteeringForce()
    {
        //Steer linear motion
        return _desiredVelocity - GetCurrentVelocity();
    }
    public Vector3 SteeringTorque()
    { 
        //Steer angular motion, turning
        return _desiredAngularVelocity - GetCurrentAngularVelocity();
    }

    public void SetSeekTarget(Transform target)
    {
        _seekTarget = target;
    }

    public void SetRunDirection(Vector3 displacement)
    {
        Vector3 direction = displacement.normalized;
        _desiredLookDirection = direction;
        _desiredRunDirection = direction;
    }

    public void SetConstantTorque(Vector3 torque)
    {
        _constantDrivingTorque = torque;
    }


    private Vector3 SeekSteeringForce()
    {
        Vector3 desiredDisplacement = Vector3.zero;
        if (_seekTarget)
            desiredDisplacement = _seekTarget.position - GetCurrentGlobalPosition();            
        if (desiredDisplacement.sqrMagnitude < .5f)
        {
            print("Steering force zero");
            return Vector3.zero ;
        }
        return desiredDisplacement.normalized * GetWalkSpeed();
    }

    private Vector3 RunSteeringForce()
    {
        return _desiredRunDirection * GetRunSpeed();
    }


    private Vector3 TurnSteeringTorque()
    {
        return _constantDrivingTorque;
        //return Quaternion.FromToRotation(transform.forward, _desiredLookDirection).eulerAngles;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _unit = GetComponent<Unit>();
    }

    private void FixedUpdate()
    {
        Vector3 seekForce = SeekSteeringForce();
        //TODO: Not sure if forward is right here, or if this even belongs in this class. Behaviour?
        Vector3 runForwardForce = RunSteeringForce();
        _desiredVelocity = seekForce + runForwardForce;
        _desiredAngularVelocity = TurnSteeringTorque();
        _unit.locomotion.SetSteerForce(SteeringForce());
        _unit.locomotion.SetSteerTorque(SteeringTorque());
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

    private float GetRunSpeed()
    {
        return _unit.stats.runSpeed;
    }

    private float GetWalkSpeed()
    {
        return _unit.stats.walkSpeed;
    }


}
