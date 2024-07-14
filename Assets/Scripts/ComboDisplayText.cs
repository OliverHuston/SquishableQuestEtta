using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComboDisplayText : MonoBehaviour
{

    PlayerController playerController;
    TextMeshProUGUI tm;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        tm = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int mult = playerController.ComboMultiplier;
        if(mult > 0) { tm.text = "x" + mult; }
        else { tm.text = ""; }
    }

}
