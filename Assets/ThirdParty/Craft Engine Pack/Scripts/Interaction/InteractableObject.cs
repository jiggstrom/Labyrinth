using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void Start()
    {
        Init();
    }

    private Material _outlineMaterial;

    private const string OutlineWidthKey = "_Outline";
    private float OutlineWidthValue = 1.5f;

    protected void Init()
    {
        Renderer renderer1 = GetComponentInChildren<Renderer>();
        Material[] materials = renderer1?.materials;

        _outlineMaterial = materials.FirstOrDefault(m => m.shader.name == "Outlined/Silhouette");
        if (_outlineMaterial != null)
        {
            OutlineWidthValue = OutlineWidthValue * renderer1.transform.localScale.magnitude;

            _outlineMaterial.SetFloat(OutlineWidthKey, 0);
        }
    }

    // Shows the outline by setting the width to be a fixed avalue when we are 
    // pointing at it.
    public void ShowOutlineMaterial()
    {
        if (_outlineMaterial != null)
        {
            _outlineMaterial.SetFloat(OutlineWidthKey, OutlineWidthValue);
        }
    }

    // Hides the outline by making the width 0 when we are no longer 
    // pointing at it.
    public void HideOutlineMaterial()
    {
        Debug.Log("Hide outline");
        if (_outlineMaterial != null)
            _outlineMaterial.SetFloat(OutlineWidthKey, 0);
    }
}
