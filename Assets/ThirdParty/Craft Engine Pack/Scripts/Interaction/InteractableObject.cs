using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    public ItemDescription m_itemDescription;
    public int Amount = 1;
    public virtual void Interact() {
        if (m_itemDescription.m_isLiftable) Pickup();
    }
    public virtual void StopInteracting() { }
    internal void Pickup()
    {
        if (GameManager.instance.LootFound(m_itemDescription, this, Amount))
            GameObject.Destroy(gameObject);
        else
        {
            //if not -> drop it!
            gameObject.transform.position = transform.position + transform.forward * 2 + transform.up;
            gameObject.transform.rotation = Quaternion.Euler(Random.insideUnitSphere * 100);
        }
    }
}
