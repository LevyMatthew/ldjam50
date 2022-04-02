using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

	float mouseSpeed = 2.0f;
	float xRot = 0.0f;
	float yRot = 0.0f;

	[SerializeField]
	List<GameObject> unitPrefab;
	[SerializeField]
	List<Button> shopButton;
	[SerializeField]
	List<float> cooldownDuration;

	List<Image> cooldownImage;
	List<float> prevClickTime;
	List<bool> cooldownComplete;
	int unitHeld = 0;

	// Start is called before the first frame update
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

	void SpawnUnit(int team, Vector3 location, int type){
		// spawning allies
		if(team == 0){
			Instantiate(unitPrefab[type], location, Quaternion.identity);
			unitHeld = -1;
		}
	}

	public void OnClick(int id){
		Debug.Log(id);
		float currTime = Time.time;
		if(cooldownComplete[id]){
			unitHeld = id;
			cooldownImage[id].fillAmount = 1f;
			prevClickTime[id] = currTime;
			cooldownComplete[id] = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		float currTime = Time.time;
		float fillRatio = 0.0f;
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
		if(Input.GetMouseButton(1)){
			float h = mouseSpeed * Input.GetAxis("Mouse X");
			float v = mouseSpeed * Input.GetAxis("Mouse Y");

			xRot += h;
			yRot -= v;

			transform.rotation = Quaternion.Euler(yRot, xRot, 0);
		}
		if(!EventSystem.current.IsPointerOverGameObject(-1) && Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit)) {
				Vector3 pos = hit.point;
				pos = new Vector3(pos.x, pos.y + 10, pos.z);
				if(unitHeld >= 0){
					SpawnUnit(0, pos, unitHeld);
				}
			}
		}
	}
}
