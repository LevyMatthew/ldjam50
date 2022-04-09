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

    private float groundedThreshold = 0.1f;

    private void Start()
    {
        locomotion = GetComponent<UnitLocomotion>();
        steering = GetComponent<UnitSteering>();
        rb = GetComponent<Rigidbody>();

        rb.mass = stats.mass;
        health = stats.maxHealth;
    }

    public void OnTriggerEnter(Collider other)
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
    }

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
        if (Time.frameCount % 5 == 0)
            RefreshSight();
        if (Time.frameCount % 10 == 0)
            RefreshBehaviour();
    }

    private bool IsGrounded(){
        return transform.position.y <= groundedThreshold;
    }

    public void RefreshBehaviour()
    {
        Vector3 sightInfluence = behaviours.ReactionToUnitSight(seenUnits, this);
        Vector3 thoughtInfluence = behaviours.ReactionToUnitKnowledge(knownUnits);
        Vector3 combatInfluence = behaviours.ReactionToUnitAttack(knownAttacks);
        Vector3 forwardInfluence = behaviours.wanderBehaviour.forwardAffinity * transform.forward;
        Vector3 centreInfluence = behaviours.wanderBehaviour.centreAffinity * -transform.position.normalized;
        Vector3 obstacleInfluence = behaviours.ReactionToObstacleSight(raycastHits, transform);
        
        //positive to turn left, negative to turn right
        float rotationInfluence = behaviours.RotationReactionToMisalignment(transform.forward, rb.velocity);
        print(rotationInfluence);
        if(IsGrounded()){
            //steering.SetRunDirection(sightInfluence + forwardInfluence + centreInfluence + wallInfluence);
            steering.SetRunDirection(centreInfluence + obstacleInfluence);
            steering.SetTurningDirection(rotationInfluence);
            //print("Unit is running in direction");
            //Check behaviour for current sight, and send to unit steering
        }
        else{
            steering.SetRunDirection(Vector3.zero);
            steering.SetTurningDirection(0);
            //print("I'm in the air");
        }
    }

}
 