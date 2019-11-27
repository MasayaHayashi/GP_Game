using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    [SerializeField] Image[] items;
    [SerializeField] UiImageAnimation[] childAnimClasses;
    [SerializeField] UiImageAnimation animClass;

    [SerializeField] Image frameBefore;
    [SerializeField] Image frameAfter;

    const float POS_UP_VAL = 2.3f;

    bool move;
    Vector3 startPos;
    Vector3 finPos;
    float lerpVal;

    const float UP_SPEED = 2.0f;

    //フレームチェンジ
    bool frameChange;
    float framaChangeLerpVal;
    const float FRAME_CHANGE_SPEED = 2.0f;

    public bool FinCallFlashAnim() { return !animClass.GetAnimPlayFlag(UiImageAnimation.eAnimType.Flash); }

    // Start is called before the first frame update
    void Start()
    {
        framaChangeLerpVal = 0.0f;
        frameChange = false;
        move = false;
        //animClass = GetComponent<UiImageAnimation>();
        if (transform.localPosition.y >= 2.3f)
            frameChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            MoveAnim();

        if (frameChange)
            FrameChangeAnim();
    }

    void FrameChangeAnim()
    {
        framaChangeLerpVal += Time.deltaTime * FRAME_CHANGE_SPEED;
        if(framaChangeLerpVal >= 1.0f)
        {
            framaChangeLerpVal = 1.0f;
            frameChange = false;
        }
        Color work = frameAfter.color;
        work.a = framaChangeLerpVal;
        frameAfter.color = work;

        work = frameBefore.color;
        work.a = 1.0f - framaChangeLerpVal;
        frameBefore.color = work;
    }

    void MoveAnim()
    {
        lerpVal += Time.deltaTime * UP_SPEED;
        if(lerpVal >= 1.0f)
        {
            lerpVal = 1.0f;
            move = false;
            if (finPos.y > 3.0f)
            {
                Destroy(gameObject);
                return;
            }else if(finPos.y >= 2.3f)
            {
                frameChange = true;
            }
        }
        transform.localPosition = Vector3.Lerp(startPos, finPos, lerpVal);
    }

    public void UpPos()
    {
        startPos = finPos = transform.localPosition;
        finPos.y += POS_UP_VAL;
        move = true;
        lerpVal = 0.0f;
    }

    public void SetColors(Sprite c1, Sprite c2, Sprite c3, Sprite c4)
    {
        items[0].sprite = c1;
        items[1].sprite = c2;
        items[2].sprite = c3;
        items[3].sprite = c4;
    }

    public void StartFlash(bool correct)
    {
        Color workColor;
        if (correct)
            workColor = Color.green;
        else
            workColor = Color.red;

        //とりあえずフレームだけ
        animClass.StartAnimFlash(false, 10, Color.white, workColor, true, 12.0f);
    }
}
