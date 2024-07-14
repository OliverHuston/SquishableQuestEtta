using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   


    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
    public float turnSpeed = 300;
    [Tooltip("Whether the character can jump")]
    public bool allowJump = true;
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed = 4f;
    [Tooltip("Value by which speed is divided when in the air")]
    public float jumpSlowing = 2f;
    [Tooltip("speed of attack animation")]
    [SerializeField] private float _comboTimeStart = .2f;
    [SerializeField] private float _comboTimePeak = .4f;
    [SerializeField] private float _comboTimeEnd = .5f;
    [SerializeField] private float comboBonusThreshold = .8f;
    
    private ComboPromptController comboPrompt;


    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float StrafeInput { get; set; }
    public bool JumpInput { get; set; }
    public bool Sprint { get; set; }
    public bool InteractInput { get; set; }
    public bool AttackInput { get; set; }


    public int ComboMultiplier { get; set; }


    private int numClicks = 0;
    private float lastClickTime = 0;

    private bool attackAvailable;

    new private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerAnimator = GetComponent<PlayerAnimator>();
        comboPrompt = FindObjectOfType<ComboPromptController>();
    }


    private void FixedUpdate()
    {
        CheckGrounded();
        ProcessMovement();
        ProcessInteract();
        ProcessAttack();
    }


    /// <summary>
    /// Checks whether the character is on the ground and updates <see cref="IsGrounded"/>
    /// </summary>
    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    IsGrounded = true;
            }
        }
    }

    /// <summary>
    /// Processes input actions and converts them into movement
    /// </summary>
    private void ProcessMovement()
    {

        // Process Movement/Jumping
        if (IsGrounded)
        {
            // Reset the velocity
            rigidbody.velocity = Vector3.zero;
            // Check if trying to jump
            if (JumpInput && allowJump)
            {
                // Apply an upward velocity to jump
                rigidbody.velocity += Vector3.up * jumpSpeed;
            }

            // Apply a forward or backward velocity based on player input

            

            rigidbody.velocity += transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed;
            rigidbody.velocity -= transform.right * Mathf.Clamp(StrafeInput, -1f, 1f) * moveSpeed;
        }
        else
        {
            // Check if player is trying to change forward/backward movement while jumping/falling
            if (!Mathf.Approximately(ForwardInput, 0f))
            {
                // Override just the forward velocity with player input at half speed
                Vector3 verticalVelocity = Vector3.Project(rigidbody.velocity, Vector3.up);
                rigidbody.velocity = verticalVelocity + transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed / jumpSlowing - transform.right * Mathf.Clamp(StrafeInput, -1f, 1f) * moveSpeed / jumpSlowing;

            }
        }
    }

    private void ProcessInteract() {
        InteractablePromptController.instance.InteractWithHighlighted(InteractInput);
    }

    private void ProcessAttack()
    {
        float timeChange = Time.time - lastClickTime;

        float comboScore = 0;
        if(timeChange < _comboTimePeak) { comboScore = (timeChange - _comboTimeStart) / (_comboTimePeak - _comboTimeStart); }
        else { comboScore = (timeChange - _comboTimeEnd) / (_comboTimePeak - _comboTimeEnd); }

        comboPrompt.UpdateAppearance(comboScore, !playerAnimator.CurrentAnimationIs("Idle"), comboBonusThreshold);

        if (timeChange > _comboTimeEnd)
        {
            numClicks = 0;
            ComboMultiplier = 1;
            attackAvailable = false;
        }
        
        if (AttackInput && numClicks < 3 && timeChange > _comboTimeStart)
        {
            lastClickTime = Time.time;
            numClicks++;


            this.ComboAttack(numClicks, comboScore);
            numClicks = Mathf.Clamp(numClicks, 0, 3);
            attackAvailable = true;
        }
    }

    private void ComboAttack(int num, float comboScore)
    {
        playerAnimator.TriggerAnimation("attack" + num);
        if (num == 1) { ComboMultiplier = 1; }
        else { ComboMultiplier++; }

        if(comboScore > comboBonusThreshold) { ComboMultiplier++; Debug.Log("Bonus!"); }

        Debug.Log("Combo mult: " + ComboMultiplier);
    }



    public int receiveAttack()
    {
        int damage = StatusManager.instance.playerStatus.atk;
        damage = damage * ComboMultiplier;

        

        if(attackAvailable)
        {
            attackAvailable = false;
            Debug.Log("Damage dealt: " + damage);
            return damage;
        }
        return 0;
    }

}
