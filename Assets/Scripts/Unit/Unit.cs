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
    private List<Collider> seenWalls = new List<Collider>();
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

        Wall seenWall = other.transform.GetComponent<Wall>();
        if (seenWall){
            seenWalls.Add(other);
            print("I see a wall");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Unit seenUnit = other.transform.GetComponent<Unit>();
        if (seenUnit){
            seenUnits.Remove(seenUnit);
        }

        Wall seenWall = other.transform.GetComponent<Wall>();
        if (seenWall){
            seenWalls.Remove(other);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Do something to build up the known Attacks
    }

    public void RefreshSight()
    {
        //Full spherecast to populate known units. Likely not needed, but 
        //possible that spawning things within range might not trigger?
    }

    public void FixedUpdate()
    {
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
        Vector3 wallInfluence = behaviours.ReactionToWallSight(seenWalls, this);
        
        Vector3 belligerenceRotationalInfluence = Vector3.zero; //TODO face forward
        if(IsGrounded()){
            //steering.SetRunDirection(sightInfluence + forwardInfluence + centreInfluence - wallInfluence);
            steering.SetRunDirection(centreInfluence + wallInfluence);
            steering.SetConstantTorque(belligerenceRotationalInfluence);
            //print("Unit is running in direction");
            //Check behaviour for current sight, and send to unit steering
        }
        else{
            steering.SetRunDirection(Vector3.zero);
            steering.SetConstantTorque(Vector3.zero);
            //print("I'm in the air");
        }
    }

}
 