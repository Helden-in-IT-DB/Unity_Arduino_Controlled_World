//functionlist:
//MyInput
//MovePlayer
//SpeedControl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public TextMeshProUGUI stateTeller;

    [Header("slope handling")]
    public float maxSlopeAngle;
    private RaycastHit slopehit;
    private bool exitingSlope;

    //speed and drag for movement
    [Header("Moverment")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
   
   //jump
   public float jumpForce;
   public float jumpCooldown;
   public float airMultiplier;
   bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float startYscale;

   //controls
   [Header("Keybinds")]
    KeyCode jumpKey = KeyCode.Space; 
    KeyCode sprintKey = KeyCode.LeftShift;
    KeyCode crouchKey = KeyCode.LeftControl;

    //groundcheck
    [Header("GroundCheck")]
    public float playerheight;
    public LayerMask whatIsGround;
    bool grounded;
    
    //movement
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    //state handler
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        slide,
        air
    }
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        startYscale = transform.localScale.y;
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump),jumpCooldown);
        }

        //when to crouch
        if(Input.GetKey(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            if(!grounded)
            {
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
        }
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
        }
    }
    private void StateHandler()
    {
       
        //sprint mode
        if(grounded && Input.GetKey(sprintKey) && !Input.GetKey(crouchKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        //crouch mode
        else if(Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        //walk mode
        else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed; 
        }
        //air mode
        else
        {
            state = MovementState.air;
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
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
    private void SpeedControl()
    {
        if(OnSlope() && !exitingSlope)
        {
            //limits speed on slopes
            if(rb.velocity.magnitude> moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        //kimits spped on ground
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
    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopehit, playerheight * 0.5f + 0.4f))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle < maxSlopeAngle && angle != 0;
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
}
