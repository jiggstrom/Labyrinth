﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : InteractableObject
{
    public GameObject Lid;
    public Loot loot;

    public override void Interact()
    {
        //if (!IsCloseEnough()) return;
        base.Interact();
        Lid.GetComponent<Animator>().Play("Opening");
        var gm = FindObjectOfType<GameManager>();
        if (loot != null)
        {
            if(gm.LootFound(loot, this)) loot = null;
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        Lid.GetComponent<Animator>().Play("Closing");
    }
}

