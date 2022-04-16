using System.Collections.Generic;
using UnityEngine;

public enum UnitTeam {Player, PlayerTeam, ComputerTeam}

public class Unit : MonoBehaviour
{
    public UnitTeam team;
    public int health;
    public UnitStats stats;
    public UnitBehaviours behaviours;
    public UnitAbilities abilities;
    public UnitGoals intentions;

    public UnitLocomotion locomotion;
    public UnitSteering steering;

    private List<Unit> seenUnits = new List<Unit>();
    private List<UnitSightingKnowledge> knownUnits = new List<UnitSightingKnowledge>();
    private List<UnitAttackKnowledge> knownAttacks = new List<UnitAttackKnowledge>();

    public List<Transform> raycastSources;
    private Dictionary<Transform, RaycastHit> raycastHits = new Dictionary<Transform, RaycastHit>();
    public LayerMask layerMask;

    private Rigidbody rb;

    public Texture2D mapTexture;

    private float groundedThreshold = 0.1f;

    private void Start()
    {
        locomotion = GetComponent<UnitLocomotion>();
        steering = GetComponent<UnitSteering>();
        rb = GetComponent<Rigidbody>();

        rb.mass = stats.mass;
        health = stats.maxHealth;
    }

    /*public void OnTriggerEnter(Collider other)
    {
        Unit seenUnit = other.transform.GetComponent<Unit>();
        if (seenUnit){
            seenUnits.Add(seenUnit);
            //print("I see a unit");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Unit seenUnit = other.transform.GetComponent<Unit>();
        if (seenUnit){
            seenUnits.Remove(seenUnit);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Do something to build up the known Attacks
    }*/

    public void RefreshSight()
    {
        foreach(Transform source in raycastSources){
            RaycastHit hit;
            //if this source saw something
            if (Physics.Raycast(source.position, source.forward, out hit, 100, layerMask))
            {
                raycastHits[source] = hit;
                Debug.DrawRay(source.position, source.forward * hit.distance, Color.yellow);
            }
            else{
                raycastHits.Remove(source);
            }
        }
    }

    public void FixedUpdate()
    {
        //if (Time.frameCount % 5 == 0)
            //RefreshSight();
        if (Time.frameCount % 1 == 0)
            RefreshBehaviour();
    }

    private bool IsGrounded(){
        return transform.position.y <= groundedThreshold;
    }

    public void RefreshBehaviour()
    {
        //Vector3 sightInfluence = behaviours.ReactionToUnitSight(seenUnits, this);
        //Vector3 thoughtInfluence = behaviours.ReactionToUnitKnowledge(knownUnits);
        //Vector3 combatInfluence = behaviours.ReactionToUnitAttack(knownAttacks);
        //Vector3 forwardInfluence = behaviours.wanderBehaviour.forwardAffinity * transform.forward;
        Vector3 centreInfluence = behaviours.wanderBehaviour.centreAffinity * -transform.position.normalized;
        //Vector3 obstacleInfluence = behaviours.ReactionToObstacleSight(raycastHits, transform);
        Vector3 mapInfluence = behaviours.ReactionToMap(this);

        Vector3 intendedMoveDirection = centreInfluence + mapInfluence;//sightInfluence
            //+ thoughtInfluence
            //+ combatInfluence
            //+ forwardInfluence
            //+ centreInfluence 
            //+ obstacleInfluence;

        //positive to turn left, negative to turn right
        //float turnInfluence = behaviours.wanderBehaviour.velocityAffinity
            //* Vector3.SignedAngle(intendedMoveDirection, transform.forward, Vector3.up);

        //Set a debug vector = intendedMoveDirection of a specific color so you can see it in scene view
        Debug.DrawLine(transform.position, transform.position + intendedMoveDirection, Color.red, 0.5f);

        if (IsGrounded()){
            //# SetRunDirection(intendedMoveDirection);
            //rb.velocity = intendedMoveDirection * stats.runSpeed;
            steering.SetRunDirection(intendedMoveDirection);
            if (centreInfluence != Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(new Vector3(-centreInfluence.x, 0, -centreInfluence.z), Vector3.up);
            }
        }
        else{
        }
    }

}
 