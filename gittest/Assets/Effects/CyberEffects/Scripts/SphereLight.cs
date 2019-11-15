using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 球体ブルームオブジェクト
// Author : MasayaHayashi

public class SphereLight : MonoBehaviour
{
    private Vector3 moveVec;
    private float   accele = 0.025f;

    private float[] clampX = new float[2];
    private float[] clampY = new float[2];

    private float vectorChangeTime = 5.0f;
    private float testX;

    private float lerpTime = 0.0f;


    private void Awake()
    {
        clampX[0] = Random.Range(-0.5f, 0.5f);
        clampX[1] = Random.Range(-0.8f, 0.9f);
        clampY[0] = Random.Range(-0.2f, 0.2f);
        clampY[1] = Random.Range(-0.8f, 0.9f);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("changeMoveVec");
    }

    // Update is called once per frame
    void Update()
    {
        testX = Mathf.Lerp(transform.position.x, transform.position.x + 5.0f, lerpTime);
        moveVec.x += testX;


        if(lerpTime >= 1.0f)
        {
            accele *= -1.0f;
            lerpTime = 0.0f;
        }
        else
        {
            lerpTime += Time.deltaTime * 0.5f;
        }

        transform.position += moveVec.normalized * accele;

    }

    public IEnumerator changeMoveVec()
    {
        while (true)
        {



            yield return new WaitForSeconds(vectorChangeTime);

        }




    }
}