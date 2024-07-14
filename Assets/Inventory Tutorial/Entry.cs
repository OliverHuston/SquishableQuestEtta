using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Lore/Entry", order = 2)]

public class Entry : ScriptableObject
{
    new public string name = "New Item";

    public bool acquired = false;
    public bool unread = true;

    public string displayedTitle;

    [TextAreaAttribute (30,30)]
    public string text = "Text goes here.";


    public virtual void Use()
    {
        // this function is supposed to be overriden
    }

    public virtual void Drop()
    {
        //Inventory.instance.RemoveItem(this);

        // this function is supposed to be overriden
        // if further functionality is needed
    }
}