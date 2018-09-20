using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public Camera cam;
    public GameObject crosshair;
    public int ViewDistance = 25;
    private Interactable focus = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Canvas.UIActive == false)
        {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(crosshair.transform.position);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, ViewDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        interactable.Interact();
                    }
                    else
                    {
                        if (interactable != focus)
                        {
                            if (focus != null) focus.OnDefocused();
                            focus = interactable.OnFocused();
                        }
                    }
                }
                else
                {
                    if (focus != null)
                    {
                        focus.OnDefocused();
                        focus = null;
                    }
                }
            }
        }
    }
}
