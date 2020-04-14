using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitableRock : HitableObject {
    public int m_hits = 4;
    public GameObject m_fragmentPrefab;
    public override void HandleHit(ItemDescription toolType)
    {
        if (effectiveTools.Contains(toolType))
            if (m_audioSource)
                m_audioSource.Play();

        m_hits -= toolType.ToolHitValue;

        if (m_hits <= 0)
        {// instantiate two stone fragments and destroy self
            Instantiate(m_fragmentPrefab, transform.position, transform.rotation);
            Instantiate(m_fragmentPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
