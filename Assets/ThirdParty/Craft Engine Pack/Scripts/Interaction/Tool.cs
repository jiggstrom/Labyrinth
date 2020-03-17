using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Axe,
    Hammer,
    Rock,
    StoneFragment,
    Pickaxe
}

public class Tool : MonoBehaviour {
    public ToolType m_type;
    public AccessViaRayCast m_eyes { get; set; }
    public virtual void Action()
    {
        if (m_eyes.m_objectToHit)
        {
            m_eyes.m_objectToHit.HandleHit(m_type);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        m_eyes?.GetComponent<ToolActionController>()?.HitSomeSuface();
    }
}

