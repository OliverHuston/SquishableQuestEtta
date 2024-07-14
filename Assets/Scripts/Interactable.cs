using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableMode { COLLIDE, KEY_PRESS }
    public InteractableMode interactableMode;

    public float promptDistance = 2;
    public float timeToInteract = 1;

    public enum InteractionResponse { DESTROY_OBJECT, DISABLE_INTERACTABLE, NONE }
    public InteractionResponse interactionResponse;

    public delegate void OnInteract();
    public OnInteract onInteract;

    GameObject playerCapsule;
    public bool interactionAllowed = true;

    private void Awake()
    {
        playerCapsule = FindObjectOfType<PlayerController>().gameObject;
        onInteract += Interact;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!interactionAllowed) return;
        if (collision.collider.gameObject == playerCapsule && interactableMode == InteractableMode.COLLIDE)
        {
            onInteract.Invoke();
        }
    }

    public void Interact()
    {
        if (interactionResponse == InteractionResponse.DESTROY_OBJECT) { Destroy(this.gameObject); }
        else if (interactionResponse == InteractionResponse.DISABLE_INTERACTABLE) { interactionAllowed = false; }
    }
}
