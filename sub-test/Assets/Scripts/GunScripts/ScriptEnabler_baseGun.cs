using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class ScriptEnabler_baseGun : MonoBehaviour
{
    [Header("references")]
    public BaseGun gunScript;
    public Rigidbody rb;
    private Transform player, fpsCam;
    public BoxCollider coll;

    [Header("gun drop stats")]


    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    private string objectName;

    private bool PickUpAble;

    [Header("call ons")]
    private bool callPickUp;

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
            this.enabled = false;
        }
        else if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            callPickUp = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!callPickUp)
        {
            FindComponents();
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
    private void FindComponents()
    {
        fpsCam = transform.parent.parent;
        player = transform.parent.parent.parent;
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;
        callPickUp = false;

        //makes the weapon a child of the gunholder and sets it in place
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
        callPickUp = true;
        
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
        this.enabled = false;
    }
}
