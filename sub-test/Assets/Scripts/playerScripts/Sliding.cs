using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("references")]
    public Transform orientation;
    public Transform PlayerObj;
    private Rigidbody rb;
    private Movement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float SlideForce;
    private float SlideTimer;

    public float slideYScale;
    private float startYscale;

    [Header("input")]
    public KeyCode slideKey = KeyCode.C;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Movement>();

        startYscale = PlayerObj.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            StartSlide();
        }
        if (Input.GetKeyUp(slideKey) && sliding)
        {
            StopSlide();
        }
    }
    private void FixedUpdate()
    {
        if (sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        sliding	= true;                                                                 

        PlayerObj.localScale = new Vector3(PlayerObj.lossyScale.x, slideYScale, PlayerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        SlideTimer = maxSlideTime;
    }
    private void StopSlide()
    {
        sliding = false;

        PlayerObj.localScale = new Vector3(PlayerObj.lossyScale.x, startYscale, PlayerObj.localScale.z);

    }
    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (!pm.OnSlope() || rb.velocity.y > -0.1f)
        {

            rb.AddForce(inputDirection.normalized * SlideForce, ForceMode.Force);

            SlideTimer -= Time.deltaTime;

            if (SlideTimer <= 0)
            {
                StopSlide();
            }
        }
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * SlideForce, ForceMode.Force);
        }
    }
}



// https://www.youtube.com/watch?v=SsckrYYxcuM