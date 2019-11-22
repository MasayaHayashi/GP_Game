using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("MoveHorizontal");
        Debug.Log("MoveHorizontal = " + moveHorizontal);

        float moveVertical = Input.GetAxisRaw("MoveVertical");
        Debug.Log("MoveVertical = " + moveVertical);
    }
}
