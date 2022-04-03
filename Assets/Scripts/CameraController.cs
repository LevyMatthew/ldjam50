using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

	float mouseSpeed = 2.0f;

	public Canvas unitStatsHUD;
	Canvas hudInstance;

	[SerializeField]
	List<GameObject> unitTemplate;
	[SerializeField]
	List<Button> shopButton;
	[SerializeField]
	List<float> cooldownDuration; //seconds

	[SerializeField]
	List<Transform> cameraPoses;

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

	void SetTarget(Transform emptyTarget){
		transform.position = emptyTarget.position;
		transform.rotation = emptyTarget.rotation;
	}

	public void SetMode(int m){
		SetTarget(cameraPoses[m]);
	}

	// Update is called once per frame
	void Update()
	{
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
		if(Input.GetMouseButton(1)){
			float deltaYaw = mouseSpeed * Input.GetAxis("Mouse X");
			float deltaPitch = mouseSpeed * Input.GetAxis("Mouse Y");
			Vector3 pivotPoint = Vector3.zero;
			transform.RotateAround(pivotPoint, 
						                     Vector3.up,
						          		  	deltaYaw);

     		transform.RotateAround(pivotPoint, 
		                                     transform.right,
		                                     -deltaPitch);
		}
		if(Input.GetMouseButtonDown(0)){
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
					Unit unit = obj.GetComponent<Unit>();
					if(unit && unit.stats){
						UnitStatsFiller usf = unitStatsHUD.GetComponent<UnitStatsFiller>();
						usf.UpdateStats(unit.stats);
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
