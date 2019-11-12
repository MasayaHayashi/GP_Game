using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 移動用スクリプト
// Author : MasayaHayashi

public class Mover : MonoBehaviour
{
    [SerializeField]
    protected bool isXZmove;

    [SerializeField, Header("境界判定用")]
    protected Vector3 borderLine;

    protected bool isMoviong = false;
    protected Vector3 moveDirection;

    protected Hashtable hash = new Hashtable();

    protected float delayTime; // 移動開始までの待機時間
    protected float moveTime;  // 移動してから終わるまでの時間

    protected const float MinDelayTime = 0.0f;
    protected const float MaxDerayTime = 0.2f;
    protected const float MinMoveTime = 1.0f;
    protected const float MaxMoveTime = 2.5f;

    protected const float OverborderLength = 5.0f; // 境界判定

    protected Vector3 startPosition;

    protected delegate bool CheckLength();

    private void Awake()
    {
        startPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void initilizeHash(string keyName, float moveVector)
    {
        // 各種時間設定
        delayTime = Random.Range(MinDelayTime, MaxDerayTime);
        moveTime  = Random.Range(MinMoveTime, MaxMoveTime);

        hash.Clear();

        hash.Add("delay", delayTime);   // 開始時間
        hash.Add(keyName, moveVector);  // 移動方向
        hash.Add("time", moveTime);
        hash.Add("easetype", iTween.EaseType.easeInOutExpo);
        hash.Add("oncomplete", "CompleteHandler");
        hash.Add("oncompletetarget", gameObject);
    }

    void CompleteHandler()
    {
        isMoviong = false;
    }

    private void move()
    {
        iTween.MoveAdd(gameObject, hash);
        isMoviong = true;
    }

    private string randamVectorString()
    {
        int randam = Random.Range(0, 2);
        
        switch(randam)
        {
            case 0:
                if (isXZmove)
                {
                    return "x";
                }
                else
                {
                    return "y";
                }
            case 1:
                return "z";
            default:
                return null;
        }
    }

}
