using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spedometer : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CalculateSpeed());
    }
    IEnumerator CalculateSpeed()
    {
        Vector3 lPosition = transform.position;
        yield return new WaitForFixedUpdate();
        speed = (lPosition - transform.position)
    }
}
// https://www.youtube.com/watch?v=JRqoxvAeZfs