using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalry : MonoBehaviour
{

	Rigidbody rb;
	float runSpeed = 10.0f;
	float startTime;
	float duration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody>();
    	startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    	if(Time.time - startTime > duration){
    		Destroy(gameObject);
    	}
    	rb.velocity = runSpeed * -transform.forward;
    }
}
