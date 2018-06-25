using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CanvasManager : MonoBehaviour {

    //public GameObject WinningScreen = null;
    public GameObject Inventory;
    public GameObject Minimap;
    public GameObject Crosshair;
    public GameObject LootScreen;
    public bool UIActive = false;

    internal void CloseLootscreen()
    {
        LootScreen.SetActive(false);
        UIActive = false;
    }
    internal void ToggleMinimap()
    {
        if (Minimap.activeInHierarchy)
        {
            Minimap.SetActive(false);
        }
        else
        {
            Minimap.SetActive(true);
        }
    }

    internal void ToggleInventory()
    {   
        if (Inventory.activeInHierarchy)
        {
            Inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UIActive = false;
        }
        else
        {
            Inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIActive = true;
        }
    }

    internal void ShowLootScreen()
    {
        LootScreen.SetActive(true);
        UIActive = true;
    }
}
