using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float second;
    int minutes;

    // Start is called before the first frame update
    void Start()
    {
        second = 0.0f;
        minutes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;
        if(second >= 60.0f)
        {
            second -= 60.0f;
            minutes++;
        }

        //--- とりあえず5分経過でアラート ---
        if(minutes >= 5)
        {
            Debug.Log("5分経過だよ！");
        }
        //**Debug
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(minutes + "分" + second + "秒");
        }
    }
}
