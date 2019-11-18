using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author :MasayaHayashi   
// プレス演出

public class Press : MonoBehaviour
{
    private Hashtable hash = new Hashtable();

    private const float StopTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        hash.Add("y", 1.5f);
        hash.Add("time", 2.0f);
        StartCoroutine("stop");

        iTween.MoveAdd(gameObject, hash);
    }

    IEnumerator stop()
    {
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();

        yield return new WaitForSeconds(StopTime);

        particle.Stop();
    }


    void CompleteHandler()
    {

    }

    public void press()
    {
        hash.Add("y", 1.5f);
        hash.Add("time", 2.0f);
        StartCoroutine("stop");

        iTween.MoveAdd(gameObject, hash);
    }
}
