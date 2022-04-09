using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{

	Plane groundPlane = new Plane(new Vector3(0, 1, 0), new Vector3(0, 0, 0));

	[SerializeField]
	List<GameObject> entityTemplate;
	[SerializeField]
	List<Button> shopButton;
	[SerializeField]
	List<float> cooldownDuration; //seconds
	Unit hoveredUnit; // the unit being hovered over
	Unit selectedUnit; // the unit that has been selected with left click
	
	List<Image> cooldownImage;
	List<float> prevClickTime;
	List<bool> cooldownComplete;
	int entityHeld = -1;
	List<Image> frameImage;

	GameObject statsGameObject;
	Vector3 groundPos;

	void Start()
	{
		cooldownImage = new List<Image>();
		prevClickTime = new List<float>();
		cooldownComplete = new List<bool>();
		for(int i = 0; i < shopButton.Count; i++){
			cooldownImage.Add(shopButton[i].transform.GetChild(0).GetComponent<Image>());
			prevClickTime.Add(0.0f);
			cooldownComplete.Add(true);
			cooldownImage[i].fillAmount = 0.0f;
		}
	}

	private Vector3 GetGroundPos(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float enter = 0.0f;
		Vector3 hitPoint = new Vector3(0, -1, 0);
		
		if (groundPlane.Raycast(ray, out enter))
		{
			hitPoint = ray.GetPoint(enter);
			hitPoint.y = 0;
		}
		return hitPoint;
	}

	void SpawnEntity(Vector3 location, int type){
		if(type == 0){
			Instantiate(entityTemplate[type], location, Quaternion.identity);
		}
		else if(type == 1){
			Instantiate(entityTemplate[type], location, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 180, 0));
		}
		else if(type == 2){
			Instantiate(entityTemplate[type], location + new Vector3(0, 3f, 0), Quaternion.identity);
		}
		else if(type == 3){
			Instantiate(entityTemplate[type], location + new Vector3(0, 3f, 0), Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 180, 0));
		}
		entityHeld = -1;
	}

	public void OnClick(int id){
		float currTime = Time.time;
		if(cooldownComplete[id]){
			entityHeld = id;
			cooldownImage[id].fillAmount = 1f;
			prevClickTime[id] = currTime;
			cooldownComplete[id] = false;
		}
	}

	private void ManageCooldowns(){
		float currTime = Time.time;
		float fillRatio = 0.0f;
		if (cooldownImage != null) { 
			for(int i = 0; i < cooldownImage.Count; i++){
				if(!cooldownComplete[i]){
					fillRatio = (currTime - prevClickTime[i])/cooldownDuration[i];
					if(fillRatio > 1){
						cooldownImage[i].fillAmount = 0.0f;
						cooldownComplete[i] = true;
					}
					else{
						cooldownImage[i].fillAmount = 1 - fillRatio;
					}
				}
			}
		}
		else
		{
			print("CameraController: Cooldown Image Not Found");
		}
	}

	private bool MouseOverMenu(){
		return EventSystem.current.IsPointerOverGameObject();
	}

	void Update()
	{
		ManageCooldowns();
		groundPos = GetGroundPos();
		if(groundPos.y != -1){
			// move the hand to the raycast point on the ground
			transform.position = groundPos;
			// left click
			if(Input.GetMouseButtonDown(0) && !MouseOverMenu()){
				if(entityHeld >= 0){
					SpawnEntity(groundPos, entityHeld);
					//print(entityHeld);
					//print(groundPos);
				}
			}
		}
	}
}
