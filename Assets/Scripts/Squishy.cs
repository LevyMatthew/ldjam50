using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squishy : MonoBehaviour
{
	int health = 10;
	int heat = 25;

    void TakeDamage(Weapon weapon){
    	health -= weapon.damage;
    	heat += weapon.heat;
    	print(health);
    	print(heat);
    }

    void OnCollisionEnter(Collision collision)
    {
    	Weapon weapon = collision.gameObject.GetComponent<Weapon>();
    	if(weapon){
	        if (collision.relativeVelocity.sqrMagnitude > 4){
	            TakeDamage(weapon);
	        }
	    }
    }
}
