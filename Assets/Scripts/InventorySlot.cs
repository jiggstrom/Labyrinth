using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image Image;
    public Button Removebutton;

    Loot item;
	
    public void AddItem(Loot newItem)
    {
        item = newItem;
        Image.sprite = item.InventoryImage;
        Image.enabled = true;
        Removebutton.interactable = true;
    }
	
    public void ClearSlot()
    {
        item = null;
        Image.sprite = null;
        Image.enabled = false;
        Removebutton.interactable = false;
    }

    public void Enabled(bool isEnabled)
    {
        this.gameObject.SetActive(isEnabled);
    }

    public void OnRemovebutton()
    {
        Debug.Log("Remove " + item.name);
        Inventory.instance.RemoveInventoryItem(item);
    }
}
