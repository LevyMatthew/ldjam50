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
            return GetSteerForce();
        }
    }

    public Vector3 _desiredVelocity = default;
    public Vector3 _desiredAngularVelocity = default;
    public Rigidbody _rb;
    public Unit _unit;

    [Header("Seek Target Steering Behaviour")]
    public Transform _seekTarget;

    [Header("Align With Target Steering Behaviour")]
    public Vector3 _lookDirection;

    private Vector3 _moveDirection;

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

    private Vector3 SeekSteeringForce()
    {
        if (_seekTarget){
            Vector3 desiredDisplacement = _seekTarget.position - GetCurrentGlobalPosition();
            Vector3 seekSteering = desiredDisplacement.normalized * GetSteerForce();
        }
        return Vector3.zero;
    }

    private Vector3 RunSteeringForce(Vector3 desiredDirection)
    {
        return desiredDirection * GetSteerForce();
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
        Vector3 runForwardForce = RunSteeringForce(Vector3.forward);
        _desiredVelocity = seekForce + runForwardForce;
        _desiredAngularVelocity = Vector3.zero;
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

    private float GetSteerForce()
    {
        return _unit.stats.GetStat(UnitStats.RunSpeedId);
    }


}
