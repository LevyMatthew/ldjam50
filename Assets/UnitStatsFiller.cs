using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitStatsFiller : MonoBehaviour
{
	public TextMeshProUGUI healthBase;
	public TextMeshProUGUI armorBase;
	public TextMeshProUGUI attackDamageBase;
	public TextMeshProUGUI attackSpeedBase;
	public TextMeshProUGUI runSpeedBase;
	public TextMeshProUGUI weightBase;

	public TextMeshProUGUI healthMultiplier; //no multiplier
	public TextMeshProUGUI armorMultiplier;
	public TextMeshProUGUI attackDamageMultiplier;
	public TextMeshProUGUI attackSpeedMultiplier;
	public TextMeshProUGUI runSpeedMultiplier;
	public TextMeshProUGUI weightMultiplier;

	public TextMeshProUGUI description;

	//0 - health
	//1 - max health
	//2 - armor
	//3 - attack damage
	//4 - attack speed
	//5 - run speed
	//6 - weight
    public void UpdateStats(UnitStats unitStats){
		healthBase.SetText("{0}/{1}", unitStats.GetBaseStat(0), unitStats.GetBaseStat(1));
		armorBase.SetText("{0}", unitStats.GetBaseStat(2));
		attackDamageBase.SetText("{0}", unitStats.GetBaseStat(3));
		attackSpeedBase.SetText("{0}", unitStats.GetBaseStat(4));
		runSpeedBase.SetText("{0}", unitStats.GetBaseStat(5));
		weightBase.SetText("{0}", unitStats.GetBaseStat(6));

		armorMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(2));
		attackDamageMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(3));
		attackSpeedMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(4));
		runSpeedMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(5));
		weightMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(6));

		description.SetText(unitStats.GetDescription());
    }
}
