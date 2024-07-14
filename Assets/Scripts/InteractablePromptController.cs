using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractablePromptController : MonoBehaviour
{
    #region Singleton
    public static InteractablePromptController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public GameObject prompter;
    public Image rotator;

    public bool availableInteraction { set;  get; }

    Interactable highlightedInteractable;

    List<Interactable> interactableObjects;

    GameObject playerCapsule;

    float interactTimer;

    private void Start()
    {
        playerCapsule = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        DetermineHighlighted();

        if (highlightedInteractable != null)
        {
            availableInteraction = true;
            
            Camera cam = CameraManager.instance.mainCamera.GetComponent<Camera>();
            Vector3 screenPos = cam.WorldToScreenPoint(highlightedInteractable.transform.position);

            //Debug.Log(screenPos.x + ", " + screenPos.y + ", " + screenPos.z);

            prompter.transform.position = screenPos;
        }
        else
        {
            availableInteraction = false;
        }

        prompter.SetActive(availableInteraction);
        if (interactTimer == 0) { rotator.gameObject.SetActive(false); }
        else
        {
            rotator.gameObject.SetActive(true);
            rotator.fillAmount = interactTimer / highlightedInteractable.timeToInteract;
        }
        
    }

    void DetermineHighlighted()
    {
        interactableObjects = FindObjectsOfType<Interactable>().ToList();
        //highlightedInteractable = null;
        Interactable bestCandidate = null;
        float bestCandidateDistance = 1000;

        foreach(Interactable g in interactableObjects) {
            //Eliminate invalid candidates
            if (!g.interactionAllowed) continue;
            if (g.interactableMode != Interactable.InteractableMode.KEY_PRESS) continue;
            if (g.gameObject.GetComponent<Renderer>().isVisible == false) continue;

            float distance = Vector3.Distance(g.gameObject.transform.position, playerCapsule.transform.position);
            if (g.promptDistance < distance) continue;

            //Compare candidate
            if(bestCandidate == null) { bestCandidate = g; bestCandidateDistance = distance; }
            else if (distance < bestCandidateDistance) { bestCandidate = g; bestCandidateDistance = distance; }
        }
        if (highlightedInteractable != bestCandidate) { interactTimer = 0; }
        highlightedInteractable = bestCandidate;
    }

    public void InteractWithHighlighted(bool interacting)
    {
        if(availableInteraction && interacting)
        {
            interactTimer += 1f * Time.deltaTime;
            if (interactTimer > highlightedInteractable.timeToInteract) { interactTimer = 0; highlightedInteractable.onInteract.Invoke();  }
        }
        else { interactTimer = 0; }

        //Debug.Log(interactTimer);
    }
}
