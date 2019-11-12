using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author MasayaHayashi
// ディゾルバー(オブジェクトにアタッチするとディゾルブができます)

public class Dissolver : MonoBehaviour
{
    [SerializeField, Header("マスクオブジェクト")]
    private List<GameObject> maskObjects;

    [SerializeField, Header("移動量")]
    private float moveY;
    [SerializeField, Header("待機時間")]
    private float delay;
    [SerializeField, Header("移動時間")]
    private float time;

    private Hashtable hash = new Hashtable();


    private void Awake()
    {
        maskObjects[0].transform.parent   = transform;
        maskObjects[0].transform.localPosition = Vector3.zero;

        hash.Add("y", moveY);
        hash.Add("delay", delay);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInSine);

        iTween.MoveAdd(maskObjects[0], hash);


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
