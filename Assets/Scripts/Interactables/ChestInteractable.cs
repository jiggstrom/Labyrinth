using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : InteractableObject
{
    public GameObject Lid;
    public ItemDescription loot;
    private bool isOpen = false;
    public override void Interact()
    {
        base.Interact();

        if (!isOpen)
        {
            isOpen = true;
            Lid.GetComponent<Animator>().Play("Opening");
            var gm = FindObjectOfType<GameManager>();
            if(loot != null)
                gm.ShowMessage($"Du hittade {loot.name}", Resources.Load<Texture>("Sprites/" + loot.m_sprite));
            

            if (gm.LootFound(loot, this)) loot = null;
        }
        else
        {
            StopInteracting();
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        var gm = FindObjectOfType<GameManager>();
        gm.CloseLootScreen();
        Lid.GetComponent<Animator>().Play("Closing");
        isOpen = false;
    }
}

