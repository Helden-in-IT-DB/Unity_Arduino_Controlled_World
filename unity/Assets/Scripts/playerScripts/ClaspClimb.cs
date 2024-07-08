using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaspClimb : MonoBehaviour
{
    [Header("references")]
    public Transform orientation;
    private Movement pm;
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
    public float SideJumpForwardFroce;
    public float pushWayForce;

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
    private RaycastHit rightwallHit;
    private RaycastHit leftWallHit;
    private RaycastHit backWallhit;
    private bool wallFront;
    private bool wallBack;
    private bool wallRight;
    private bool wallLeft;

    private Transform lastWall;
    private Vector3 lastWallNomral;
    public float minWallNormalAngleChange;

    [Header("stamina regen")]
    public float groundRegen;
    public float airRegen;

 
    



    void Start()
    {
        climbTimer = maxClimbTime;
        pm = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Wallcheck();
        StateMachine();
        TimerCheck();
        Meter.fillAmount = climbTimer / maxClimbTime; 
        if ((climbing || clasping) && !exitingWall) ClimbingMovement();
    }

    private void TimerCheck()
    {
        if (exitingWallTimer < 0) exitingWallTimer = 0;
        if (climbTimer < 0) climbTimer = 0;
        else if (climbTimer > maxClimbTime) climbTimer = maxClimbTime;
    }
    private void StateMachine()
    {
        switch (true)
        {
            // state 1 - climbing
            case bool _ when wallFront && (pm.movementInput.y >= 1) && wallLookAngle <= maxWallLookAngle:
                if (!climbing && climbTimer > 0)
                {
                    StartClimbing();
                }

                if (climbTimer > 0) climbTimer -= Time.deltaTime;
                if (climbTimer <= 0) StopClimbing();
                break;
            //state 2 exiting wall
            case bool _ when exitingWall:

                if (climbing) StopClimbing();
                if (clasping) OnReleaseClasp();

                if (exitingWallTimer > 0) exitingWallTimer -= Time.deltaTime;
                if (exitingWallTimer < 0) exitingWall = false;
                break;

            // state 3 - clasping
            case bool _ when clasping:
                if (climbTimer > 0 && wallFront)
                {
                    OnClasp();
                }
                if (clasping)
                {
                    if (climbTimer > 0) climbTimer -= (Time.deltaTime / claspTimerSlowDown);
                    if (climbTimer <= 0) OnReleaseClasp();
                }
                break;
            // state 4 - none
            default:
                if (climbing) StopClimbing();
                //if (clasping) OnReleaseClasp();
                break;
        }
        
        if (wallFront && (pm.movementInput.y <= -1) && clasping) WallRelease();

        /* old claspjump switch case
        //switch for wall exiting
        switch (true)
        {
            case bool _ when !wallFront && Input.GetKeyDown(jumpKey) && clasping:
                Claspjump();
                break;
            // wall jump mode 2 - jump off when clasping and facing wall
            case bool _ when wallFront && Input.GetKeyDown(jumpKey) && clasping:
            // exit mode 1 - get off the wall when when clasping, looking at wall, walking backwards
            case bool _ when wallFront && (pm.movementInput.y <= -1) && clasping:
                WallRelease();
                break;
            // ground / climb jump check to avoid repetition
            case bool _ when pm.grounded || climbJumpsLeft == 0:
                break;
            // wall jump mode 1 - normal wall jump when wall in infront
            case bool _ when wallFront && Input.GetKeyDown(jumpKey) && climbing:
                ClimbBackJump();
                break;

        }
        */

    }
    private void OnJump()
    {
        //switch for wall exiting
        switch (true)
        {
            case bool _ when !wallFront && clasping:
                Claspjump();
                break;
            // wall jump mode 2 - jump off when clasping and facing wall
            case bool _ when wallFront && clasping:
                WallRelease();
                break;
            // ground / climb jump check to avoid repetition
            case bool _ when pm.grounded || climbJumpsLeft == 0:
                break;
            // wall jump mode 1 - normal wall jump when wall in infront
            case bool _ when wallFront && climbing:
                ClimbBackJump();
                break;

        }
    }
    private void Wallcheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsGround);
        wallBack = Physics.SphereCast(transform.position, sphereCastRadius, -orientation.forward, out backWallhit, detectionLength, whatIsGround);
        wallRight = Physics.SphereCast(transform.position, sphereCastRadius, orientation.right, out rightwallHit, detectionLength, whatIsGround);
        wallLeft = Physics.SphereCast(transform.position, sphereCastRadius, -orientation.right, out leftWallHit, detectionLength, whatIsGround);

        wallLookAngle = Vector3.Angle(orientation.forward, - frontWallHit.normal);
        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNomral, frontWallHit.normal)) > minWallNormalAngleChange;
        
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
        if ((wallFront && newWall) || pm.grounded)
        {
            climbJumpsLeft = climbJumps;
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
            OnReleaseClasp();
        }
        climbing = true;
        pm.climbing = true;
        
        lastWall = frontWallHit.transform;
        lastWallNomral = frontWallHit.normal;
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
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
    private void StopClimbing()
    {
        climbing = false;
        pm.climbing = false;
    }
    private void OnClasp()
    {
        if (wallFront)
        {
            if (climbing)
            {
                StopClimbing();
            }
            clasping = true;
            pm.clasping = true;
        }
    }
    private void OnReleaseClasp()
    { 
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        clasping = false;
        pm.clasping = false;
    }

    private void ClimbBackJump()
    {
        exitingWall = true;
        exitingWallTimer = exitingWallTime;
        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climbJumpsLeft--;
    }
    private void Claspjump()
    {
        Vector3 forceToApply;

        switch (true)
        {
            case bool _ when wallRight:
                forceToApply = transform.up * (climbJumpUpForce / 2) + orientation.forward * SideJumpForwardFroce + -orientation.right * pushWayForce;
                break;

            case bool _ when wallLeft:
                forceToApply = transform.up * (climbJumpUpForce / 2) + orientation.forward * SideJumpForwardFroce + orientation.right * pushWayForce;
                break;
            default:
                forceToApply = transform.up * (climbJumpUpForce / 2) + orientation.forward * SideJumpForwardFroce;
                break;
        }
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
        exitingWall = true;
        exitingWallTimer = exitingWallTime;
        climbJumpsLeft--;
    }
    private void WallRelease()
    {
        exitingWall = true;
        exitingWallTimer = exitingWallTime;
        Vector3 forceToApply = transform.up * 1f + frontWallHit.normal * climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
