using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsChecker : MonoBehaviour
{
    void OnTriggerExit(Collider collider)
    {
    	print(collider);
    	collider.enabled = false;
        // Unit unit = collider.GetComponent<Unit>();
        // if(unit){
        // 	print("unit has left the play area");
        // 	unit.GetComponent<CapsuleCollider>().enabled = false;
        // }
    }
}
