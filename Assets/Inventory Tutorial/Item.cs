using UnityEngine;

public enum ItemType { heal, offensive, defensive, equipment }
public enum Rarity { COMMON, UNCOMMON, RARE }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "Description goes here.";
    public Sprite icon;
    public ItemType type;
    public Rarity rarity;

    public virtual void Use()
    {
        // this function is supposed to be overriden
    }

    public virtual void Drop()
    {
        Inventory.instance.RemoveItem(this);

        // this function is supposed to be overriden
        // if further functionality is needed
    }
}