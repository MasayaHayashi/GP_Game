using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    [SerializeField] Image[] items;

    const float POS_UP_VAL = 2.3f;

    bool move;
    Vector3 startPos;
    Vector3 finPos;
    float lerpVal;

    const float UP_SPEED = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            MoveAnim();
    }

    void MoveAnim()
    {
        lerpVal += Time.deltaTime * UP_SPEED;
        if(lerpVal >= 1.0f)
        {
            lerpVal = 1.0f;
            move = false;
            if (finPos.y > 3.0f)
                Destroy(gameObject);
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

    public void SetColors(Color c1, Color c2, Color c3, Color c4)
    {
        items[0].color = c1;
        items[1].color = c2;
        items[2].color = c3;
        items[3].color = c4;
    }
}
