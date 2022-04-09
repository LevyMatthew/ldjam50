using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	public DifficultySettings difficulty;
	public List<DifficultySettings> difficultyOptions;

	public List<GameObject> units;

	float lastSpawnTime = 0.0f;
	int totalSpawns = 0;

	private void Spawn(){
		int unitID = 0; //TODO: Random sampling from set
		Quaternion rotation = Quaternion.Euler(0, Random.Range(difficulty.angleOffset, difficulty.angleOffset + difficulty.angleRange), 0);
		Vector3 position = rotation * (Vector3.right * difficulty.radius);
		Instantiate(units[unitID], position, Quaternion.identity);
		lastSpawnTime = Time.time;
		totalSpawns++;
	}

	public void ResetSpawnCount(){
		lastSpawnTime = 0.0f;
		totalSpawns = 0;
	}

	public void SetDifficulty(int d){
		difficulty = difficultyOptions[d];
	}

	private void FixedUpdate(){
		if(totalSpawns + 1 <= difficulty.spawnLimit && Time.time - lastSpawnTime > difficulty.interval){
			Spawn();
		}
	}
}
