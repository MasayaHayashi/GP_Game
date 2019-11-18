using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author : MasayaHAyashi
// スケール拡大コンポーネント

public class addScale : MonoBehaviour
{
    private Vector3 addSpeed = new Vector3(0.33f,0.0f,0.02f);
    private Hashtable hash = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("stop");

        hash.Add("x", 2.5f);
        hash.Add("y", 2.2f);
        hash.Add("z", 2.2f);
        hash.Add("time", 8.0f);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);

        iTween.ScaleAdd(gameObject, hash);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator stop()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();

        //1フレーム停止
        yield return new WaitForSeconds(2);

        particle.Stop();
    }


}
