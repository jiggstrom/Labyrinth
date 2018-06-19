using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    private GameManager gm;
    public GameObject Slots;

    private InventorySlot[] slotlist;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        var slotList = Slots.GetComponentsInChildren<InventorySlot>();
        var slotindex = 0;
        foreach(Loot l in gm.inventory)
        {
            var slot = slotList[slotindex];
            slot.Image.sprite = l.InventoryImage;
            slot.Removebutton.onClick.AddListener(()=> RemoveInventoryItem(slotindex, l));
        }
    }

    private void RemoveInventoryItem(int slotindex, Loot l)
    {
        gm.RemoveInventoryItem(l);
        slotlist[slotindex].Image.sprite = null;
        slotlist[slotindex].Removebutton.onClick = null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
