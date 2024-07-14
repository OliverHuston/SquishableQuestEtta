using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnInteract : MonoBehaviour
{
    public string animatorTrigger;

    Interactable interactable;
    Animator animator;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract += PlayAnimation;

        animator = GetComponentInChildren<Animator>();
    }


    private void PlayAnimation() {
        animator.SetTrigger("interact");
    }
}
