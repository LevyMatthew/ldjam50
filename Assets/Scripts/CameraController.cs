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

	public Canvas unitStatsHUD;
	Canvas hudInstance;

	[SerializeField]
	List<GameObject> unitTemplate;
	[SerializeField]
	List<Button> shopButton;
	[SerializeField]
	List<float> cooldownDuration; //seconds

	List<Image> cooldownImage;
	List<float> prevClickTime;
	List<bool> cooldownComplete;
	int unitHeld = -1;

	// Start is called before the first frame update
	void Start()
	{
		unitStatsHUD.gameObject.SetActive(false);
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
			Instantiate(unitTemplate[type], location, Quaternion.identity);
			unitHeld = -1;
		}
		else{
			print("Not Implemented: Spawn Enemy Unit");
		}
	}

	public void OnClick(int id){
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
				GameObject obj = hit.transform.gameObject;
				Vector3 pos = hit.point;
				pos = new Vector3(pos.x, pos.y + 10, pos.z);
				if(unitHeld >= 0){
					SpawnUnit(0, pos, unitHeld);
				}
				else{
					UnitStats stats = obj.GetComponent<UnitStats>();
					if(stats){ //If we hit a model?
						UnitStatsFiller usf = unitStatsHUD.GetComponent<UnitStatsFiller>();
						usf.UpdateStats(stats);
						unitStatsHUD.transform.SetParent(obj.transform, false);
						unitStatsHUD.gameObject.SetActive(true);
					}
					else{
						unitStatsHUD.gameObject.SetActive(false);
					}
				}
			}
		}
	}
}
