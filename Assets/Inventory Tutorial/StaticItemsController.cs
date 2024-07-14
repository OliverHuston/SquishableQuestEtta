using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StaticItemsController : MonoBehaviour
{
    public List<TextMeshProUGUI> fields;

    private void Start()
    {
        StatusManager.instance.onStatusChangedCallback += UpdateFields;

    }

    void UpdateFields()
    {
        Type fieldsType = typeof(CharacterStatus);

        foreach (TextMeshProUGUI field in fields)
        {
            string value = fieldsType.GetField(field.name).GetValue(StatusManager.instance.playerStatus).ToString();
            
            field.text = value;
        }
    }
}