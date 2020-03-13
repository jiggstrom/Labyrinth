using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableCrate : HitableObject
{
    public int m_hits = 4;//like a health of log
    public uint m_stickCount = 1;//drop count
    public GameObject m_stickPrefab;
    public GameObject DestroyedObjectPrefab;

    public override void HandleHit(ToolType toolType)
    {
        Debug.Log($"Hit, tool is {toolType}");
        if (toolType == ToolType.Axe || toolType == ToolType.StoneFragment)
            if (m_audioSource)
                m_audioSource.Play();

        if (toolType == ToolType.Axe)//axe better chops
            m_hits -= 2;
        else if (toolType == ToolType.StoneFragment)
            m_hits -= 1;

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
