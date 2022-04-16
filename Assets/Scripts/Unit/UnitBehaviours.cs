using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Behaviours")]
public class UnitBehaviours : ScriptableObject
{
    [Serializable]
    public struct WanderBehaviour
    {
        [Header("Steering Affinities")]
        [Range(-1f,1f),Tooltip("How much self wants to be near unit")]
        public float friendlyUnitAffinity;
        [Range(-1f,1f),Tooltip("How much self wants to align forward with unit")]
        public float friendlyUnitAlignment;
        [Range(-1f,1f),Tooltip("How much self wants to be near unit")]
        public float enemyUnitAffinity;
        [Range(-1f,1f),Tooltip("How much self wants to align forward with unit")]
        public float enemyUnitAlignment;
        [Range(-1f, 1f),Tooltip("How much self wants to walk forward")]
        public float forwardAffinity;
        [Range(-1f, 1f),Tooltip("How much self wants to walk towards origin")]
        public float centreAffinity;
        [Range(-1f, 1f),Tooltip("How much self wants to walk towards obstacles")]
        public float obstacleAffinity;
        [Range(-1f, 1f),Tooltip("How much self wants to walk along surfaces of obstacles")]
        public float obstacleSliding;
        [Range(-1f, 1f), Tooltip("How much self wants to follow the map")]
        public float mapAffinity;
        [Header("Rotation Affinities")]
        [Range(-1f,1f),Tooltip("How much self wants to face where they're going")]
        public float velocityAffinity;
        [Range(-1f,1f),Tooltip("How much self wants to face seen obstacles")]
        public float sightAffinity;
    }

    [Serializable]
    public struct CombatBehaviour
    {
        [Range(-1f, 1f)]
        public float belligerence; //Flee = -1, Fight = 1. Cohesion/Separation
    }

    public WanderBehaviour wanderBehaviour;
    public CombatBehaviour combatBehaviour;
   

    public Vector3 ReactionToUnitSight(List<Unit> seenUnits, Unit seer)
    {
        Vector3 intendedMoveDirection = Vector3.zero;
        foreach (Unit u in seenUnits)
        {
            bool isEnemy = (u.team != seer.team);
            float sentiment;

            if (isEnemy)
                sentiment = wanderBehaviour.enemyUnitAffinity;
            else
                sentiment = wanderBehaviour.friendlyUnitAffinity;

            Vector3 displacement = u.transform.position - seer.transform.position;
            Vector3 sqrDirection = displacement / displacement.sqrMagnitude;
            intendedMoveDirection += sqrDirection * sentiment;
        }
        return intendedMoveDirection;
    }

    public Vector3 ReactionToObstacleSight(Dictionary<Transform, RaycastHit> seenObstacles, Transform seer)
    {
        Vector3 intendedMoveDirection = Vector3.zero;
        foreach (var item in seenObstacles)
        {
            Transform source = item.Key;
            RaycastHit hit = item.Value;
            Vector3 projection = Vector3.Project(seer.forward, hit.normal);
            Vector3 rejection = seer.forward - projection;
            float dist = hit.distance;
            float importance = Mathf.Exp(-dist);
            intendedMoveDirection += importance * (rejection * wanderBehaviour.obstacleSliding - hit.normal * wanderBehaviour.obstacleAffinity);
        }
        return intendedMoveDirection;
    }

    public Vector3 ReactionToUnitKnowledge(List<UnitSightingKnowledge> nearbyUnits)
    {
        Vector3 intendedMoveDirection = Vector3.zero;
        foreach (UnitSightingKnowledge uk in nearbyUnits)
        {
            float sentiment;
            if (uk.isEnemy)
                sentiment = wanderBehaviour.enemyUnitAffinity;
            else
                sentiment = wanderBehaviour.friendlyUnitAffinity;
            Vector3 displacement = uk.spotted.transform.position - uk.reporter.transform.position;
            Vector3 sqrDirection = displacement / displacement.sqrMagnitude;
            intendedMoveDirection += sqrDirection * sentiment;
        }
        return intendedMoveDirection;
    }

    public Vector3 ReactionToMap(Unit seer)
    {
        Vector3 intendedMoveDirection = Vector3.zero;
        float sentiment = wanderBehaviour.mapAffinity;
        //Color color = seer.mapTexture.GetPixel((int)(seer.transform.position.x), (int)(seer.transform.position.z));
        float angle = Mathf.Atan2(seer.transform.position.z, seer.transform.position.x);
        float deg = angle * 180f / Mathf.PI;
        Vector3 a = -seer.transform.right;
        Vector3 b = seer.transform.right;
        float t;

        if (deg >= 0 && deg <= 60)
        {
            t = (deg - 0f) / 60f;
            intendedMoveDirection = Vector3.Lerp(a, b, t);
        }
        else if (deg >= 60 && deg <= 120)
        {
            t = (deg - 60f) / 60f;
            intendedMoveDirection = Vector3.Lerp(a, b, t);
        }
        else if (deg >= 120 && deg <= 180)
        {
            t = (deg - 120f) / 60f;
            intendedMoveDirection = Vector3.Lerp(a, b, t);
        }

        //units care more about the doors as they get closer to the centre
        float dist = seer.transform.position.magnitude;
        sentiment *= Mathf.Lerp(1f, 0, (dist - 50f) / 100f);

        return intendedMoveDirection * sentiment;
    }

    public Vector3 ReactionToUnitAttack(List<UnitAttackKnowledge> attacks)
    {
        //Debug.Log("Reaction to combat not implemented yet");
        return Vector3.zero;
    }
}

public struct UnitSightingKnowledge
{
    public Unit spotted;
    public Unit reporter;
    public bool isEnemy;

    public UnitSightingKnowledge(Unit reporter, Unit spotted) : this()
    {
        this.spotted = spotted;
        this.reporter = reporter;
        isEnemy = (this.spotted.team != this.reporter.team);
    }
}

public struct UnitAttackKnowledge
{
    public Unit instigator;
    public Unit victim;
    public int damage;

    public UnitAttackKnowledge(Unit victim, Unit instigator, int damage) : this()
    {
        this.victim = victim;
        this.instigator = instigator;
        this.damage = damage;
    }
}