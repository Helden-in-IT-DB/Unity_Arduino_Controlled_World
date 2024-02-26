using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Climbing : MonoBehaviour
{
    [Header("references")]
    public Transform orientation;
    public Rigidbody rb;
    public Movement pm;
    public LayerMask whatIsWall;


    [Header("climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbtimer;

    private bool climbing;

    [Header("ClimbJumping")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;

    public KeyCode jumpKey = KeyCode.Space;
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

    private Transform lastWall;
    private Vector3 lastWallNomral;
    public float minWallNormalAngleChange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wallcheck();
        StateMachine();

        if (climbing && !exitingWall) ClimbingMovement();
    }

    private void StateMachine()
    {
        // state 1 - climbing
        if(wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && !exitingWall)
        {
            if(!climbing && climbtimer > 0)
            {
                StartClimbing();
            }

                // timer
                if (climbtimer > 0) climbtimer -= Time.deltaTime;
                if (climbtimer < 0) StopClimbing();
            
        }
        // state 2 - exiting
        else if (exitingWall)
        {
            if (climbing) StopClimbing();

            if (exitingWallTimer > 0) exitingWallTimer -= Time.deltaTime;
            if (exitingWallTimer < 0) exitingWall = false;
        }
        //state 3 - none
        else 
        {
            if(climbing) StopClimbing();
        }

        if(wallFront && Input.GetKeyDown(jumpKey) && climbJumpsLeft > 0)
        {
            ClimbJump();
        }
    }
    private void Wallcheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, - frontWallHit.normal);

        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNomral, frontWallHit.normal)) > minWallNormalAngleChange;

        if ( (wallFront && newWall) || pm.grounded)
        {
            climbtimer = maxClimbTime;
            climbJumpsLeft = climbJumps;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        pm.climbing = true;

        lastWall = frontWallHit.transform;
        lastWallNomral = frontWallHit.normal;
    }
    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }
    private void StopClimbing()
    {
        climbing = false;
        pm.climbing = false;
    }

    private void ClimbJump()
    {
        exitingWall = true;
        exitingWallTimer = exitingWallTime;
        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climbJumpsLeft--;
    }
}

// https://www.youtube.com/watch?v=tAJLiOEfbQg