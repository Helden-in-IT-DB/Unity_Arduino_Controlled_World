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

    private Vector3 startScale;
    private Vector3 slideScale;
    private float lerpSpeed = 1f;

    [Header("input")]
    public KeyCode slideKey = KeyCode.C;
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Movement>();

        
        startScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
        slideScale = new Vector3 (transform.localScale.x, slideYScale, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
        {
            SlidingMovement();
        }
    }

    private void OnSlide()
    {
        if (pm.movementInput != Vector3.zero)
        {
            PlayerObj.transform.localScale = Vector3.Lerp(slideScale, startScale, lerpSpeed * Time.deltaTime);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            if (rb.velocity.y > -0.1f)
            {
                SlideTimer = maxSlideTime / 2;
            }
            else
            {
                SlideTimer = maxSlideTime;
            }
            pm.sliding = true;
        }



    }
    private void OnReleaseSlide()
    {
        if (pm.sliding)
        {
            pm.sliding = false;

            PlayerObj.localScale = Vector3.Lerp(startScale, slideScale, lerpSpeed * Time.deltaTime / 2);
        }
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
                OnReleaseSlide();
            }
        }
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * SlideForce, ForceMode.Force);
        }
    }
}



// https://www.youtube.com/watch?v=SsckrYYxcuM