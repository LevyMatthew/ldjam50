using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	float radius = 12.0f;
	float explosionForce = 50.0f;
	public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20.0f);
        Unit u;
        Rigidbody rb;
        Vector3 diff;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        foreach (Collider c in hitColliders)
        {
        	u = c.GetComponent<Unit>();
        	if(u && u.team == UnitTeam.ComputerTeam){
        		rb = u.GetComponent<Rigidbody>();
        		Vector3 unitPos = u.transform.position;
        		Vector3 expPos = transform.position;
        		diff = unitPos - expPos;
        		if(diff.magnitude <= radius){
	        		diff = Vector3.Normalize(diff);
	        		Vector3 expForce = new Vector3(diff.x, 0.5f, diff.z) * explosionForce;
	            	rb.AddForce(expForce, ForceMode.Impulse);
	            }
            }
        }
    }
}
