using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public CanvasManager Canvas;

    private Interactable currentInteractable;

    public List<Loot> inventory;
    private int energy;
    private int hunger;
    private int temperatureOffset;

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
        if (Input.GetButtonDown("Inventory"))
        {
            if (Canvas.Inventory.activeInHierarchy)
                Canvas.Inventory.SetActive(false);
            else
                Canvas.Inventory.SetActive(true);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (Canvas.LootScreen.activeInHierarchy)
            {
                Canvas.LootScreen.SetActive(false);
                LootTaken();
            }
        }
    }

    internal void RemoveInventoryItem(Loot loot)
    {
        inventory.Remove(loot);
    }

    internal void LootFound(Loot loot, Interactable interactable)
    {
        currentInteractable = interactable;
        Canvas.LootScreen.SetActive(true);
        var screen = Canvas.LootScreen.GetComponent<LootScreen>();
        if(screen != null)
        {
            if (loot != null)
            {
                screen.Text.text = "Du hittade " + loot.name;
                screen.Image.sprite = loot.InventoryImage;
                inventory.Add(loot);
            }
            else
            {
                screen.Text.text = "Du hittade ingenting";
                screen.Image.sprite = null;
            }
        }
    }

    internal void LootTaken()
    {
        if (currentInteractable != null)
        {
            currentInteractable.StopInteracting();
            currentInteractable = null;
        }
    }
}
