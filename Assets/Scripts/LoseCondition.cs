using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
	private bool isGameOver = false;

    private void Start()
    {
        EventManager.TransitionEvent += OnTransition;
    }

    private void OnTransition(int t)
    {
        // starting the game
        if(t == 2)
        {
            isGameOver = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string name = collision.gameObject.name;
    	Unit unit = collision.gameObject.GetComponent<Unit>();
    	if(unit && !isGameOver && name.StartsWith("Enemy")){
            EventManager.StartTransitionEvent(3);
    		isGameOver = true;
	    }
    }

    private void OnDisable()
    {
        EventManager.TransitionEvent -= OnTransition;
    }
}
