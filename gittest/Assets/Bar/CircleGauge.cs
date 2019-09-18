using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGauge : MonoBehaviour
{

    //----- private
    [SerializeField] private float NUM = 0;
    [SerializeField] private float CNT = 0;
    [SerializeField] private bool FLAG_NUM = false;
    [SerializeField] private bool FLAG_CNT = false;

    //----- public
    public Image circleGauge;


    // Start is called before the first frame update
    void Start()
    {

        if(FLAG_NUM && FLAG_CNT)
        {
            FLAG_CNT = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(FLAG_NUM)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                circleGauge.fillAmount -= 1.0f / NUM;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                circleGauge.fillAmount += 1.0f / NUM;
            }
        }
        if (FLAG_CNT)
        {
            if (Input.GetKey(KeyCode.A))
            {
                circleGauge.fillAmount -= 1.0f / CNT * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                circleGauge.fillAmount += 1.0f / CNT * Time.deltaTime;
            }
        }

        

        

    }
}
