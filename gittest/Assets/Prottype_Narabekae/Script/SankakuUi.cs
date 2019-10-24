using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SankakuUi : MonoBehaviour
{
    Transform selfTrans;
    UiImageAnimation animationClass;

    // Start is called before the first frame update
    void Start()
    {
        selfTrans = transform;
        animationClass = GetComponent<UiImageAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosX(float x)
    {
        Vector3 pos = selfTrans.position;
        pos.x = x;
        selfTrans.position = pos;
    }

    public void StartAnim(bool correct)
    {
        Color workColor;
        if (correct)
            workColor = Color.green;
        else
            workColor = Color.red;

        animationClass.StartAnimFlash(false, 4, Color.white, workColor, true, 8.0f);
    }
}
