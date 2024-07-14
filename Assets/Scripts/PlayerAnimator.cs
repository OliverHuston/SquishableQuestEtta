using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAnimator : MonoBehaviour
{
    public GameObject hitBoxObject;

    private Animator animator;
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponentInChildren<Animator>();
        playerController = FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude;

        animator.SetFloat("speed", speed);

        
    }

    public void TriggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
        //else { animator.ResetTrigger(trigger); }
    }

    public void ClearTrigger(string trigger)
    {
        animator.ResetTrigger(trigger);
    }

    public float CurrentAnimationProgress()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //return 0f;
    }

    public bool CurrentAnimationIs(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }



}
