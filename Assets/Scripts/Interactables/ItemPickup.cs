using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {
    public Loot loot;
    public int Amount = 1;
    public override void Interact()
    {
        if (!IsCloseEnough()) return;

        base.Interact();

        Pickup();

    }

    private void Pickup()
    {
        var oldAmount = loot.Amount;
        loot.Amount *= Amount;

        GameManager.instance.LootFound(loot, this);
        loot.Amount = oldAmount;
        Destroy(gameObject);
    }
}
