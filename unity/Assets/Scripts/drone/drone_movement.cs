using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone_movement : MonoBehaviour
{
    [SerializeField] private GameObject connectedPlayer;

    // Start is called before the first frame update
    
    void Start()
    {
        if (transform.parent.parent.parent.gameObject != null)
        {
            connectedPlayer = transform.parent.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.parent.parent.gameObject != null && transform.parent.parent.parent.gameObject != connectedPlayer)
        {
            connectedPlayer = transform.parent.parent.gameObject;
        }
    }
}
