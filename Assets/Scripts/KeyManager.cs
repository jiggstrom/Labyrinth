using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {
    public GameObject MiniMap;
    public GameObject Inventory;

    public Camera cam;
    public GameObject crosshair;

    private Interactable focus = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Minimap"))
        {
            if (MiniMap.activeInHierarchy)
                MiniMap.SetActive(false);
            else
                MiniMap.SetActive(true);
        }
        if (Input.GetButtonDown("Inventory"))
        {
            if (Inventory.activeInHierarchy)
                Inventory.SetActive(false);
            else
                Inventory.SetActive(true);
        }

        // If we press right mouse
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    // We create a ray
        //    Ray ray = cam.ScreenPointToRay(crosshair.transform.position);
        //    RaycastHit hit;

        //    // If the ray hits
        //    if (Physics.Raycast(ray, out hit, 100))
        //    {
        //        Interactable interactable = hit.collider.GetComponent<Interactable>();
        //        if (interactable != null)
        //        {
        //            interactable.OnFocused(GameObject.Find("RigidBodyFPSController").transform);
        //        }
        //    }
        //}
    }
}
