using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("references")]
    public Transform player, gunContainer, fpsCam;
    public LayerMask WhatIsItem;
    private GameObject Object;

    
    [Header("detecttion")]
    public float pickUpRange;
    public float sphereCastRadius;
    private RaycastHit itemFrontHit;
    private bool itemFront;
    private bool HandsfullCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     ItemCheck();
     CheckAndToggleScript();
    }

    private void ItemCheck()
    {
        itemFront = Physics.Raycast(fpsCam.position, fpsCam.forward, out itemFrontHit, pickUpRange, WhatIsItem);
        if (itemFront)
        {
            //Debug.Log(itemFrontHit.collider.name);
            Object = itemFrontHit.collider.gameObject;
            //Debug.Log(Object);
        }
        else
        {
            Object = null;
        }
    }

    void CheckAndToggleScript()
    {
        if (Object != null && Input.GetKeyDown(KeyCode.E) && !HandsfullCheck)
        {
            // Get all MonoBehaviour components attached to the Object
            MonoBehaviour[] scripts = Object.GetComponentsInChildren<MonoBehaviour>();

            // Check if only one script was found
            if (scripts.Length >= 1) // Length is 2 because the GameObject itself is also counted
            {
                for (int i = 0; i <= (scripts.Length - 1); i++)
                {
                    // Check if the script starts with 'S'
                    if (scripts[i].GetType().Name.StartsWith("S"))
                    {
                        // Enable the script
                        Object.transform.SetParent(gunContainer);
                        HandsfullCheck = true;
                        scripts[i].enabled = true;
                        //Debug.Log("Script enabled on " + Object.name);
                    }
                }

            }
            else
            {
                Debug.Log("No scripts found on " + scripts.Length);
            }
        }
        if (HandsfullCheck && Input.GetKey(KeyCode.Q)) HandsfullCheck = false;
    }
}
