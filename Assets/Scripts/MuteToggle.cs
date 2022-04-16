using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{

	private bool isMuted = false;

	void Start(){
		EventManager.MuteEvent += Toggle;
	}

	private void Toggle(){
		isMuted = !isMuted;
		Camera.main.GetComponent<AudioSource>().mute = isMuted;
	}

	private void OnDisable(){
		EventManager.MuteEvent -= Toggle;
	}
}
