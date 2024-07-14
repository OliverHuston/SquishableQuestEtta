using UnityEngine;

public enum EquipType { WEAPON, RELIC, HELMET, ARMOR }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public EquipType equipType;
    public string modelName;

    public int spdModifier;
    public int atkModifier;
    public int defModifier;
    public int recModifier;
    public int ptyModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        Inventory.instance.RemoveItem(this);
    }
}