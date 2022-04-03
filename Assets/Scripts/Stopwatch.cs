using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
	float startTime;
	float finalTime;
	bool running = false;
	public TextMeshProUGUI label;

	public void Begin(){
		running = true;
		startTime = Time.time;
	}

	public void Stop(){
		running = false;
		finalTime = GetTime();
		DisplayTime(finalTime);
	}

	public float GetTime(){
		return Time.time - startTime;
	}

	private void DisplayTime(float time){
		TimeSpan timeSpan = TimeSpan.FromSeconds(time);
 		string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
 		label.SetText(timeText);
	}

	void Update(){
		if(running){
			DisplayTime(GetTime());
	 	}
	}
}
