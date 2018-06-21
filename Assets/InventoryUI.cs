using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    private GameManager gm;
    public GameObject Slots;

    private InventorySlot[] slotList;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        slotList = Slots.GetComponentsInChildren<InventorySlot>();
        var slotindex = 0;
        foreach(Loot l in gm.inventory)
        {
            var slot = slotList[slotindex];
            slot.Image.sprite = l.InventoryImage;
            slot.Image.enabled = true;
            slot.Removebutton.onClick.AddListener(()=> RemoveInventoryItem(slotindex, l));
            slot.Removebutton.interactable = true;
        }
    }
    void OnEnable()
    {
        var slotindex = 0;
        foreach (Loot l in gm.inventory)
        {
            var slot = slotList[slotindex];
            slot.Image.sprite = l.InventoryImage;
            slot.Image.enabled = true;
            slot.Removebutton.onClick.AddListener(() => RemoveInventoryItem(slotindex, l));
            slot.Removebutton.interactable = true;
        }
    }
    private void RemoveInventoryItem(int slotindex, Loot l)
    {
        gm.RemoveInventoryItem(l);
        var slot = slotList[slotindex];
        slot.Image.sprite = null;
        slot.Image.enabled = false;
        slot.Removebutton.onClick = null;
        slot.Removebutton.interactable = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
