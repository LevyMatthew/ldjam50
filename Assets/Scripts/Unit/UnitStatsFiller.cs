using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitStatsFiller : MonoBehaviour
{
    //Attach component to UI HUD system
    //Populates text meshes using UnitStats
	public TextMeshProUGUI healthBase;
	public TextMeshProUGUI armorBase;
	public TextMeshProUGUI attackDamageBase;
	public TextMeshProUGUI attackSpeedBase;
	public TextMeshProUGUI runSpeedBase;
	public TextMeshProUGUI massBase;

	// public TextMeshProUGUI healthMultiplier; //no multiplier
	// public TextMeshProUGUI armorMultiplier;
	// public TextMeshProUGUI attackDamageMultiplier;
	// public TextMeshProUGUI attackSpeedMultiplier;
	// public TextMeshProUGUI runSpeedMultiplier;
	// public TextMeshProUGUI massMultiplier;

	public TextMeshProUGUI description;

	//0 - health
	//1 - max health
	//2 - armor
	//3 - attack damage
	//4 - attack speed
	//5 - run speed
	//6 - weight
    public void UpdateStats(Unit unit){
    	UnitStats unitStats = unit.stats;
		healthBase.SetText("{0}/{1}", unit.health, unitStats.maxHealth);
		armorBase.SetText("{0}", unitStats.armor);
		attackDamageBase.SetText("{0}", unitStats.attackDamage);
		attackSpeedBase.SetText("{0}", unitStats.attackSpeed);
		runSpeedBase.SetText("{0}", unitStats.runSpeed);
		massBase.SetText("{0}", unitStats.mass);

		// armorMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(2));
		// attackDamageMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(3));
		// attackSpeedMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(4));
		// runSpeedMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(5));
		// weightMultiplier.SetText("x{0:1}", unitStats.GetMultiplier(6));

		description.SetText(unitStats.description);
    }
}
