using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            Destroy(gameObject);
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
        if (Input.GetButtonDown("Map"))
        {
            if (Inventory.instance.items.Any(x => x.name == "Map"))
                Canvas.ToggleMinimap();
        }
        if (Input.GetButtonDown("Inventory"))
        {
            Canvas.ToggleInventory();
        }
        if (Input.GetButtonUp("Cancel"))
        {
            CloseLootScreen();
        }
        //fpc.MouseLookEnabled = !Canvas.UIActive;
    }

    public void CloseLootScreen() {
        if (Canvas.LootScreen.activeInHierarchy)
        {
            Canvas.CloseLootscreen();
            LootTaken();
            //fpc.MouseLookEnabled = true;
        }
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

                if (loot.LootType == LootType.Health)
                {
                    var vitals = Player.GetComponent<PlayerVitals>();
                    vitals.GetHealth(loot.Amount);
                    return true;
                }
                else if (loot.LootType == LootType.Energy)
                {
                    var vitals = Player.GetComponent<PlayerVitals>();
                    vitals.GetEnergy(loot.Amount);
                    return true;
                }
                else if (loot.LootType == LootType.Gold)
                {
                    var vitals = Player.GetComponent<PlayerVitals>();
                    vitals.GetGold(loot.Amount);
                    return true;
                }
                else
                {
                    return Inventory.instance.AddLoot(loot);
                }
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

    public void ShowMessage(string text, Sprite image)
    {
        Canvas.ShowLootScreen();
        var screen = Canvas.LootScreen.GetComponent<LootScreen>();
        if (screen != null)
        {
            screen.Text.text = text;
            if (image != null)
            {
                screen.Image.sprite = image;
            }
            else
            {
                screen.Image.sprite = null;
            }
        }
    }

    public void ShowHint(string text)
    {
        Canvas.ShowHint(text);
    }

    public void PlayerDeath()
    {
        if (!playerDead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            LevelLoadManager.instance.FadeToLevel("Meny");
            Debug.Log("Player death");
            playerDead = true;
        }
    }
}
