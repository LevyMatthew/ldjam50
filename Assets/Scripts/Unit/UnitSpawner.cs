using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	public DifficultySettings difficulty;

	public List<GameObject> units;

	private void Spawn(){
		int unitID = Random.Range(0, units.Count);
		Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		Vector3 position = rotation * (Vector3.right * difficulty.radius);
		Instantiate(units[unitID], position, Quaternion.identity);
	}

	private void FixedUpdate(){
		if(Time.frameCount % 50*difficulty.interval == 0){
			Spawn();
		}
	}
}
