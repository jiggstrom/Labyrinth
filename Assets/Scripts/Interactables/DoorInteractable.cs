using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorInteractable : InteractableObject {
    public GameObject Door;
    public ItemDescription[] ItemsNeeded;
    public int GoldNeeded = 0;
    private bool isOpen = false;
    public Texture goldImage;
    public delegate void OnOpened();
    public OnOpened onOpened;

    public override void Interact()
    {
        Debug.Log("Interacting with door");
        base.Interact();

        if(isOpen)
        {
            base.StopInteracting();
            isOpen = false;
            Door.GetComponent<Animator>().Play("Close");
            Door.GetComponent<AudioSource>().Play();
        }
        else
        {
            var gm = FindObjectOfType<GameManager>();
            if(ItemsNeeded.Length > 0)
            {

                var inventory = gm.inventory;
                if (inventory != null)
                {
                    foreach (var item in ItemsNeeded)
                    {
                        //TODO:
                        if (!inventory.PlayerHasItem(item.name))
                        {
                            Debug.Log("Missing: " + item.name);
                            gm.ShowMessage("Du behöver " + item.name + " för att öppna dörren.", Resources.Load<Texture>("Sprites/" + item.m_sprite));
                            return;
                        }
                    }
                    foreach (var item in ItemsNeeded)
                    {
                        if (inventory.PlayerHasItem(item.name)) inventory.Remove(item.name);
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

            Debug.Log("Door opens!");

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
