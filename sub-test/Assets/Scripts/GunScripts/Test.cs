using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform player, gunContainer, fpsCam;
    public LayerMask WhatIsItem;
    private GameObject Object;
    private string objectName;
    [Header("detecttion")]
    public float pickUpRange;
    public float sphereCastRadius;
    private RaycastHit itemFrontHit;
    private bool itemFront;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     ItemCheck();

        
    }

    private void ItemCheck()
    {
             itemFront = Physics.Raycast(fpsCam.position, fpsCam.forward, out itemFrontHit, pickUpRange, WhatIsItem);
        if (itemFront)
        {
                //Debug.Log(itemFrontHit.collider.name);
                Debug.Log(itemFrontHit.collider.name);

        }   
    }
}
