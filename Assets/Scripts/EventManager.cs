using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class EventManager : MonoBehaviour
{
	public static event Action MuteEvent; //toggling the mute button
	public static event Action<int> DifficultyEvent; //change difficulty
	public static event Action<int> TransitionEvent; //transitioning to a different menu or gamestate

	public static void StartMuteEvent(){
		MuteEvent?.Invoke();
	}

	public static void StartDifficultyEvent(int difficulty){
		DifficultyEvent?.Invoke(difficulty);
	}

    //0 - main menu
    //1 - difficulty menu
    //2 - playing game
    //3 - game over
	public static void StartTransitionEvent(int target){
		TransitionEvent?.Invoke(target);
	}
}
