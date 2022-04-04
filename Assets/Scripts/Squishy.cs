using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squishy : MonoBehaviour
{
	int heat = 25;
    public Unit unit;
    public UnitStatsFiller usf;

    void TakeDamage(Weapon weapon){
    	unit.health -= weapon.damage;
    	heat += weapon.heat;
        usf.UpdateStats(unit);
    }

    void OnCollisionEnter(Collision collision)
    {
    	Weapon weapon = collision.gameObject.GetComponent<Weapon>();
    	if(weapon){
            TakeDamage(weapon);
	    }
    }
}
