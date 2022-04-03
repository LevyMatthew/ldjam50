using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSteering : MonoBehaviour
{
    //Path determination
    //Calculate a force direction (steering direction)
    //Literature: Steering Behaviors For Autonomous Characters, Craig W. Reynolds, 1999
    public float maxSteerForce = 0.1f;    
        
    private Vector3 _desiredVelocity = default;
    private Vector3 _desiredAngularVelocity = default;
    private Rigidbody _rb;
    private UnitStats _unitStats;
    private UnitLocomotion _unitLocomotion;
    public Transform _seekTarget;

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
            Vector3 direction = _seekTarget.position - GetCurrentGlobalPosition();
            Vector3 desired = direction.normalized * GetMaxSpeed();
            return desired;
        }
        return Vector3.zero;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _unitStats = GetComponent<UnitStats>();
        _unitLocomotion = GetComponent<UnitLocomotion>();      
    }

    private void Update()
    {
        _desiredVelocity = SeekSteeringForce();
        _desiredAngularVelocity = Vector3.zero;
        _unitLocomotion.SetSteerForce(SteeringForce());
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
    private float GetMaxSpeed()
    {
        return _unitStats.GetStat(UnitStats.RunSpeedId);
    }


}
