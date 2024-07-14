using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{

    private Animator animator;
    private Rigidbody rigidbody;
    public NavMeshAgent navMeshAgent;
    private Damagable damagable;

    Transform player;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject.GetComponent<Transform>();
        damagable = GetComponent<Damagable>();
}

    // Update is called once per frame
    void Update()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude;
        animator.SetFloat("speed", speed);

        //navMeshAgent.SetDestination(player.position);


        rigidbody.velocity = Vector3.zero;
        if (damagable.health > 0) { 
            
            rigidbody.velocity += transform.forward * 1f;

            int damping = 2;


            var lookPos = player.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }



    }
}
