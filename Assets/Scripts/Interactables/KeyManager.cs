using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {
    public Camera cam;
    public GameObject crosshair;

    private Interactable focus = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //If we press right mouse
        if (Input.GetButtonDown("Interact"))
        {
            if (GameManager.instance.Canvas.UIActive == false)
            {
                // We create a ray
                Ray ray = cam.ScreenPointToRay(crosshair.transform.position);
                RaycastHit hit;

                // If the ray hits
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
            else
            {
                //GameManager.instance.
            }
        }
    }
}
