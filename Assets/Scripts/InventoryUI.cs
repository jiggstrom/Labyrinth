using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    private Inventory inventory;
    public Transform Slots;

    private InventorySlot[] slotList;
	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        slotList = Slots.GetComponentsInChildren<InventorySlot>();
        inventory.onChanged += DrawUI;
        DrawUI();
    }

    void DrawUI()
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slotList[i].AddItem(inventory.items[i]);
            }
            else
            {
                slotList[i].ClearSlot();
            }
        }
    }

    public void RemoveInventoryItem(int slotindex, Loot l)
    {
        Inventory.instance.RemoveInventoryItem(l);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
