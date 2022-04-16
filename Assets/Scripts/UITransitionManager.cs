using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UITransitionManager : MonoBehaviour
{
	public List<CinemachineVirtualCamera> cameras;
    public List<GameObject> menus;
	public int currentCameraID;
	private CinemachineVirtualCamera currentCamera;

    private float startCloseTime = 0.0f;
    private float currentTime = 0.0f;
    private int menuToClose = -1;
    private float menuCloseDuration = 1.0f;


    public void Start()
	{
		EventManager.TransitionEvent += UpdateCamera;
        currentCamera = cameras[currentCameraID];
		currentCamera.Priority++;

        for(int i = 0; i < menus.Count; i++)
        {
            menus[i]?.SetActive(false);
        }
        menus[currentCameraID]?.SetActive(true);
	}

    public void UpdateCamera(int target){
        menus[target]?.SetActive(true);
        CloseAfterTime(currentCameraID);

        currentCamera.Priority--;
		currentCameraID = target;
		currentCamera = cameras[currentCameraID];
		currentCamera.Priority++;
    }

    private void CloseAfterTime(int m)
    {
        startCloseTime = Time.time;
        menuToClose = m;
    }

    private void Update()
    {
        if(menuToClose >= 0)
        {
            currentTime = Time.time;
            if (currentTime - startCloseTime >= menuCloseDuration)
            {
                menus[menuToClose]?.SetActive(false);
                menuToClose = -1;
            }
        }
    }

    private void OnDisable(){
		EventManager.TransitionEvent -= UpdateCamera;
	}
}
