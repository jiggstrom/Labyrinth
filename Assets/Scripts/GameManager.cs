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

    private InteractableObject currentInteractable;

    public delegate void OnBeginLookAt(InteractableObject obj);
    public OnBeginLookAt onBeginLookAt;

    public delegate void OnStopLookAt(InteractableObject obj);
    public OnStopLookAt onStopLookAt;

    public delegate void OnBeginInteract(InteractableObject obj);
    public OnBeginInteract onBeginInteract;
    public Inventory inventory;

    private int energy;
    private int hunger;
    private int temperatureOffset;
    private PlayerController fpc;
    private bool playerDead = false;

    // Use this for initialization
    void Start () {
        fpc = Player.GetComponent<PlayerController>();
        inventory = Player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Map"))
        {
            Debug.Log("Map pressed");
            if (inventory.PlayerHasItem("Map"))
            {
                Debug.Log("Map found");
                Canvas.ToggleMinimap();
            }
        }
    }



    internal void DisablePlayer(bool value)
    {
        fpc.EnableMovement = !value;
        if (value == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseLootScreen() {
        if (Canvas.LootScreen.activeInHierarchy)
        {
            Canvas.CloseLootscreen();
            LootTaken();
        }

    }

    internal bool LootFound(ItemDescription loot, InteractableObject interactable, int amount = 0)
    {
        currentInteractable = interactable;
        //Canvas.ShowLootScreen();
        //var screen = Canvas.LootScreen.GetComponent<LootScreen>();

        //if (screen != null)
        //{
            if (loot != null)
            {
                ShowHint("Du hittade " + loot.name);
                //screen.Image.texture = Resources.Load<Texture>("Sprites/" + loot.m_sprite); //loot.InventoryImage;

                if (loot.LootType == LootType.Health)
                {
                    var vitals = Player.GetComponent<PlayerVitals>();
                    vitals.GetHealth(amount);
                    return true;
                }
                //else if (loot.LootType == LootType.Energy)
                //{
                //    var vitals = Player.GetComponent<PlayerVitals>();
                //    vitals.GetEnergy(amount);
                //    return true;
                //}
                else if (loot.LootType == LootType.Gold)
                {
                    var vitals = Player.GetComponent<PlayerVitals>();
                    vitals.GetGold(amount);
                    return true;
                }
                else
                {
                    inventory.Put(loot);
                    return true;
                }
            }
            else
            {
                ShowHint("Du hittade ingenting");
                //screen.Image.texture = null;
                return false;
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

    public void ShowMessage(string text, Texture image)
    {
        Canvas.ShowLootScreen();
        var screen = Canvas.LootScreen.GetComponent<LootScreen>();
        if (screen != null)
        {
            screen.Text.text = text;
            if (image != null)
            {
                screen.Image.texture = image;
            }
            else
            {
                screen.Image.texture = null;
            }
        }
    }

    public void ShowHint(string text)
    {
        Canvas.ShowHint(text);
    }
    public void HideHint()
    {
        Canvas.HideHint();
    }
    public void PlayerDeath()
    {
        if (!playerDead)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            LevelLoadManager.instance.Greet("Tyvärr klarade du inte att överleva ensam i den bortglömda gruvan. Försök igen.","Meny");
            Debug.Log("Player death");
            playerDead = true;
        }
    }

    public void LookAt(InteractableObject i)
    {
        if (onBeginLookAt != null) onBeginLookAt.Invoke(i);
    }
    public void StopLookAt(InteractableObject i)
    {
        if (onStopLookAt != null) onStopLookAt.Invoke(i);
    }
    public void InteractWith(InteractableObject i)
    {
        if (onBeginInteract != null) onBeginInteract.Invoke(i);
    }
}
