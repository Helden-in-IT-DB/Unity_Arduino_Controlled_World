using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDrop : MonoBehaviour
{
    [Header("references")]
    public BaseGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;
    public LayerMask WhatIsItem;

    [Header("gun drop stats")]
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    private string objectName;

    [Header("detecttion")]
    public float pickUpRange;
    public float sphereCastRadius;
    private RaycastHit itemFrontHit;
    private bool itemFront;
    private bool PickUpAble;
    // Start is called before the first frame update
    void Start()
    {
        
        objectName = gameObject.name;
        /* setup
        //setup
        if (!equipped)
        {
            gunScript.DisableAmmoText();
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        else if (equipped){
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
        */
        
    }
    
    // Update is called once per frame
    void Update()
    {
        /* 
        //item pick up
        ItemCheck();
        //check if player is in range and 'E' is pressed
        if(!equipped &&  itemFront && PickUpAble && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }
        */

        /* drop
        //drop if equiped and 'Q' is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }        
        if (equipped)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        */
    }

    /*
    //item check and pickup
    private void ItemCheck()
    {
        itemFront = Physics.Raycast(fpsCam.position, fpsCam.forward, out itemFrontHit, pickUpRange, WhatIsItem);
        if (itemFront)
        {
           if (itemFrontHit.collider.name == objectName) 
            {
                //Debug.Log(itemFrontHit.collider.name);
                PickUpAble = true;
            }
            else
            {
                PickUpAble = false;
            }
        }
    }
    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //makes the weapon a child of the gunholder and sets it in place
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        //makes rigidbody kinematic and boxcollider a trigger
        rb.isKinematic = true;
        coll.enabled = false;

        //Enable script
        gunScript.enabled = true;
    }
    */

    /* item drop and momentum tranfer
    private void Drop()
    {
        equipped = false;
        slotFull = false;
        
        //sets parent to null
        transform.SetParent(null);

        //makes rigidbody not kinematic and boxcollider normal
        rb.isKinematic = false;
        coll.enabled = true;

        //sets gun's velocity to the one of the player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        // adds random rotation
        float random = Random.Range(-1f, 1);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //add throw forces
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);        
        
        //disable script
        gunScript.DisableAmmoText();
        gunScript.enabled = false;
    }
    */
}
