using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {
    public Loot loot;

    public override void Interact()
    {
        base.Interact();

        Pickup();

    }

    private void Pickup()
    {
    
        GameManager.instance.LootFound(loot, this);
        Destroy(gameObject);
    }
}
