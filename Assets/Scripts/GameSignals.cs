using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSignals : MonoBehaviour
{
	public Stopwatch stopwatch;
	public MuteToggle muteToggle;
	public UnitSpawner unitSpawner;

	public GameObject mainMenu;
	public GameObject difficultyMenu;
	public GameObject shopHUD;
	public GameObject gameOverMenu;

	bool isGameOver = false;

	public void Reset(){
		print("Reset");
		isGameOver = false;
		//gameOverMenu.SetActive(false);
		foreach (Unit u in Object.FindObjectsOfType<Unit>()) {
			if(!u.name.Equals("Friendly King Unit")){
            	u.gameObject.SetActive(false);
            }
        }
		stopwatch.Begin();
		mainMenu.SetActive(false);
		gameOverMenu.SetActive(false);
		difficultyMenu.SetActive(false);
		shopHUD.SetActive(true);
		unitSpawner.gameObject.SetActive(true);
		CameraController cam = Camera.main.GetComponent<CameraController>();
		cam.SetMode(2);
		cam.SetLocked(false);
	}

	public void GameOver(){
		// if the game is already lost
		if(isGameOver){
			return;
		}
		isGameOver = true;
		print("Game Over");
		stopwatch.Stop();
		gameOverMenu.SetActive(true);
		unitSpawner.gameObject.SetActive(false);
		shopHUD.SetActive(false);
		CameraController cam = Camera.main.GetComponent<CameraController>();
		cam.SetMode(3);
		cam.SetLocked(true);
	}

	public void ToggleMute(){
		print("ToggleMute");
		muteToggle.Toggle();
	}

	public void SetDifficulty(int d){
		print("SetDifficulty" + d);
		unitSpawner.SetDifficulty(d);
	}

	public void QuitGame(){
		print("Quit");
		Application.Quit();
	}
}
