using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Rules")]
public class UnitRules : ScriptableObject
{
    public int moveSpeed;    
    [SerializeField]
    public Dictionary<UnitTeam,float> unitSentiment; //Unit->Float(-1 to 1)
    public float unitBelligerence = 1; //Flee = -1, Fight = 1. Cohesion/Separation

    public class UnitKnowledge
    {
        public Unit spotted;
        public Unit reporter;
    }

    public void Decide(List<UnitKnowledge> nearbyUnits)
    {
        foreach (UnitKnowledge uk in nearbyUnits)
        {
            float sentiment = unitSentiment[uk.spotted.team];
            Vector3 relativePosition = uk.spotted.transform.position - uk.reporter.transform.position;
            //Steer influence force = relativePosition * sentimentzdt
        }
    }

    
}