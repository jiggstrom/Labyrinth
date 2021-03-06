﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitableCrate : HitableObject
{
    public int m_hits = 4;//like a health of log
    public uint m_stickCount = 1;//drop count
    public GameObject m_stickPrefab;
    public GameObject DestroyedObjectPrefab;

    public override void HandleHit(ItemDescription toolType)
    {
        if (effectiveTools.Contains(toolType))
            if (m_audioSource)
                m_audioSource.Play();

        m_hits -= toolType.ToolHitValue;

        if (m_hits <= 0)
        {
            // Spawn a shattered object
            if (DestroyedObjectPrefab != null)
            {
                var n = Instantiate(DestroyedObjectPrefab, transform.position, transform.rotation);
                n.GetComponent<ObjectVanish>()?.VanishInSeconds(3);
            }

            for (int i = 0; i < m_stickCount; ++i) // instantiate sticks
            {
                Vector2 randomCircle = Random.insideUnitCircle;
                Instantiate(m_stickPrefab, transform.position + new Vector3(randomCircle.x, 0.0f, randomCircle.y), transform.rotation);
            }
            Destroy(gameObject);
        }
    }

}
