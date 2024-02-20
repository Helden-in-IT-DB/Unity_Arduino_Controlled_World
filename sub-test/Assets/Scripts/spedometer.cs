using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Spedometer : MonoBehaviour
{
    public TextMeshProUGUI spedometer;
    public float interval;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetSpedometer", 0, interval);
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
        speed = (lPosition - transform.position).magnitude / Time.deltaTime;
    }
        void SetSpedometer()
    {
        spedometer.text = "speed: " + ((float)Math.Round(speed, 2)).ToString("00.00");
    }
}
// https://www.youtube.com/watch?v=JRqoxvAeZfs