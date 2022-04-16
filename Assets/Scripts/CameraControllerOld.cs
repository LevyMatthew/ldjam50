using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControllerOld : MonoBehaviour
{

	public float mouseSpeed = 80.0f;
	public float scrollSpeed = 3.0f;
	public float panSpeed = 80f;

	public float zoomInLimit = -10;
	public float zoomDefault = -100;
	public float zoomOutLimit = -300;

	bool cameraLocked = false;

	[SerializeField]
	List<Transform> cameraPoses;

	void Start()
	{
	}

	void SetTarget(Transform emptyTarget){
		transform.position = emptyTarget.position;
		transform.rotation = emptyTarget.rotation;
	}

	private void SetMode(int m){
		SetTarget(cameraPoses[m]);
	}

	private void MouseOrbit(){
		Vector3 pos = transform.position;
		if(Input.GetMouseButton(1)){
			Cursor.lockState = CursorLockMode.Locked;
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
		transform.position = pos;
	}

	private void MouseRotate(){
		if(Input.GetMouseButton(1)){
			Cursor.lockState = CursorLockMode.Locked;
			float deltaYaw = mouseSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
			float deltaPitch = mouseSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

			Vector3 rot = transform.eulerAngles;
			transform.eulerAngles = new Vector3(-deltaPitch, deltaYaw, 0.0f) + rot;
		}
		else{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void MouseZoom(){
		float z = Camera.main.transform.localPosition.z + Input.mouseScrollDelta.y * scrollSpeed;
		z = Mathf.Clamp(z, zoomOutLimit, zoomInLimit);
		Camera.main.transform.localPosition = new Vector3(0, 5, z);
	}

	private void MousePan(){
		float h = panSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		float v = panSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
		Vector3 pos = transform.localPosition;
		transform.localPosition = transform.forward * v + transform.right * h + pos;
	}

	// Update is called once per frame
	void Update()
	{
		if(cameraLocked){
			return;
		}
		//MouseOrbit();
		MouseRotate();
		MousePan();
		//MouseZoom();
	}
}
