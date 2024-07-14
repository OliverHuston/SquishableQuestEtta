using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentEquipper : MonoBehaviour
{
    public enum SlotType { WEAPON, RELIC, HELMET, ARMOR }
    public SlotType slotType;



    public List<GameObject> children;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            children.Add(transform.GetChild(i).gameObject);
        }
    }

    private void FixedUpdate()
    {
        UpdateEquippedModels();
    }


    void UpdateEquippedModels()
    {
        
        int index = (int)this.slotType;
        if(EquipmentManager.instance.currentEquipment[index] == null) return;

        string mn = EquipmentManager.instance.currentEquipment[index].modelName;

        foreach (GameObject child in children)
        {
                if (child.name == mn) { child.SetActive(true); }
                else { child.SetActive(false); }
        }
    }
}
