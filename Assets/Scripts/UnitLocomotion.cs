using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLocomotion : MonoBehaviour
{
    //Animation, articulation
    //Receives final steer force vector decision from UnitSteering
    //Receives final attack signal from UnitFighting
    //Interfacing with physics system, applies forces/velocities as necessary
    //Triggering animation system for cosmetic purposes

    private Vector3 _steerForce = default;
    private Rigidbody _rb;

    public void SetSteerForce(Vector3 steerForce)
    {
        _steerForce = steerForce;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_steerForce);
    }
}
