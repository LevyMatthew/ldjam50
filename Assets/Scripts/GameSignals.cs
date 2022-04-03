using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSignals : MonoBehaviour
{
	int difficulty = 0;

	public Stopwatch stopwatch;
	public MuteToggle muteToggle;

	public void Reset(){
		print("Reset");
		stopwatch.Begin();
	}

	public void GameOver(){
		print("Game Over");
		stopwatch.Stop();
	}

	public void ToggleMute(){
		print("ToggleMute");
		muteToggle.Toggle();
	}

	public void SetDifficulty(int d){
		print("SetDifficulty" + d);
		difficulty = d;
	}
}
