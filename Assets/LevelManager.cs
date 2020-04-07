﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private LevelData _levelData;
    private List<ZombieManager> zombies;
    private bool InventoryChecked;
    private bool MapChecked;
    private bool HasInteracted;
    public DoorInteractable levelExitDoor;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.inventory.OnInventoryChanged += InventoryChanged;
        GameManager.instance.onBeginLookAt += BeginLookAt;
        GameManager.instance.onStopLookAt += StopLookAt;
        GameManager.instance.onBeginInteract += SetHasInteracted;
        levelExitDoor.onOpened += LevelCleared;


        var ps = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        var pl = GameObject.FindGameObjectWithTag("Player").transform;
        //pl.localPosition = ps.localPosition;

    }

    private void LevelCleared()
    {
        var lm = LevelLoadManager.instance;
        lm.Greet(
            "Grattis, du har nu klarat introduktionen och är redo för en ritig utmaning! Välkommen till nästa nivå!", "Meny");
    }

    private void SetHasInteracted(InteractableObject obj)
    {
        HasInteracted = true;
    }

    private void BeginLookAt(InteractableObject x)
    {
        if (!HasInteracted)
            GameManager.instance.ShowHint("Tryck höger musknapp för att interagera med saker.");
        x.ShowOutlineMaterial();

    }
    private void StopLookAt(InteractableObject x)
    {
        if (!HasInteracted)
            GameManager.instance.HideHint();
        x.HideOutlineMaterial();
    }

    private void InventoryChanged()
    {
        if (GameManager.instance.inventory.PlayerHasItem("Map") && !MapChecked)
        {
            GameManager.instance.ShowHint("Tryck 'M' för att visa/dölja kartan.");
        }
        else if (!InventoryChecked)
        {
            GameManager.instance.ShowHint("Tryck 'E' för att visa/dölja inventory.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Map"))
        {
            if (GameManager.instance.inventory.PlayerHasItem("Map")) MapChecked = true;
        }
        if (Input.GetButtonDown("Inventory"))
        {
            if (GameManager.instance.inventory.Itemcount() > 0) InventoryChecked = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.inventory.OnInventoryChanged -= InventoryChanged;
        GameManager.instance.onBeginLookAt -= BeginLookAt;
        GameManager.instance.onStopLookAt -= StopLookAt;
        GameManager.instance.onBeginInteract -= SetHasInteracted;
        levelExitDoor.onOpened -= LevelCleared;

    }
}
