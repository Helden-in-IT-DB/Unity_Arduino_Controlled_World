using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaspClimb : MonoBehaviour
{
    [Header("references")]
    public Transform orientation;
    public Movement pm;
    public Rigidbody rb;
    public LayerMask whatIsGround;
    
    [Header("UI")]
    public Image Meter;

    [Header("climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    public float climbTimer;

    private bool climbing;

    [Header("clasping")]
    public float claspTimerSlowDown;
    public float claspSpeed;
    private bool clasping;

    
    [Header("ClimbJumping")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;

    public int climbJumps;
    private int climbJumpsLeft;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitingWallTime;
    public float exitingWallTimer;


    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    [Header("stamina regen")]
    public float groundRegen;
    public float airRegen;

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode grab = KeyCode.Mouse1;

    void Start()
    {
        climbTimer = maxClimbTime;
    }

    // Update is called once per frame
    void Update()
    {
        Wallcheck();
        StateMachine();
        Meter.fillAmount = climbTimer / maxClimbTime; 
        if (climbing || clasping) ClimbingMovement();
    }
    private void StateMachine()
    {
        switch (true)
        {
            // state 1 - climbing
            case bool _ when wallFront && Input.GetKey(KeyCode.W) && wallLookAngle <= maxWallLookAngle:
                if (!climbing && climbTimer > 0)
                {
                    StartClimbing();
                }

                if (climbTimer > 0) climbTimer -= Time.deltaTime;
                if (climbTimer <= 0) StopClimbing();
                break;
            //state 2 exiting wall
            case bool _ when exitingWall:

            break;
            
            // state 3 - clasping
            case bool _ when Input.GetKey(grab):
                if (climbTimer > 0 && wallFront)
                {
                    StartClasp();
                }
                if (clasping)
                {
                    if (climbTimer > 0) climbTimer -= (Time.deltaTime / claspTimerSlowDown);
                    if (climbTimer <= 0) StopClasping();
                }
                break;
            // state 4 - none
            default:
                if (climbing) StopClimbing();
                if (clasping) StopClasping();
                break;
        }

    }
    private void Wallcheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsGround);
        wallLookAngle = Vector3.Angle(orientation.forward, - frontWallHit.normal);

        
        if (climbTimer < maxClimbTime)
        {
            if (pm.grounded)
            {
                PassiveRegen(groundRegen);
            }
            else if (pm.state == Movement.MovementState.air)
            {
                PassiveRegen(airRegen);
            }
        }
    }

    private void PassiveRegen(float RegenMultiplier)
    {
        climbTimer += (Time.deltaTime / RegenMultiplier);
    }
    private void StartClimbing()
    {
        if (clasping)
        {
            StopClasping();
        }
        climbing = true;
        pm.climbing = true;
    }
    private void ClimbingMovement()
    {
        if (climbing)
        {
            rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        }
        else if (clasping)
        {
            rb.velocity = new Vector3(rb.velocity.x, claspSpeed, rb.velocity.z);  
        }
    }
    private void StopClimbing()
    {
        climbing = false;
        pm.climbing = false;
    }
    private void StartClasp()
    {
        if (climbing)
        {
            StopClimbing();
        }       
        clasping = true;
        pm.clasping = true;
    }
    private void StopClasping()
    { 
        clasping = false;
        pm.clasping = false;
    }
}
