using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 移動用スクリプト
// Author : MasayaHayashi

public class Mover : MonoBehaviour
{
    [SerializeField]
    private bool isXZmove;

    [SerializeField, Header("境界判定用")]
    private Vector3 borderLine;

    private bool isMoviong = false;
    private Vector3 moveDirection;

    private Hashtable hash = new Hashtable();

    private float delayTime; // 移動開始までの待機時間
    private float moveTime;  // 移動してから終わるまでの時間

    private const float MinDelayTime = 0.0f;
    private const float MaxDerayTime = 0.2f;
    private const float MinMoveTime = 1.0f;
    private const float MaxMoveTime = 2.5f;

    private const float OverborderLength = 5.0f; // 境界判定

    private Vector3 startPosition;

    delegate bool CheckLength();

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

        if(isOverborder())
        {
            transform.position = startPosition; // 仮処理 初期値に戻す
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
                return "z";
            default:
                return null;
        }
    }

    // 境界判定
    private bool isOverborder()
    {
        Vector3 movedVector = transform.position - startPosition;

        // XZ用判定
        CheckLength checkLengthXZ = () => 
        {
            float length = Mathf.Sqrt(movedVector.x * movedVector.x + movedVector.z * movedVector.z);

            if (length > OverborderLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        };

        // YZ用判定
        CheckLength checkLengthYZ = () =>
        {
            float length = Mathf.Sqrt(movedVector.y * movedVector.y + movedVector.z * movedVector.z);

            if (length > OverborderLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        };

        // 判定
        if (isXZmove)
        {
            if(checkLengthXZ())
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            if(checkLengthYZ())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
