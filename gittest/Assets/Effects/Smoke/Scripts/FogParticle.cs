using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author : MasayaHAyashi

public class FogParticle : MonoBehaviour
{
    private Hashtable hash = new Hashtable();

    private Vector3 startPosition;
    private Vector3 startScale;

    [SerializeField, Header("拡大スケール設定")]
    private Vector3 addScaleVector;

    // Start is called before the first frame update
    void Start()
    {
        // 初期状態確保
        startPosition = transform.localPosition;
        startScale    = transform.localScale;

        StartCoroutine("stop");

        hash.Add("x", addScaleVector.x);
        hash.Add("y", addScaleVector.y);
        hash.Add("z", addScaleVector.z);
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

    void initilize()
    {
        transform.localPosition = startPosition;
        transform.localScale    = startScale;
    }
}
