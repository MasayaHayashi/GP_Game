using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author MasayaHayashi
// ディゾルバー(オブジェクトにアタッチするとディゾルブができます)

public class Dissolver : SingletonMonoBehaviour<Dissolver>
{
    private List<GameObject> maskObjects = new List<GameObject>();

    [SerializeField, Header("移動量")]
    private float moveY;
    [SerializeField, Header("待機時間")]
    private float delay;
    [SerializeField, Header("移動時間")]
    private float time;

    private Hashtable hash = new Hashtable();

    private const float DeffaltMoveY = 3.0f;
    private const float DeffaltDelay = 1.3f;
    private const float DeffaltTime = 3.0f;

    private bool complite = true;
    public bool isComplite { get { return complite; } }

    private bool isStarting = false;

    private Vector3 EvacuationPosition = new Vector3(0.0f, 500.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        maskObjects.Add(transform.GetChild(0).gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            begin();
        }

    }

    void CompleteHandler()
    {
        complite = true;
        initilize();
    }

    private void initilize()
    {
        maskObjects[0].transform.localPosition = EvacuationPosition;
        hash.Clear();
        isStarting = true;
    }

    private void begin()
    {
        if(!complite && isStarting)
        {
            return;
        }

        isStarting = true;
        complite = false;

        moveY = DeffaltMoveY;
        delay = DeffaltDelay;
        time = DeffaltTime;

        // マスクオブジェクト初期化
        maskObjects[0].transform.localPosition = Vector3.zero;

        hash.Add("y", moveY);
        hash.Add("delay", delay);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInSine);

        hash.Add("oncomplete", "CompleteHandler");
        hash.Add("oncompletetarget", gameObject);

        iTween.MoveAdd(maskObjects[0], hash);

    }

    public void initilizeAttach()
    {
        maskObjects.Clear();
        maskObjects.Add(transform.GetChild(0).gameObject);
        isStarting = false;
    }
}
