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
    private Vector3 move;
    private float moveSpeed = 8;
    private bool moving;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new Playercontrols();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moving)
        {
        moveDirection = orientation.forward * move.y + orientation.right * move.x;
        rb.velocity = moveDirection * moveSpeed;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

    }
    private void OnJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * moveSpeed, ForceMode.Impulse);
    }
    private void OnMove(InputValue inputValue)
    {
        move = inputValue.Get<Vector3>();
        moving = true;
        //moveDirection = orientation.forward * inputValue.Get<Vector3>().y + orientation.right * inputValue.Get<Vector3>().x;

        Debug.Log($"move: {move}");
        if (move == new Vector3(0f, 0f, 0f))
        {
            moving = false;
        }
    }

 }
