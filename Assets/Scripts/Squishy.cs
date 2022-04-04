using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squishy : MonoBehaviour
{
	int heat = 25;
    public Unit unit;
    public UnitStatsFiller usf;

    void TakeDamage(Weapon weapon){
        if(unit.team == UnitTeam.Player){
            //launch this squishy towards castle
        }
        else{
            
        }
    	unit.health -= weapon.damage;
    	heat += weapon.heat;
        usf.UpdateStats(unit);
    	print("I am taking damage!!!");

    }

    void OnCollisionEnter(Collision collision)
    {
    	Weapon weapon = collision.gameObject.GetComponent<Weapon>();
    	if(weapon){
            TakeDamage(weapon);
	    }
    }
}
