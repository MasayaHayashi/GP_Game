using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回転用スクリプト
// Author : HayashiMasaya

public class Rotatar : MonoBehaviour
{
    [SerializeField, Header("回転軸")]
    private Vector3 axis;

    [SerializeField, Header("回転スピード")]
    private float rotSpeed;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = rotSpeed * Time.deltaTime;
        Quaternion qua = Quaternion.AngleAxis(angle, axis);

        transform.rotation = qua * transform.rotation;
    }
}
