using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

    public override void Interact()
    {
        base.Interact();

        Pickup();

    }

    private void Pickup()
    {
        Debug.Log("Picked up item");
        Destroy(gameObject);
    }
}
