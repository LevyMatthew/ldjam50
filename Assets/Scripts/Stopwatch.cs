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

	void Start(){
		EventManager.TransitionEvent += OnTransition;
	}

	private void OnTransition(int t){
		//play game, start timer
		if(t == 2){
			Begin();
		}
		//game over, stop timer
		else if(t == 3){
			Stop();
		}
	}

	private void Begin(){
		running = true;
		startTime = Time.time;
	}

	private void Stop(){
		running = false;
		finalTime = GetTime();
		DisplayTime(finalTime);
	}

	private float GetTime(){
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

	private void OnDisable(){
		EventManager.TransitionEvent -= OnTransition;
	}
}
