using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone_switch : MonoBehaviour
{
    [SerializeField] private GameObject connectedPlayer;


    // Update is called once per frame
    void Update()
    {
        
        if (transform.parent.parent.parent.gameObject != null || transform.parent.parent.parent.gameObject != connectedPlayer)
        {
            connectedPlayer = transform.parent.parent.gameObject;
        }
        
        this.enabled = false;
    }
}
