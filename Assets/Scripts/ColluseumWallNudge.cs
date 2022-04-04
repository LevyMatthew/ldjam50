using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColluseumWallNudge : MonoBehaviour
{
	public float force;
    List<Unit> collidingUnits;

    void Start(){
        collidingUnits = new List<Unit>();
    }

	private void NudgeUnit(Unit unit){
		unit.GetComponent<Rigidbody>().AddForce(-transform.right * force, ForceMode.Force);
	}

    void OnCollisionEnter(Collision collision)
    {
    	Unit unit = collision.gameObject.GetComponent<Unit>();
    	if(unit){
            collidingUnits.Add(unit);
	    }
    }

    void OnCollisionExit(Collision collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if(unit){
            collidingUnits.Remove(unit);
        }
    }

    void Update(){
        foreach(Unit u in collidingUnits){
            NudgeUnit(u);
        }
    }
}
