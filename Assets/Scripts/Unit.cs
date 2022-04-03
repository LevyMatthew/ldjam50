using UnityEngine;

public enum UnitTeam {Player, PlayerTeam, ComputerTeam}

public class Unit : MonoBehaviour
{
    public UnitTeam team;
    public UnitStats stats;
    public UnitAbilities abilities;
    public UnitGoals intentions;
}
 