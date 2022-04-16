using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    public bool activeDefault;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.TransitionEvent += OnTransition;
        shopUI.SetActive(activeDefault);
    }

    private void OnTransition(int t)
    {
        if(t == 2)
        {
            shopUI.SetActive(true);
        }
        else
        {
            shopUI.SetActive(false);
        }
    }

    private void OnDisable()
    {
        EventManager.TransitionEvent -= OnTransition;
    }
}
