using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {
    public int Amount = 1;
    public override void Interact()
    {
        if (!IsCloseEnough()) return;

        base.Interact();

        Pickup();

    }

    private void Pickup()
    {
        //var oldAmount = m_itemDescription.Amount;
        //loot.Amount *= Amount;

        //TODO:
        GameManager.instance.LootFound(m_itemDescription, this, Amount);
        //loot.Amount = oldAmount;
        Destroy(gameObject);
    }
}
