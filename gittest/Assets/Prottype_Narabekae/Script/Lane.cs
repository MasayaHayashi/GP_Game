using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] string setsumei0 = "※オブジェクトの名前を必ず全部別々に設定してください";
    [SerializeField] string setsumei = "↓レーンの移動スピード";
    [SerializeField] string setsumei1 = "xとzに -1.0 ~ 1.0で指定してください";
    public Vector3 laneVelocity;

    [HideInInspector] public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
