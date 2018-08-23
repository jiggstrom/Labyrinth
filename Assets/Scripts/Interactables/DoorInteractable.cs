using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable {
    public GameObject Door;
    public Loot[] ItemsNeeded;
    public int GoldNeeded = 0;
    private bool isOpen = false;
    public override void Interact()
    {
        base.Interact();

        if(isOpen)
        {
            base.StopInteracting();
            Door.GetComponent<Animator>().Play("Close");
        }
        else
        {
            var gm = FindObjectOfType<GameManager>();
            if(ItemsNeeded.Length > 0)
            {
                var inventory = gm.GetComponent<Inventory>();
                if (inventory != null)
                {
                    foreach (var item in ItemsNeeded)
                    {
                        if (!inventory.items.Contains(item))
                        {
                            Debug.Log("Missing: " + item.Description);
                            gm.ShowMessage("You need " + item.Description + " before you can open this door.", item.InventoryImage);
                            return;
                        }
                    }
                    foreach (var item in ItemsNeeded)
                    {
                        if (!inventory.items.Contains(item)) inventory.items.Remove(item);
                    }
                }
                else
                    return;
            }
            if(GoldNeeded > 0)
            {
                var vitals = gm.Player.GetComponent<PlayerVitals>();
                if(vitals != null)
                {
                    var amount = 0;
                    int.TryParse(vitals.goldAmountText.text,out amount);
                    if(amount >= GoldNeeded)
                    {
                        vitals.GetGold(-GoldNeeded);
                    }
                    else
                    {
                        Debug.Log("Gold needed " + GoldNeeded.ToString() + ", gold availible: " + amount.ToString());
                        return;
                    }
                }
            }

            Door.GetComponent<Animator>().Play("Open");
            
        }
    }

    public override void StopInteracting()
    {
    }
}
