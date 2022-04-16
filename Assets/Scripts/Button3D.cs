using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]
public class Button3D : MonoBehaviour
{
	public float hoverScale = 1.1f; //scale multiplier when hovering over button
	public float clickScale = 1.2f; //scale multiplier when clicking on button
	public int destination; //cameraID to transition to on click
    public int difficulty; //difficulty to set on click
    public AudioClip hoverSound;
	public AudioClip clickSound;
	AudioSource audioSource;

	Vector3 initScale;

	void Start(){
		initScale = transform.localScale;
		audioSource = GetComponent<AudioSource>();
	}

	void OnMouseEnter()
    {
        transform.localScale = initScale * hoverScale;
        audioSource.PlayOneShot(hoverSound, 1f);
    }

    void OnMouseExit()
    {
        transform.localScale = initScale;
    }

    void OnMouseDown(){
    	transform.localScale = initScale * clickScale;
    }

    void OnMouseUpAsButton(){
    	transform.localScale = initScale * hoverScale;
    	audioSource.PlayOneShot(clickSound, 1F);
    	EventManager.StartTransitionEvent(destination);
        if(difficulty >= 0)
        {
            EventManager.StartDifficultyEvent(difficulty);
        }
    }
}
