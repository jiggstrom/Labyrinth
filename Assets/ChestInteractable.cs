﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    public GameObject Lid;
    public GameObject Canvas;

    public override void Interact()
    {
        base.Interact();
        Lid.GetComponent<Animator>().Play("Opening");
        var gm = FindObjectOfType<GameManager>();
        gm.LootFound("Coins",this);

    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        Lid.GetComponent<Animator>().Play("Closing");
    }
}

