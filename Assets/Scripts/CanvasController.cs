using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject HUD;
    public GameObject Inventory;


    private enum DisplayUI { HUD = 0, Inventory = 1 }
    DisplayUI displayUI = DisplayUI.HUD;

    public bool f1 { get; set; }


    public void Awake()
    {
        HUD.SetActive(true);
        Inventory.SetActive(false);
    }

    private void Update()
    {
        if (f1) { 
            HUD.SetActive(!HUD.activeInHierarchy);
            Inventory.SetActive(!Inventory.activeInHierarchy);
        }
    }


}
