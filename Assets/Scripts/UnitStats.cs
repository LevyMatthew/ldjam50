using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
	//0 - health
	//1 - max health
	//2 - armor
	//3 - attack damage
	//4 - attack speed
	//5 - run speed
	//6 - weight
	public int GetBaseStat(int i){
		return i;
	}

	//0 - health (no multiplier)
	//1 - max health (no multiplier)
	//2 - armor
	//3 - attack damage
	//4 - attack speed
	//5 - run speed
	//6 - weight
	public float GetMultiplier(int i){
		return 1 + (i / 10f);
	}
}
