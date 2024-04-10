using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (Object != null)
        {
            // Get all MonoBehaviour components attached to the Object
            MonoBehaviour[] scripts = Object.GetComponentsInChildren<MonoBehaviour>();

            // Check if only one script was found
            if (scripts.Length >= 1) // Length is 2 because the GameObject itself is also counted
            {
                for (int i = 0; i <= (scripts.Length - 1); i++)
                {
                  // Check if the script starts with 'S'
                if (scripts[i].GetType().Name.StartsWith("s"))
                {
                    // Enable the script
                    scripts[i].enabled = true;
                    Debug.Log("Script enabled on " + Object.name);
                }
                else
                {
                    //Debug.Log("Script found but does not start with 'S' on " + Object.name);
                }  
                }
                
            }
            else
            {
                Debug.Log("No scripts found on " + scripts.Length);
            }
        }
    }
}
