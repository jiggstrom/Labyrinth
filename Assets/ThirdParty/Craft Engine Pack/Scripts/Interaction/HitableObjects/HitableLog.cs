using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitableLog : HitableObject {

    public int m_hits = 12;//like a health of log
    public uint m_stickCount = 5;//drop count
    public GameObject m_stickPrefab;

    public override void HandleHit(ItemDescription toolType)
    {
        if (effectiveTools.Contains(toolType))
            if (m_audioSource)
                m_audioSource.Play();


        m_hits -= toolType.ToolHitValue;

        if (m_hits <= 0)
        {
            for (int i = 0; i < m_stickCount; ++i) // instantiate sticks
            {
                Vector2 randomCircle = Random.insideUnitCircle;
                Instantiate(m_stickPrefab, transform.position + new Vector3(randomCircle.x, 0.0f, randomCircle.y), transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
