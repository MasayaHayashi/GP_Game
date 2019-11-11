using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatFlashColor : MonoBehaviour
{
    Renderer selfRenderer;
    bool flashAnim;
    Color defColor;

    Color m_animColor;
    int m_flashNum;
    float m_intervalTime;

    float timeCnt;
    bool isDefColor;
    int flashCnt;

    // Start is called before the first frame update
    void Start()
    {
        selfRenderer = GetComponent<Renderer>();

        flashAnim = false;
        defColor = selfRenderer.material.color;
        timeCnt = 0.0f;
        isDefColor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashAnim)
            AnimUpdate();
    }

    void AnimUpdate()
    {
        timeCnt += Time.deltaTime;
        if(timeCnt >= m_intervalTime)
        {
            timeCnt = 0.0f;
            isDefColor ^= true;

            Color workColor;
            if (isDefColor)
            {
                workColor = defColor;
                flashCnt++;
                if (flashCnt >= m_flashNum)
                    flashAnim = false;
            }
            else
                workColor = m_animColor;

            selfRenderer.material.color = workColor;
        }
    }

    public void StartFlash(Color animColor, int num, float interval)
    {
        flashAnim = true;
        m_animColor = animColor;
        m_flashNum = num;
        m_intervalTime = interval;
        isDefColor = true;
        flashCnt = 0;
        timeCnt = 0.0f;
    }
}
