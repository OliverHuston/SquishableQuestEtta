using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform transformSource;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transformSource.position;
        this.transform.rotation = transformSource.rotation;
    }
}
