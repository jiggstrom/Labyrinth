using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[Serializable]
public class CanvasManager : MonoBehaviour
{

    //public GameObject WinningScreen = null;
    public GameObject Inventory;
    public GameObject Minimap;
    public GameObject Crosshair;
    public GameObject LootScreen;
    public GameObject HintMsg;
    private bool _UIActive = false;
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
    }

    internal void ToggleInventory()
    {
        if (Inventory.activeInHierarchy)
        {
            Inventory.SetActive(false);            
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

    public bool UIActive
    {
        get { return _UIActive; }
        set

        {
                GameManager.instance.DisablePlayer(value);
                _UIActive = value;
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

    internal void ShowHint(string text)
    {
        var textObj = HintMsg.GetComponentInChildren<TMPro.TMP_Text>();
        textObj.text = text;
        HintMsg.SetActive(true);
    }
    internal void HideHint()
    {
        HintMsg.SetActive(false);
    }
}
