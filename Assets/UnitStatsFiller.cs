using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitStatsFiller : MonoBehaviour
{
	public TextMeshProUGUI healthBase;

    public void UpdateStats(UnitStats unitStats){
    		healthBase.SetText("{0}", unitStats.GetBaseStat(1));
    }
}
