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

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode grab = KeyCode.LeftAlt;

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
            // state 2 - clasping
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
            // state 3 - none
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

        
        if (pm.grounded)
        {
            climbTimer = maxClimbTime;
        }
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
        rb.useGravity = false;
        clasping = true;
        pm.clasping = true;
    }
    private void StopClasping()
    {
        rb.useGravity = true;
        clasping = false;
        pm.clasping = false;
    }
}
