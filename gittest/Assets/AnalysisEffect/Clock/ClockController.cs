using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;   // DateTimeに必要


public class ClockController : MonoBehaviour
{

   public bool sec;   // 秒針の有無
    public bool secTick;   // 秒針を秒ごとに動かすか

    public GameObject hour;
    public GameObject minute;
    public GameObject second;

    public double m_Time; 

    void Start()
    {
       // if (!sec)
       //     Destroy(second); // 秒針を消す
    }

    void Update()
    {
        DateTime dt = DateTime.Now;

        m_Time += Time.deltaTime;
        
        //12:00   
        int htmp=8;
        int mtmp=0;
        int stemp=0;


        hour.transform.eulerAngles = new Vector3(0, 0, (float)htmp / 12 * -360 + (float)mtmp / 60 * -30);
        minute.transform.eulerAngles = new Vector3(0, 0, (float)mtmp / 60 * -360);
        second.transform.eulerAngles = new Vector3(0, 0, (float)stemp / 60 * -360);


        //if (sec)
        //{
        //    if (secTick)
        //        second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360);
        //    else
        //        second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360 + (float)dt.Millisecond / 60 / 1000 * -360);
        //}

    }
}
