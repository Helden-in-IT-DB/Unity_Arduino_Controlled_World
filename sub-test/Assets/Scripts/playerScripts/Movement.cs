//functionlist:
//MyInput
//MovePlayer
//SpeedControl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("references")]
    //climb script
    public ClaspClimb CmS;
    public TextMeshProUGUI stateTeller;
    public Collider coll;
    private string oldName;

    [Header("slope handling")]
    public float maxSlopeAngle;
    private RaycastHit slopehit;
    private bool exitingSlope;

    //speed and drag for movement
    [Header("Moverment Spped")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float climbSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float SlopeIncreaseMultiplier;

    public float groundDrag;
   
   [Header("jumping")]
   public float jumpForce;
   public float jumpCooldown;
   public float airMultiplier;
   bool readyToJump;
   bool jumping;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    
    //public float startYscale;
    private Vector3 startScale;
    private Vector3 crouchScale;
    private float lerpSpeed = 1f;


    //groundcheck
    [Header("GroundCheck")]
    public float playerheight;
    public LayerMask whatIsGround;
    public bool grounded;
    
    //movement
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    public Vector3 movementInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    //state handler
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        climbing,
        clasping,
        air
    }

    public bool sliding;
    public bool climbing;
    public bool clasping;
    private bool crouching;
    private bool sprinting;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        //startYscale = transform.localScale.y;
        startScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
        crouchScale = new Vector3 (transform.localScale.x, crouchYScale, transform.localScale.z);
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatIsGround);
        //calls the input for movement
        MyInput();
        SpeedControl();
        StateHandler();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        stateTeller.text = $"{state}";
    }
    private void FixedUpdate()
    {
        MovePlayer(); 
    }

    private void MyInput()
    {
        if (jumping) OnJump();
    }
    private void OnCrouch()
    {
        //when to crouch
        if(!crouching)
        {
            crouching = true;  
            if (grounded && !OnSlope())
            {
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            transform.localScale = Vector3.Lerp(crouchScale, startScale, lerpSpeed * Time.deltaTime);
        }
        else if(crouching)
        {
            crouching = false;
            Debug.Log(message: "return from crouch");
            transform.localScale = Vector3.Lerp(startScale, crouchScale, lerpSpeed * Time.deltaTime / 2);
        }
    }
    private void OnSprint()
    {
        if (!sprinting) sprinting = true;
        else if (sprinting) sprinting = false;
    }
    private void StateHandler()
    {
        switch (true)
        {
            //mode - clasping
            case bool _ when clasping:
                state = MovementState.clasping;
                break;
                
            //mode - climbing
            case bool _ when climbing:
                state = MovementState.climbing;
                desiredMoveSpeed = climbSpeed;
                break;

            //mode - sliding
            case bool _ when sliding:
                state = MovementState.sliding;

                if (OnSlope() && rb.velocity.y < 0.1f)
                {
                    desiredMoveSpeed = slideSpeed;
                }
                else
                {
                    desiredMoveSpeed = sprintSpeed;
                }
                break;

            //sprint mode
            case bool _ when grounded && sprinting && !crouching:
                state = MovementState.sprinting;
                desiredMoveSpeed = sprintSpeed;
                break;
                
            //crouch mode
            case bool _ when crouching:
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
                break;

            //walk mode
            case bool _ when grounded:
                state = MovementState.walking;
                desiredMoveSpeed = walkSpeed;
                break;

            //air mode
            default:

                state = MovementState.air;
                break;
        }

        // check if the desiredMovespeed has changed drastically
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4 && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }
        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    //lerp for momentum
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerps movementspeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopehit.normal);
                float SlopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * SlopeIncreaseMultiplier * SlopeAngleIncrease;
            }
            else 
            {
                time += Time.deltaTime * speedIncreaseMultiplier;
            }
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }
    // like for lerp reference : https://docs.unity3d.com/ScriptReference/Mathf.Lerp.html
    
        private void MovePlayer()
    {

        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        //on slope
        if(OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0 && !exitingSlope)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        //on ground
        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            //turns gravity off when on slope so you don't slide off
            rb.useGravity = !OnSlope();
    }
    
    private void OnMove(InputValue inputValue)
    { 
        movementInput = inputValue.Get<Vector3>();
    }

    private void SpeedControl()
    {
        if(OnSlope())
        {
            //limits speed on slopes
            if(rb.velocity.magnitude> moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        //limits speed on ground
        else
        {
            Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //speed limit
            if (flatvel.magnitude >= moveSpeed)
            {
                Vector3 limitedVel = flatvel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

    }
    private void OnJump()
    {
        //when to jump
        if(readyToJump && grounded)
        {
            readyToJump = false;
            exitingSlope = true;
            jumping = true;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump),jumpCooldown);
        }

    }
    private void OnReleaseJump()
    {
        jumping = false;
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopehit, playerheight * 0.5f + 0.4f, whatIsGround))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle <= maxSlopeAngle && angle != 0;
        }
        else
        {
            return false;
        }
    }
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopehit.normal).normalized;
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.layer != whatIsGround) && collision.gameObject.name != oldName)
        {
            Debug.Log(collision.gameObject.name);
            oldName = collision.gameObject.name;
        }
    }
    */
    private void Onswitch()
    {
        movementInput = Vector3.zero;
    }
}
