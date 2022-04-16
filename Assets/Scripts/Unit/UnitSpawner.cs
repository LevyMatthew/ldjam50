using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	public DifficultySettings difficulty;
	public List<DifficultySettings> difficultyOptions;

	public List<GameObject> units;

	private bool spawning = true;

	float lastSpawnTime = 0.0f;
	int totalSpawns = 0;

	void Start(){
        EventManager.TransitionEvent += OnTransition;
        EventManager.DifficultyEvent += SetDifficulty;
    }

    private void OnTransition(int t)
    {
        // starting the game
        if(t == 2)
        {
            Begin();
        }
        else
        {
            Stop();
        }
    }

	private void Begin(){
		ResetSpawnCount();
		spawning = true;
	}

	private void Stop(){
		spawning = false;
	}

	private void Spawn(){
		// if the spawner is inactive, don't spawn anything
		if(!spawning){
			return;
		}
		int unitID = 0; //TODO: Random sampling from set
		Quaternion rotation = Quaternion.Euler(0, Random.Range(difficulty.angleOffset, difficulty.angleOffset + difficulty.angleRange), 0);
		Vector3 position = rotation * (Vector3.right * difficulty.radius);
		Instantiate(units[unitID], position, Quaternion.identity);
		lastSpawnTime = Time.time;
		totalSpawns++;
	}

    private void ResetSpawnCount()
    {
        lastSpawnTime = 0.0f;
        totalSpawns = 0;
        foreach (GameObject d in Object.FindObjectsOfType<GameObject>())
        {
            if (d.GetComponent<Despawnable>())
            {
                Destroy(d);
            }
        }
    }

    private void SetDifficulty(int d){
        print(d);
		difficulty = difficultyOptions[d];
	}

	private void FixedUpdate(){
		if(totalSpawns + 1 <= difficulty.spawnLimit && Time.time - lastSpawnTime > difficulty.interval){
			Spawn();
		}
	}

	private void OnDisable(){
		EventManager.DifficultyEvent -= SetDifficulty;
	}
}
