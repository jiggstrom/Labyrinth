using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region "Singelton"
    public static Inventory instance;
    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple instances of inventory found!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public List<Loot> items;
    public int maxItems = 20;

    public delegate void OnChanged();
    public OnChanged onChanged;

    public bool AddLoot(Loot loot)
    {
        if(items.Count >= maxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        items.Add(loot);

        if(onChanged != null) onChanged.Invoke();
        return true;
    }

    public void RemoveInventoryItem(Loot loot)
    {
        items.Remove(loot);
        if (onChanged != null) onChanged.Invoke();
    }
}
