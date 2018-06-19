using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    public GameObject Lid;
    public GameObject Canvas;
    public Loot loot;

    public override void Interact()
    {
        base.Interact();
        Lid.GetComponent<Animator>().Play("Opening");
        var gm = FindObjectOfType<GameManager>();
        if (loot != null)
        {
            gm.LootFound(loot, this);
            loot = null;
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        Lid.GetComponent<Animator>().Play("Closing");
    }
}

