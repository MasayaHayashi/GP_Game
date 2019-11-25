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

    [SerializeField, Header("拡大までの時間")]
    private float time;


    // Start is called before the first frame update
    void Start()
    {
        // 初期状態確保
        startPosition = transform.localPosition;
        startScale    = transform.localScale;

        begin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void begin()
    {
        transform.localScale = startScale;
        hash.Clear();


        hash.Add("x", addScaleVector.x);
        hash.Add("y", addScaleVector.y);
        hash.Add("z", addScaleVector.z);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInOutSine);

        hash.Add("oncomplete", "CompleteHandler");
        hash.Add("oncompletetarget", gameObject);

        iTween.ScaleAdd(gameObject, hash);
    }

    void initilize()
    {
        transform.localPosition = startPosition;
        transform.localScale    = startScale;
    }

    void CompleteHandler()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }
}
