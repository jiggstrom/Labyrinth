using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private LevelData _levelData;
    private List<ZombieManager> zombies;
    private bool InventoryChecked;
    private bool MapChecked;

    // Use this for initialization
    void Start () {
        Inventory.instance.onChanged += () =>
        {
            if (Inventory.instance.items.Any(x => x.name == "Map") && !MapChecked)
            {
                GameManager.instance.ShowHint("Tryck 'M' för att visa/dölja kartan.");
            }
            else if (!InventoryChecked)
            {
                GameManager.instance.ShowHint("Tryck 'E' för att visa/dölja inventory.");
            }
        };
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Map"))
        {
            if (Inventory.instance.items.Any(x => x.name == "Map")) MapChecked = true;
        }
        if (Input.GetButtonDown("Inventory"))
        {
            if (Inventory.instance.items.Count > 1) InventoryChecked = true;
        }
    }
}
