using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavigation : MonoBehaviour
{

	public Transform destinationTransform;

	NavMeshAgent agent;
	Rigidbody rb;

	float runSpeed = 25f;
	float timeLast;

	NavMeshPath path;

	void Awake(){
		agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
		agent.updatePosition = false;
		agent.updateRotation = false;
		agent.SetDestination(destinationTransform.position);
		path = new NavMeshPath();
	}

	void FixedUpdate(){
		if(rb.position.y <= 1.0f)
		{
			float currTime = Time.time;
			if(currTime - timeLast >= 0.5f){
				agent.SetDestination(destinationTransform.position);
				NavMesh.CalculatePath(transform.position, destinationTransform.position, agent.areaMask, path);
				agent.SetPath(path);
				timeLast = currTime;
			}
			Vector3 current = rb.velocity;
			Vector3 desired = (agent.nextPosition - rb.position);
			
			current.y = 0;
			desired.y = 0;

			rb.AddForce((desired - current).normalized * runSpeed, ForceMode.Force);
		}
	}

	// public Transform target;
 //    private NavMeshPath path;
 //    private float elapsed = 0.0f;
 //    private Rigidbody rb;

 //    private float runSpeed = 15f;

 //    private int pathPosition = 0;
  
 //    void Start()
 //    {
 //        path = new NavMeshPath();
 //        rb = GetComponent<Rigidbody>();
 //        elapsed = 0.0f;
 //    }

 //    void Update()
 //    {
 //        // Update the way to the goal every second.
 //        elapsed += Time.deltaTime;
 //        if (elapsed > 1f)
 //        {
 //            elapsed -= 1f;
 //            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
 //            print("Recalculating");
 //            pathPosition = 0;
 //        }
 //        for (int i = 0; i < path.corners.Length - 1; i++){
 //            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
 //        }

 //        if(path.corners.Length > 1){
	//         Vector3 current = rb.velocity;
	// 		Vector3 desired = (path.corners[1] - rb.position);
			
	// 		current.y = 0;
	// 		desired.y = 0;

	// 		rb.AddForce((desired - current).normalized * runSpeed, ForceMode.Force);
	// 	}
	// }
}