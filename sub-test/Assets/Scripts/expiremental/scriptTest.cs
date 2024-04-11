using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptTest : MonoBehaviour
{
    private int vectorZeroCount;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("turned on");
        vectorZeroCount = 1;	
    }

    // Update is called once per frame
    void Update()
    {
        if (vectorZeroCount == 1)
        {
        transform.localPosition = new Vector3(0, 0, 0);
            vectorZeroCount--;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.SetParent(null);
            vectorZeroCount++;
            this.enabled = false;
        }
    }
}
