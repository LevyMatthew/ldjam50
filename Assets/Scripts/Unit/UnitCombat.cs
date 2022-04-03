using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    public ConfigurableJoint handA;
    public ConfigurableJoint handB;

    public void StartFight(Unit target)
    {
        print("Start Fighting");
        print(target);
        //handA.AddTorque(Vector3.right * 100f);
        //handB.AddTorque(Vector3.right * 100f);
    }

    public void StopFight(Unit target)
    {
        print("Stop Fighting");
        print(target);
       // handA.connectedBody.AddTorque(Vector3.right * -100f);
       // handB.connectedBody.AddTorque(Vector3.right * -100f);
    }
    
    //Similar to UnitSteering and/or UnitLocomotion
    //but for swinging and triggering held weapons
    //Which path does the agent intend for weapons/shields to follow (Steering)?
    // Decision to swing equipment or spawn spell comes from UnitBehaviourSelection        
    //Calculate local drive torque and force for the weapon, apply to ConfigurableJoint
}
