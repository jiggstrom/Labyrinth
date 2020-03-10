using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    public GameObject Lid;
    public ItemDescription loot;
    private bool isOpen = false;
    public override void Interact()
    {
        //if (!IsCloseEnough()) return;
        base.Interact();

        if (!isOpen)
        {
            isOpen = true;
            Lid.GetComponent<Animator>().Play("Opening");
            var gm = FindObjectOfType<GameManager>();
            //if (loot != null)
            //{
                if (gm.LootFound(loot, this)) loot = null;
            //}
        }
        else
        {
            StopInteracting();
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        Lid.GetComponent<Animator>().Play("Closing");
        isOpen = false;
    }
}

