using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarManager : MonoBehaviour
{
    private Toggle[] toggles;


    private void Awake()
    {
        toggles = new Toggle[5];

        for (int i = 0; i < 5; i++) {
            toggles[i] = transform.GetChild(i).GetComponent<Toggle>();
        }

    }

    void OnEnable()
    {
        toggles[0].isOn = false;
        toggles[1].isOn = false;
        toggles[3].isOn = false;
        toggles[4].isOn = false;

        toggles[2].isOn = true;
        toggles[2].onValueChanged.Invoke(false);
        
        
    }



}
