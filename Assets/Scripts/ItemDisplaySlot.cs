using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplaySlot : MonoBehaviour
{
    public Item item;

    public Image border;
    public Image background;
    public Image image;

    public Color commonColor;
    public Color uncommonColor;
    public Color rareColor;


    void Start()
    {
        
    }


    void FixedUpdate()
    {
        if (item == null) return;

        background.color = ItemRarityColor();
        image.sprite = item.icon;
    }


    private Color ItemRarityColor() {
        if (item.rarity == Rarity.COMMON) { return commonColor; }
        else if (item.rarity == Rarity.UNCOMMON) { return uncommonColor; }
        else if (item.rarity == Rarity.RARE) { return rareColor; }
        return new Color(255, 0, 0);
    }

    public void EquipItem()
    {
        if (item.type != ItemType.equipment) return;
        EquipmentManager.instance.Equip((Equipment)item);
    }
}
