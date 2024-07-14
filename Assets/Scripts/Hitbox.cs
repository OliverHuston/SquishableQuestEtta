using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hitbox : MonoBehaviour
{
    public Collider collider;

    // Start is called before the first frame update
    void Awake()
    {
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
