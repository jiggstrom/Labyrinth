using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public CanvasManager Canvas;

    private Interactable currentInteractable;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            if (Canvas.Minimap.activeInHierarchy)
                Canvas.Minimap.SetActive(false);
            else
                Canvas.Minimap.SetActive(true);
        }
        //if (Input.GetButtonDown("Inventory"))
        //{
        //    if (Inventory.activeInHierarchy)
        //        Inventory.SetActive(false);
        //    else
        //        Inventory.SetActive(true);
        //}
        if (Input.GetButtonDown("Cancel"))
        {
            if (Canvas.LootScreen.activeInHierarchy)
            {
                Canvas.LootScreen.SetActive(false);
                LootTaken();
            }
        }

    }

    internal void LootFound(string loot, Interactable interactable)
    {
        currentInteractable = interactable;
        Canvas.LootScreen.SetActive(true);
    }

    internal void LootTaken()
    {
        currentInteractable.StopInteracting();
        currentInteractable = null;
    }
}
