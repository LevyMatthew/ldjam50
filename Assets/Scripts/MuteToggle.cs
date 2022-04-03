using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggle : MonoBehaviour
{

	bool isMuted = false;

	public void Toggle(){
		isMuted = !isMuted;
		Camera.main.GetComponent<AudioSource>().mute = isMuted;
	}
}
