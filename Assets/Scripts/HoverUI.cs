using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUI : MonoBehaviour
{
	public RectTransform rt;
	public UnitStatsFiller usf;
	public Unit unit;

	private Vector3 initScale;
	private float baseScaleDistance = 100; //at a distance of 100, scale is 1. at 50, scale is 1/2

    // Start is called before the first frame update
    public void Start()
    {
    	if(usf)
    		usf.UpdateStats(unit);
    	initScale = rt.localScale;
    }

    public void MoveUI(){
		float dist = Vector3.Distance(Camera.main.transform.position, unit.transform.position);
		float scaleFactor = baseScaleDistance/dist;
		rt.localScale = initScale * scaleFactor;

		Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position + new Vector3(0, 10f, 0));
		rt.position = screenPos;
    }

    // Update is called once per frame
    public void Update()
    {
        MoveUI();
    }
}
