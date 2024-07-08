using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    public Transform camaraPos;

    // Update is called once per frame
    void Update()
    {
        //transforms the position of the camara to the desired gameobject
        transform.position = camaraPos.position;
    }
}
