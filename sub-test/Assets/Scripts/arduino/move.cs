using UnityEngine;
using System.IO.Ports;
using System;

public class move : MonoBehaviour
{
    string msg;
    //makes a connecttion with the arduino. check device manager for which 'COM'
    SerialPort sp = new SerialPort("COM4", baudRate: 9600);
    public bool working;
    // Start is called before the first frame update
    void Start()
    {
        InitArduino();
    }

    void InitArduino()
    {
        try
        {
            //open's arduino connecttion
            sp.Open();
            sp.ReadTimeout = 1000;
            working = true;
        }
        catch (Exception e)
        {
          //debug message to show when something is not going right.
            Debug.Log(message: "connection error: " + e);
            ShutDown();
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
            Debug.Log(message: "input read error: " + e);
            InitArduino();       
        }
    }
    void OnAplicationQuit()
    {
        Debug.Log(message: "closing port");
        sp.Close();
        sp.Dispose();
    }
    public void ShutDown()
    {
      working = false;
      sp.Close();
      Debug.Log("disabling script");
      this.enabled = false;
    }
    public void TurnOn()
    {
      Debug.Log("enabling script");
      this.enabled = true;
      InitArduino();
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
