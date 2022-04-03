using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSignals : MonoBehaviour
{

	public void Reset(){
		print("Reset");
	}

	public void SetMute(bool isMuted){
		print("SetMute" + isMuted);
	}

	public void SetDifficulty(int difficulty){
		print("SetDifficulty" + difficulty);
	}
}
