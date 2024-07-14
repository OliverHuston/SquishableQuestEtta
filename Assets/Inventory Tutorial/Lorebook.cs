using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Lore/Lorebook", order = 1)]

public class Lorebook : ScriptableObject
{
    new public string name = "New Item";

    [TextAreaAttribute(1, 3)]
    public string description = "Description goes here.";
    public List<Entry> entries = new List<Entry>();
    

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