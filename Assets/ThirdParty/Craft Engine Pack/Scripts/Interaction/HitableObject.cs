using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObject : InteractableObject {
    protected AudioSource m_audioSource;
    public ItemDescription[] effectiveTools;
    void Start()
    {
        base.Init();
        m_audioSource = GetComponent<AudioSource>();
    }
    public virtual void HandleHit(ToolType toolType)
    {

    }
}
