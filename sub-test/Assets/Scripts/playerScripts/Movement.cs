//functionlist:
//MyInput
//MovePlayer
//SpeedControl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    //speed and drag for movement
    [Header("Moverment")]
    public float moveSpeed;
    public float groundDrag;
   
   //jump
   public float jumpForce;
   public float jumpCooldown;
   public float airMultiplier;
   bool readyToJump;

   [Header("Keybinds")]
    KeyCode jumpKey = KeyCode.Space; 
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


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatIsGround);
        //calls the input for movement
        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

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
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        //on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //speed limit
        if (flatvel.magnitude >= moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }   
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
