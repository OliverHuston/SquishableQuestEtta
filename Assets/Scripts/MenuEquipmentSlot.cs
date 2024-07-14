using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuEquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum SlotType { WEAPON, RELIC, HELMET, ARMOR }
    public SlotType slotType;

    public int Number_per_row = 3;
    public int Max_squares = 9;

    public enum DisplaySide { LEFT, RIGHT }
    public DisplaySide displaySide;

    public GameObject equippedItemDisplaySlot;
    public GameObject equipmentBrowser;

    public GameObject itemDisplaySlotPrefab;

    List<Item> items;


    void Start()
    {
        equipmentBrowser.SetActive(false);
        StatusManager.instance.onStatusChangedCallback += UpdateEquipmentSlotDisplay;
    }


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        equipmentBrowser.SetActive(true);
        UpdateEquipmentSlotDisplay();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        equipmentBrowser.SetActive(false);
    }

    private void UpdateEquipmentSlotDisplay()
    {

        //Equipped Item
        Button b = equippedItemDisplaySlot.GetComponent<Button>();
        b.interactable = false;
        ItemDisplaySlot equippedDisplay = equippedItemDisplaySlot.GetComponent<ItemDisplaySlot>();
        equippedDisplay.item = EquipmentManager.instance.currentEquipment[(int)slotType];

        //Inventory Items
        items = new List<Item>();
        foreach (Item item in Inventory.instance.items)
        {
            if (item.type == ItemType.equipment) {
                Equipment e = (Equipment)item;
                int a = (int)e.equipType;
                if (a == (int)slotType) { items.Add(item); }
            }      
        }

        int sideMult = 0;
        if (displaySide == DisplaySide.LEFT) { sideMult = - 1; }
        else if (displaySide == DisplaySide.RIGHT) { sideMult = 1; }

        int count = 0;
        foreach (Item item in items)
        {
            if (count < Max_squares)
            {
                if (equipmentBrowser.transform.childCount > count)
                {
                    ItemDisplaySlot displaySlot = equipmentBrowser.transform.GetChild(count).GetComponent<ItemDisplaySlot>();
                    displaySlot.item = item;
                }
                else
                {
                    GameObject newPrefab = GameObject.Instantiate(itemDisplaySlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    newPrefab.transform.SetParent(equipmentBrowser.transform);
                    newPrefab.transform.position = equipmentBrowser.transform.position + new Vector3(sideMult * (210 + 210 * (count % Number_per_row)), -210 * Mathf.Floor(count / Number_per_row), 0);
                    ItemDisplaySlot displaySlot = newPrefab.GetComponent<ItemDisplaySlot>();
                    displaySlot.item = item;
                }
            }

            count++;
        }

        //Removing extra display slots
        for (int i = equipmentBrowser.transform.childCount - 1; i > count - 1; i--)
        {
            DestroyImmediate(equipmentBrowser.transform.GetChild(i).gameObject);
        }
    }
}
