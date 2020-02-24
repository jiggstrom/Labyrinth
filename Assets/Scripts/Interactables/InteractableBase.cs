using System;
using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : InteractableObject
{

    public float radius = 3f;               // How close do we need to be to interact?
    public Transform interactionTransform;  // The transform from where we interact in case you want to offset it

    bool isFocus = false;   // Is this interactable currently being focused?

    bool hasInteracted = false; // Have we already interacted with the object?

    public override void Interact()
    {
        GameManager.instance.InteractWith(this);       
    }

    void Start()
    {
    }

    void Update()
    {
        // If we are currently being focused
        // and we haven't already interacted with the object
        //if (isFocus && !hasInteracted)
        //{
        //    // If we are close enough
        //    float distance = Vector3.Distance(player.position, interactionTransform.position);
        //    if (distance <= radius)
        //    {
        //        // Interact with the object
        //        Interact();
        //        hasInteracted = true;
                
        //    }
        //    else
        //    {
        //        //OnDefocused();
        //    }
        //}
    }

    internal bool IsCloseEnough()
    {
        float distance = Vector3.Distance(GameManager.instance.Player.transform.position, interactionTransform.position);
        return (distance <= radius);
    }

    // Called when the object starts being focused
    public virtual Interactable OnFocused()
    {
        if (IsCloseEnough())
        {
            isFocus = true;
            hasInteracted = false;
            GameManager.instance.LookAt(this);
            return this;
        }

        return null;

    }

    // Called when the object is no longer focused
    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        GameManager.instance.StopLookAt(this);
    }

    // Draw our radius in the editor
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public override void StopInteracting()
    {
        hasInteracted = true;
    }

}