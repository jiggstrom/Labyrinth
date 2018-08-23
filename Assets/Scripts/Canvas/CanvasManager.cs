using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[Serializable]
public class CanvasManager : MonoBehaviour {

    //public GameObject WinningScreen = null;
    public GameObject Inventory;
    public GameObject Minimap;
    public GameObject Crosshair;
    public GameObject LootScreen;
    public bool UIActive = false;
    public FirstPersonController fpc;
    private bool SetCursorLock = false;

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

    public void Update()
    {
        if (SetCursorLock)
        {
            SetCursorLock = false;
            fpc.MouseLook.SetCursorLock(!UIActive);
        }
    }

    internal void ToggleInventory()
    {   
        if (Inventory.activeInHierarchy)
        {
            Inventory.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            UIActive = false;
            SetCursorLock = true; //Update on next frame
        }
        else
        {
            Inventory.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            UIActive = true;
            SetCursorLock = true; //Update on next frame
        }
    }

    internal void ShowLootScreen()
    {
        LootScreen.SetActive(true);
        UIActive = true;
        SetCursorLock = true; //Update on next frame

    }

    internal void CloseLootscreen()
    {
        LootScreen.SetActive(false);
        UIActive = false;
        SetCursorLock = true; //Update on next frame
    }
}
