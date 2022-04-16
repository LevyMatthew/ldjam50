using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Transform cameraTransform;

	public float normalSpeed;
	public float fastSpeed;
	public float movementSpeed;
	public float movementTime;
	public float rotationAmount;
    public float rotationMax;
    public float rotationMin;
	public Vector3 zoomAmount;
    public float zoomMax;
    public float zoomMin;

    public Vector3 newPosition;
	public Quaternion newRotation;
	public Vector3 newZoom;

	public Vector3 dragStartPosition;
	public Vector3 dragCurrentPosition;
	public Vector3 rotation;

    float rotX = 0;
    float rotY = 0;


    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newZoom = cameraTransform.localPosition;
        rotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    	HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput(){
    	if(Input.mouseScrollDelta.y != 0){
            Vector3 delta = Input.mouseScrollDelta.y * zoomAmount;
            if((newZoom + delta).y > zoomMax)
            {
                newZoom = new Vector3(0, zoomMax, -zoomMax);
            }
            else if((newZoom + delta).y < zoomMin)
            {
                newZoom = new Vector3(0, zoomMin, -zoomMin);
            }
            else
            {
                newZoom += delta;
            }
    	}

    	if(Input.GetMouseButtonDown(0)){
    		Plane plane = new Plane(Vector3.up, Vector3.zero);

    		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    		float entry;

    		if(plane.Raycast(ray, out entry)){
    			dragStartPosition = ray.GetPoint(entry);
    		}
    	}

    	if(Input.GetMouseButton(0)){
    		Plane plane = new Plane(Vector3.up, Vector3.zero);

    		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    		float entry;

    		if(plane.Raycast(ray, out entry)){
    			dragCurrentPosition = ray.GetPoint(entry);

    			newPosition = transform.position + dragStartPosition - dragCurrentPosition;
    		}
    	}

    	if(Input.GetMouseButtonDown(1)){
    		//rotateStartPosition = Input.mousePosition;
    	}

    	if(Input.GetMouseButton(1)){
            //rotateCurrentPosition = Input.mousePosition;

            //Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            //rotateStartPosition = rotateCurrentPosition;
            float dx = Input.GetAxis("Mouse X") * rotationAmount;
            float dy = -Input.GetAxis("Mouse Y") * rotationAmount;
            rotX += dx;
            if (rotY + dy >= rotationMax)
            {
                rotY = rotationMax;
            }
            else if (rotY + dy <= rotationMin)
            {
                rotY = rotationMin;
            }
            else
            {
                rotY += dy;
            }
            rotation = new Vector3(rotY, rotX, 0);
            rotation.z = 0;

            //rotation = transform.eulerAngles + new Vector3(difference.y, -difference.x, 0) * rotationAmount;
            //rotation = transform.eulerAngles + new Vector3(difference.y, 0, 0);

            //newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }


    	// if(Input.GetMouseButtonDown(2)){
    	// 	rotateStartY = Input.mousePosition;
    	// }

    	// if(Input.GetMouseButton(2)){
    	// 	rotateCurrentY = Input.mousePosition;

    	// 	Vector3 difference = rotateStartY - rotateCurrentY;

    	// 	rotateStartY = rotateCurrentY;

    	// 	rotation = transform.eulerAngles + new Vector3(difference.y, 0, 0);

    	// 	//newRotation *= Quaternion.Euler(Vector3.right * (difference.y / 5f));
    	// }
    }

    void HandleMovementInput(){
    	if(Input.GetKey(KeyCode.LeftShift)){
    		movementSpeed = fastSpeed;
    	}
    	else{
    		movementSpeed = normalSpeed;
    	}

        Vector3 fwd = transform.forward;
        fwd.y = 0;
        fwd = fwd.normalized;

        Vector3 right = transform.right;
        right.y = 0;
        right = right.normalized;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
    		newPosition += (fwd * movementSpeed);
    	}
    	if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
    		newPosition += (fwd * -movementSpeed);
    	}
    	if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
    		newPosition += (right * movementSpeed);
    	}
    	if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
    		newPosition += (right * -movementSpeed);
    	}

    	/*if(Input.GetKey(KeyCode.Q)){
    		newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
    	}
    	if(Input.GetKey(KeyCode.E)){
    		newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
    	}

    	if(Input.GetKey(KeyCode.R)){
    		newZoom += zoomAmount;
    	}
    	if(Input.GetKey(KeyCode.F)){
    		newZoom -= zoomAmount;
    	}*/


    	transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
