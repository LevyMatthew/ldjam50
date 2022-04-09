using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPull : MonoBehaviour
{
	public float force = 1f;

	private void NudgeUnit(Unit unit){
		print("Door");
		unit.GetComponent<Rigidbody>().AddForce((transform.position - unit.transform.position).normalized * force, ForceMode.Impulse);
	}

    void OnTriggerEnter(Collider c)
    {
    	Unit unit = c.gameObject.GetComponent<Unit>();
    	if(unit){
            NudgeUnit(unit);
	    }
    }
}
