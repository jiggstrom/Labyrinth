using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image Image;
    public Button Removebutton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enabled(bool isEnabled)
    {
        this.gameObject.SetActive(isEnabled);
    }
}
