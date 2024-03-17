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

    [Header("detecttion")]
    public float pickUpRange;
    public float sphereCastRadius;
    private RaycastHit itemFrontHit;
    public bool itemFront;
    // Start is called before the first frame update
    void Start()
    {
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
    }
    
    // Update is called once per frame
    void Update()
    {
        ItemCheck();
        //check if player is in range and 'E' is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && itemFront && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

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
    }
    private void ItemCheck()
    {
        //itemFront = Physics.SphereCast(player.position, sphereCastRadius, fpsCam.forward, out itemFrontHit, pickUpRange, WhatIsItem);
        if (Physics.Raycast(player.position, fpsCam.forward, out itemFrontHit))
        {
            Debug.Log(itemFrontHit.collider.gameObject);
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
}
