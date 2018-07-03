using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    #region "Singelton"
    public static GameManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of gamemanager found!");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject Player;
    public CanvasManager Canvas;

    private Interactable currentInteractable;

    private int energy;
    private int hunger;
    private int temperatureOffset;
    private FirstPersonController fpc;
    private bool playerDead = false;

    // Use this for initialization
    void Start () {
        fpc = Player.GetComponent<FirstPersonController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Canvas.UIActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            Canvas.ToggleMinimap();
        }
        if (Input.GetButtonDown("Inventory"))
        {
            Canvas.ToggleInventory();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (Canvas.LootScreen.activeInHierarchy)
            {
                Canvas.CloseLootscreen();                
                LootTaken();
            }
        }
        fpc.MouseLookEnabled = !Canvas.UIActive;
    }

    internal void RemoveInventoryItem(Loot loot)
    {
        Inventory.instance.RemoveInventoryItem(loot);
    }

    internal bool LootFound(Loot loot, Interactable interactable)
    {
        currentInteractable = interactable;
        Canvas.ShowLootScreen();
        var screen = Canvas.LootScreen.GetComponent<LootScreen>();
        if(screen != null)
        {
            if (loot != null)
            {
                screen.Text.text = "Du hittade " + loot.name;
                screen.Image.sprite = loot.InventoryImage;
                return Inventory.instance.AddLoot(loot);
            }
            else
            {
                screen.Text.text = "Du hittade ingenting";
                screen.Image.sprite = null;
                return false;
            }
        }
        return false;
    }

    internal void LootTaken()
    {
        if (currentInteractable != null)
        {
            currentInteractable.StopInteracting();
            currentInteractable = null;
        }
    }
    public void PlayerDeath()
    {
        if (!playerDead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            var playcam = Player.GetComponentInChildren<Camera>();
            Debug.Log("Player death");
            playerDead = true;
        }
    }
}
