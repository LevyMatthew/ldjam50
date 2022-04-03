using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Rules")]
public class UnitRules : ScriptableObject
{
    public int moveSpeed;    
    public Dictionary<UnitTeam,float> unitSentiment; //Unit->Float(-1 to 1)

}