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
    private Rigidbody rb;

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
            print("I see a unit");
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
        //Full spherecast to populate known units. Likely not needed, but 
        //possible that spawning things within range might not trigger?
    }

    public void FixedUpdate()
    {
        if (Time.frameCount % 100 == 0)
            RefreshBehaviour();
    }

    public void RefreshBehaviour()
    {
        Vector3 sightInfluence = behaviours.ReactionToUnitSight(seenUnits, this);
        Vector3 thoughtInfluence = behaviours.ReactionToUnitKnowledge(knownUnits);
        Vector3 combatInfluence = behaviours.ReactionToUnitAttack(knownAttacks);
        Vector3 forwardInfluence = behaviours.wanderBehaviour.forwardAffinity * transform.forward;
        Vector3 centreInfluence = behaviours.wanderBehaviour.centreAffinity * -transform.position.normalized;
        
        Vector3 belligerenceRotationalInfluence = behaviours.combatBehaviour.belligerence * stats.attackSpeed * Vector3.up;

        steering.SetRunDirection(sightInfluence + forwardInfluence + centreInfluence);
        steering.SetConstantTorque(belligerenceRotationalInfluence);
        print("Unit is running in direction");
        //Check behaviour for current sight, and send to unit steering
    }

}
 