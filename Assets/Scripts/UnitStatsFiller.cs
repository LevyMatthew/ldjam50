using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitStatsFiller : MonoBehaviour
{
    //Attach component to UI HUD system
    //Populates text meshes using UnitStats
	public TextMeshProUGUI healthBase;

    public void UpdateStats(UnitStats unitStats){
    		healthBase.SetText("{0}", unitStats.GetBaseStat(UnitStats.HealthId));
    }
}
