using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiImageAnimation : MonoBehaviour
{
    Image selfImage;

    public enum eAnimType
    {
        Flash,
        MAX,
    }

    public struct tFlashDat
    {
        public bool loop;
        public int flashNum;
        public Color startColor;
        public Color finishColor;
        public bool lastColorStartColor;
        public float speed;

        public float TimeCnt;
        public int NumCnt;
    }

    //アニメーション実行中か
    bool[] animPlayFlags = new bool[(int)eAnimType.MAX];
    //アニメーション情報
    tFlashDat flashAnimDat;

    // Start is called before the first frame update
    void Start()
    {
        selfImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        FlashAnimUpdate();
    }

    void FlashAnimUpdate()
    {
        if (!animPlayFlags[(int)eAnimType.Flash])
            return;

        float workLerp = 0.0f;
        //time
        flashAnimDat.TimeCnt += Time.deltaTime * flashAnimDat.speed;
        if (flashAnimDat.TimeCnt >= 2.0f) {
            flashAnimDat.TimeCnt = 0.0f;
            flashAnimDat.NumCnt++;
            if(!flashAnimDat.loop && flashAnimDat.NumCnt >= flashAnimDat.flashNum)
            {
                animPlayFlags[(int)eAnimType.Flash] = false;
            }
        }else if(flashAnimDat.TimeCnt >= 1.0f)
        {
            workLerp = 2.0f - flashAnimDat.TimeCnt;
            if (!flashAnimDat.loop && flashAnimDat.NumCnt >= flashAnimDat.flashNum - 1 && !flashAnimDat.lastColorStartColor)
            {
                animPlayFlags[(int)eAnimType.Flash] = false;
                workLerp = 1.0f;
            }
        }

        //color
        selfImage.color = Color.Lerp(flashAnimDat.startColor, flashAnimDat.finishColor, workLerp);
    }

    //アニメーション開始
    public void StartAnimFlash(bool loop, int flashNum, Color startColor, Color finishColor, bool lastColorStartColor, float speed)
    {
        flashAnimDat.loop = loop;
        flashAnimDat.finishColor = finishColor;
        flashAnimDat.flashNum = flashNum;
        flashAnimDat.speed = speed;
        flashAnimDat.startColor = startColor;
        flashAnimDat.lastColorStartColor = lastColorStartColor;

        flashAnimDat.NumCnt = 0;
        flashAnimDat.TimeCnt = 0.0f;
        animPlayFlags[(int)eAnimType.Flash] = true;
    }
    //アニメーションストップ
    public void StopAnim(eAnimType type)
    {
        animPlayFlags[(int)type] = false;
    }
}
