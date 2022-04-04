using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
	float height = 0.0f;
	float offset;
	float y;

	void Start(){
		offset = Random.Range(0.0f, 10.0f);
	}

    // Update is called once per frame
    void Update()
    {
    	height = 0.4f * Mathf.Abs(Mathf.Sin(Time.time * 5.0f + offset));
        transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
    }
}
