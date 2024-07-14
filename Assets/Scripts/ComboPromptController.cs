using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboPromptController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject expandingCircle;


    public void UpdateAppearance(float comboScore, bool active, float comboBonusThreshold)
    {
        if (comboScore < 0) active = false;
        expandingCircle.SetActive(active);
        expandingCircle.transform.localScale = new Vector3(comboScore, comboScore, comboScore);
    }

}
