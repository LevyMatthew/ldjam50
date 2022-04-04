using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{

	Rigidbody rb;
	float startTime;
	float duration = 30.0f;

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
    }
}
