using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerController playerController;
    private CanvasController canvasController;
    private MouseLook[] mouseLookControllers;

    private enum InputMode {Movement = 0, Inventory = 1 }
    InputMode inputMode = InputMode.Movement;

    bool mouse1ClickStatus;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        canvasController = FindObjectOfType<CanvasController>();
        mouseLookControllers = FindObjectsOfType<MouseLook>();


        
    }

    private void FixedUpdate()
    {
        // Get input values
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);
        bool sprint = Input.GetKey(KeyCode.Q);
        bool interact = Input.GetKey(KeyCode.E);

        bool attack = mouse1ClickStatus;    
        mouse1ClickStatus = Input.GetMouseButtonDown(0);


        if (canvasController.Inventory.activeInHierarchy) { inputMode = InputMode.Inventory; }
        else { inputMode = InputMode.Movement; }

        if (inputMode == InputMode.Movement)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerController.ForwardInput = vertical;
            playerController.StrafeInput = horizontal;
            playerController.JumpInput = jump;
            playerController.Sprint = sprint;
            playerController.InteractInput = interact;
            playerController.AttackInput = attack;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            playerController.ForwardInput = 0;
            playerController.StrafeInput = 0;
            playerController.JumpInput = false;
            playerController.InteractInput = false;
        }
    }


    private void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButtonDown(0)) mouse1ClickStatus = true;
        

        foreach (MouseLook m in mouseLookControllers)
        {
            if (inputMode == InputMode.Movement)
            {
                m.MouseX = MouseX;
                m.MouseY = MouseY;
            }
            else
            {
                m.MouseX = 0;
                m.MouseY = 0;
            }
        }

        bool openInventory = Input.GetKeyDown(KeyCode.F1);
        
        
        canvasController.f1 = openInventory;
    }
}
