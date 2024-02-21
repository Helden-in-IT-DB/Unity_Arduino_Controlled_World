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
        
    }
}
