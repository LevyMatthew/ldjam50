using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeter : MonoBehaviour
{
	public float x; //fraction of how far along we are. L = Ax + B(1-x) where L is current, A is start, B is end

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Set x value based on motion rule
        //Camera pivot stays still, set transform.rotation = slerp()
    }


}
