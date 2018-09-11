using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HideMessage()
    {
        gameObject.SetActive(false);
    }
}
