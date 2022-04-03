using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSignals : MonoBehaviour
{

	bool isMuted = false;

	public void Reset(){
		print("Reset");
	}

	public void ToggleMute(){
		print("ToggleMute");
		isMuted = !isMuted;
		Camera.main.GetComponent<AudioSource>().mute = isMuted;
	}

	public void SetDifficulty(int difficulty){
		print("SetDifficulty" + difficulty);
	}
}
