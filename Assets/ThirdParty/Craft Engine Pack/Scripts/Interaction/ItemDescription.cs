﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDescription : ScriptableObject
{
    public bool m_isLiftable = true;//can we lift it and put in backpack?
    public bool m_isInteractable = false;//can we interact with it?
    public bool m_isVisibleInHands = true;// can we hold it in hands?
    public LootType LootType;
    public string m_name;
    public string m_prefab;
    public string m_sprite;
    public int m_maxCount;// max count of stack. 1 - > non-stackable
    public string InteractVerb;
    public ItemDescription[] m_receiptItems;
    public int ToolHitValue = 1;
    public bool Equals(ItemDescription item)
    {
        return item.m_name == m_name;
    }
}
