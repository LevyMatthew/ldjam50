using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	float radius = 12.0f;
	float explosionForce = 50.0f;
    float startTime;
    float duration = 1.0f;
    float diff;
    float currTime;

    void Start()
    {
        startTime = Time.time;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20.0f);
        Rigidbody rb;
        Vector3 diff;
        foreach (Collider c in hitColliders)
        {
    		rb = c.GetComponent<Rigidbody>();
            if(rb){
        		Vector3 unitPos = c.transform.position;
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

    void Update(){
        currTime = Time.time;
        diff = currTime - startTime;
        if(diff >= duration){
            Destroy(gameObject);
        }
    }
}
