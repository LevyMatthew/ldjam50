using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Stats")]
public class UnitStats : ScriptableObject
{
    public int moveSpeed;
    
    public const int HealthId = 0;
    public const int MaxHealthId = 1;
    public const int ArmorId = 2;
    public const int AttackDamageId = 3;
    public const int AttackSpeedId = 4;
    public const int RunSpeedId = 5;
    public const int MassId = 6;
	//0 - health
	//1 - max health
	//2 - armor
	//3 - attack damage
	//4 - attack speed
	//5 - run speed
	//6 - mass
	public int GetBaseStat(int i){
        if (i == RunSpeedId)
            return moveSpeed;
		return i;
	}

	//0 - health (no multiplier)
	//1 - max health (no multiplier)
	//2 - armor
	//3 - attack damage
	//4 - attack speed
	//5 - run speed
	//6 - mass
	public float GetMultiplier(int i){
		return 1 + (i / 10f);
	}

	public string GetDescription(){
		return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque bibendum felis rutrum, porta nibh sed, vestibulum risus. Etiam ut odio at nisi molestie pulvinar a fermentum tellus.";
	}

    public float GetStat(int i)
    {
        return GetBaseStat(i) * GetMultiplier(i);
    }
}
