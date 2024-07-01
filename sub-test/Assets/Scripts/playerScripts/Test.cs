using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("references")]
    public Transform player, gunContainer, itemContainer, fpsCam;
    public LayerMask WhatIsItem;
    private GameObject Object;
    [SerializeField] private GameObject arms;

    
    [Header("detecttion")]
    public float pickUpRange;
    public float sphereCastRadius;
    private RaycastHit itemFrontHit;
    private bool itemFront;
    private bool HandsfullCheck;
    private bool objectScripted;

    // Update is called once per frame
    void Update()
    {
     if (!HandsfullCheck) ItemCheck();
     if (HandsfullCheck && !objectScripted) MoveObject(); 
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
    private void MoveObject()
    {
        Object.transform.localPosition = Vector3.Lerp(Vector3.zero, Object.transform.localPosition, 5 * Time.deltaTime);
    }
    private void OnPickUp()
    {
        if (Object != null && !HandsfullCheck)
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
                        objectScripted = true;
                        scripts[i].enabled = true;
                        arms.SetActive(true);
                        //Debug.Log("Script enabled on " + Object.name);
                    }
                }

            }
            else
            {
                Object.transform.SetParent(itemContainer);
                objectScripted = false;
                HandsfullCheck = true;
            }
        }
    }
    private void OnDrop()
    {
        if (HandsfullCheck)
        {
            HandsfullCheck = false;
            arms.SetActive(false);
            if (!objectScripted) UnscriptedItemDrop();
        }
    }
    private void UnscriptedItemDrop()
    {
        Object.transform.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;
        Object.transform.SetParent(null);
    }
}
