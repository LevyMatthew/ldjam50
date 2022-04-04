using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{

	public GameSignals gs;

    void OnCollisionEnter(Collision collision)
    {
    	Unit unit = collision.gameObject.GetComponent<Unit>();
    	if(unit){
            gs.GameOver();
	    }
    }
}
