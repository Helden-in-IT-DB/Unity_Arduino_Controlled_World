using UnityEngine;
using System.IO.Ports;
using System;

public class move : MonoBehaviour
{
    string msg;
    SerialPort sp = new SerialPort("COM4", baudRate: 9600);
    // Start is called before the first frame update
    void Start()
    {
        InitArduino();
    }

    void InitArduino()
    {
        try
        {
            sp.Open();
            sp.ReadTimeout = 1000;
        }
        catch (Exception e)
        {
            Debug.Log(message: "rip lol: " + e);
        }
    }
    // Update is called once per frame
    void Update()
    {
        try 
        {
            if (msg != sp.ReadLine())
            {
                Debug.Log(message: sp.ReadLine());
                msg = sp.ReadLine();
            }
            switch (msg)
            {
                case "Left":
                    transform.Translate(Vector3.left * Time.deltaTime * 5);
                    break;
                case "Front":
                    transform.Translate(Vector3.forward * Time.deltaTime * 5);
                    break;
                case "Right":
                    transform.Translate(Vector3.right * Time.deltaTime * 5);
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(message: "Somthing went wrong lol: " + e);
        }
    }
    void OnAplicationQuit()
    {
        Debug.Log(message: "closing port");
        sp.Close();
        sp.Dispose();
    }
}




/*version 2 with danni

void setup() {
  // put your setup code here, to run once:
  pinMode(4, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);
  pinMode(2, INPUT_PULLUP);
  Serial.begin(9600);
  }

int buttonStatus = 0;
int buttonStatus2 = 0;

void loop() {
  // put your main code here, to run repeatedly:
  int pinValue2 = digitalRead(2);
  int pinValue3 = digitalRead(3);
  int pinValue4 = digitalRead(4);

  delay(10);

  //check button input
  if (pinValue2 == 0)
  {
    Serial.println("Left");
  }
  if (pinValue3 == 0)
  {
    Serial.println("Front");
  }
    if (pinValue4 == 0)
  {
    Serial.println("Right");
  }
  if ((pinValue4 && pinValue3 && pinValue2) != 0)
  {
    Serial.println("Null");
  }
}


*/  
