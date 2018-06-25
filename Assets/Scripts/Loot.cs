using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public LootType LootType;
    public new string name;
    public string Description;
    public Sprite InventoryImage;
    public int Amount;
}