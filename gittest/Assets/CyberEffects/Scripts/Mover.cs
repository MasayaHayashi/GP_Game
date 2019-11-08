using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 移動用スクリプト
// Author : MasayaHayashi

public class Mover : MonoBehaviour
{
    [SerializeField]
    private bool isXZmove;

    private bool isMoviong = false;
    private Vector3 moveDirection;

    private Hashtable hash = new Hashtable();

    private float delayTime; // 移動開始までの待機時間
    private float moveTime;  // 移動してから終わるまでの時間

    private const float MinDelayTime = 0.0f;
    private const float MaxDerayTime = 0.2f;
    private const float MinMoveTime = 1.0f;
    private const float MaxMoveTime = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoviong)
        {
            // 移動ベクトル、速度設定
            float moveVector = Random.Range(1.0f, 2.0f);
            if (Random.Range(0, 2) == 0)
            {
                moveVector *= -1;
            }

            initilizeHash(randamVectorString(), moveVector);
            move();


        }

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

        // 改善する必要あり
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
                if(isXZmove)
                {
                    return "z";
                }
                else
                {
                    return "z";
                }
            default:
                return null;
        }
    }

}
