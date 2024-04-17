using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testMove : MonoBehaviour
{
    private Playercontrols playerControls;
    public Rigidbody rb;
    public Transform orientation;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new Playercontrols();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    private void OnJump()
    {
        Debug.Log("jump");
    }
    private void OnMove(InputValue inputValue)
    {
        moveDirection = orientation.forward * inputValue.Get<Vector3>().y + orientation.right * inputValue.Get<Vector3>().x;
        rb.velocity = moveDirection * 8f;
        Debug.Log("move");
    }

 }
