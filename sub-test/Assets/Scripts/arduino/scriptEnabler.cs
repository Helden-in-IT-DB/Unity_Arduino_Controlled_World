using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptEnabler : MonoBehaviour
{
    // connects to script
    public move script;
    //Input action
    public KeyCode action;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(action))
        {
            if(script.arduinoWorking)
            {
                script.ShutDown();
            }
            else 
            {
                script.TurnOn();
            }
        }    
    }
}
