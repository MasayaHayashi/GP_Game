using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] string setsumei0 = "※オブジェクトの名前を必ず全部別々に設定してください";
    [SerializeField] string setsumei = "↓レーンの移動スピード";
    [SerializeField] string setsumei1 = "xとzに -1.0 ~ 1.0で指定してください";
    public Vector3 laneVelocity;
    [SerializeField] string setumei4 = "上にアイテムが乗った時にこの位置まで滑り落ちます";
    public float itemPosY;

    [HideInInspector] public Vector3 pos;

    static List<Lane> lanes = new List<Lane>();

    MeshRenderer upperPlane;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        lanes.Add(this);

        //上の板を取得
        upperPlane = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        UpperClear();

        //ディゾルブ演出を入れる
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Close()
    {
        Destroy(gameObject);    //ディゾルブ演出入れてから消す
    }

    public void UpperHighlight()
    {
        upperPlane.material.color = new Color(0.5f, 0.7f, 0.0f, 0.5f);
    }

    public void UpperClear()
    {
        upperPlane.material.color = new Color(0.0f,0.0f,0.0f,0.0f);
    }

    public static void LanesClose()
    {
        for (int i = 0; i < lanes.Count; i++)
            lanes[i].Close();
        lanes.Clear();
    }
}
