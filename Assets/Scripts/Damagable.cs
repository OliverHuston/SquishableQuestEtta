using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public Collider hitBoxCollider;

    private PlayerController playerController;

    public int health = 100;
    public int damageResistance = 0;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == hitBoxCollider)
        {
            float resistance = (100f - damageResistance) / 100f;

            int damage = playerController.receiveAttack();
            damage = Mathf.RoundToInt(damage * resistance);

            health -= damage;
;        }
    }


    private void Update()
    {
        //if (health <= 0) Destroy(gameObject);
    }


}
