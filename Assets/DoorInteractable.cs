using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable {

    public override void Interact()
    {
        base.Interact();
        GetComponent<Animator>().Play("Open");
        var gm = FindObjectOfType<GameManager>();
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        GetComponent<Animator>().Play("Close");
    }
}
