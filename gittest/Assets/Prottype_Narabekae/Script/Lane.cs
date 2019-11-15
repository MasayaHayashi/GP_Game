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

    static List<Lane> lanes = new List<Lane>();


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        lanes.Add(this);

        //ディゾルブ演出を入れる
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Close()
    {
        lanes.Remove(this);
        Destroy(gameObject);    //ディゾルブ演出入れてから消す
    }

    public static void LanesClose()
    {
        for (int i = 0; i < lanes.Count; i++)
            lanes[i].Close();
    }
}
