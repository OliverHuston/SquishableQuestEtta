using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType {ITEM, LORE_ENTRY}
    public ScriptableObject pickup;

    public PickupType pickupType;

    Interactable interactable;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract += PickUp;
    }


    private void PickUp()
    {
        if (pickupType == PickupType.ITEM)
        {
            Inventory.instance.AddItem((Item)pickup);
            NewItemNotifier.instance.NotifyOfNewItem((Item)pickup);
        }
        else if (pickupType == PickupType.LORE_ENTRY)
        {
            LorebookManager.instance.AcquireEntry((Entry)pickup);
            NewItemNotifier.instance.NotifyOfNewLoreEntry((Entry)pickup);
        }

        interactable.onInteract -= PickUp;
    }


}
