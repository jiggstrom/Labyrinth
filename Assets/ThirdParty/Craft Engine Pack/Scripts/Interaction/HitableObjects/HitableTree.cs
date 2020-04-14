using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitableTree : HitableObject {

    public int m_hits = 100; 
    public int m_axeHitValue = 10;
    public int m_stoneFragmentHitValue = 5;

    public GameObject m_woodSpawns;
    public GameObject m_stickSpawns;

    public GameObject m_woodPrefab;
    public GameObject m_stickPrefab;

    public override void HandleHit(ItemDescription toolType)
    {
        if (effectiveTools.Contains(toolType))
            if (m_audioSource)
                m_audioSource.Play();


        m_hits -= toolType.ToolHitValue;

        if (m_hits <= 0)
        {
            //instantiate logs and sticks
            for (int i = 0; i < m_woodSpawns.transform.childCount; ++i)
                Instantiate(m_woodPrefab, m_woodSpawns.transform.GetChild(i).position, m_woodSpawns.transform.GetChild(i).rotation);
            for (int i = 0; i < m_stickSpawns.transform.childCount; ++i)
                Instantiate(m_stickPrefab, m_stickSpawns.transform.GetChild(i).position, m_stickSpawns.transform.GetChild(i).rotation);
            Destroy(gameObject);
        }
    }
}
