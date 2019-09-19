using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorInteractable : Interactable {
    public GameObject Door;
    public Loot[] ItemsNeeded;
    public int GoldNeeded = 0;
    private bool isOpen = false;
    public Sprite goldImage;
    public delegate void OnOpened();
    public OnOpened onOpened;

    public override void Interact()
    {
        if (!IsCloseEnough()) return;

        base.Interact();

        if(isOpen)
        {
            //base.StopInteracting();
            isOpen = false;
            Door.GetComponent<Animator>().Play("Close");
            Door.GetComponent<AudioSource>().Play();
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
                        //TODO:
                        //if (!inventory.items.Contains(item))
                        //{
                        //    Debug.Log("Missing: " + item.Description);
                        //    gm.ShowMessage("Du behöver " + item.Description + " för att öppna dörren.", item.InventoryImage);
                        //    return;
                        //}
                    }
                    foreach (var item in ItemsNeeded)
                    {
                        //TODO:
                        //if (!inventory.items.Contains(item)) inventory.items.Remove(item);
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
                        gm.ShowMessage("Det kostar " + GoldNeeded.ToString() + " guld att öppna dörren men du har bara " + amount.ToString(),goldImage );
                        return;
                    }
                }
            }

            isOpen = true;
            Door.GetComponent<Animator>().Play("Open");
            Door.GetComponent<AudioSource>().Play();
            if (onOpened != null) onOpened.Invoke();
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        Door.GetComponent<Animator>().Play("Close");
        Door.GetComponent<AudioSource>().Play();        
    }
}
