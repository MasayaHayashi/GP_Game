using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    [SerializeField, Header("噴射パーティクル")]
    private ParticleSystem jetParticle;

    [SerializeField, Header("フォグ")]
    private ParticleSystem fogHeavy;

    private Hashtable hash = new Hashtable();
    private Hashtable fogHash = new Hashtable();

    private float alpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        hash.Add("y", 5.0f);
        hash.Add("time", 22.0f);

        fogHash.Add("x", 3.0f);
        fogHash.Add("y", 2.0f);
        fogHash.Add("z", 3.0f);
        fogHash.Add("time", 12.0f);
    }

    // Update is called once per frame
    void Update()
    {


        jetParticle.gameObject.SetActive(true);

        alpha -= 0.01f;

        fogHeavy.gameObject.SetActive(true);

        iTween.ScaleAdd(fogHeavy.gameObject, fogHash);

        iTween.MoveAdd(gameObject,hash);
    }
}
