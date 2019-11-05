using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachine : MonoBehaviour
{
    float lerpVal;
    float speed = 1.0f;
    bool anim;
    Transform selfTrans;

    // Start is called before the first frame update
    void Start()
    {
        lerpVal = 0.0f;
        selfTrans = transform;
        anim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            lerpVal += Time.deltaTime * speed;
            Vector3 startPos = selfTrans.position;
            Vector3 pressPos = selfTrans.position;
            pressPos.y = 1.5f;
            startPos.y = 4.0f;

            if (lerpVal >= 1.0f && speed > 0.0f)
            {
                lerpVal = 1.0f;
                speed *= -1.0f;

                //当たっているアイテムを削除
                Collider[] collisions = Physics.OverlapBox(selfTrans.position, selfTrans.localScale / 2.0f, transform.rotation);
                for (int i = 0; i < collisions.Length; i++)
                {
                    if (LayerMask.LayerToName(collisions[i].gameObject.layer) == "Item") {
                        Destroy(collisions[i].gameObject);
                    }
                }
            }
            else if(lerpVal <= 0.0f && speed < 0.0f){
                lerpVal = 0.0f;
                speed *= -1.0f;
                anim = false;
            }

            Vector3 pos = Vector3.Lerp(startPos, pressPos, lerpVal);
            selfTrans.position = pos;
        }
    }

    public void Press()
    {
        anim = true;
        lerpVal = 0.0f;
    }
}
