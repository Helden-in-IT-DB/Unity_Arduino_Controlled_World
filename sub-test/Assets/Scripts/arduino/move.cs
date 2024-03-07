using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class move : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 9600);
    // Start is called before the first frame update
    void Start()
    {
        sp.Open ();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen) 
        {
            try 
            {
                if (sp.ReadByte() == 1) 
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * 5);
                }
                if (sp.ReadByte() == 2)
                {
                    transform.Translate(Vector3.back * Time.deltaTime * 5);
                }
            } catch (System.Exception) {}
        }
    }
}

//arduiono code; https://www.youtube.com/watch?v=iWPU9NSC-34

//const int butPin1 = 6; //connect button to digital pin 6
//const int butPin2 = 7; //connect button to digital pin 7

//void setup()
//{
//   Serial.begin(9600);

//   pinMode(butPin1, INPUT);
//   pinMode(butPin2, INPUT);

//   digitalWrite(butPin1, HIGH);
//   digitalWrite(butPin1, HIGH);
//    }

//void loop()
//{
//   if (digitalRead(butPin1) == LOW)
//   {
//  Serial.write(1);
//  Serial.flush();
//  delay(20);
//   }
//   if (digitalRead(butPin2) == LOW)
//   {
//  Serial.println("RIGHT");
//  Serial.write(2);
//  Serial.flush();
//  delay(20);
//   }
//}
