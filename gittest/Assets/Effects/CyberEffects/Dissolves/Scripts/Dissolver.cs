using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author MasayaHayashi
// ディゾルバー(オブジェクトにアタッチするとディゾルブができます)

public class Dissolver : SingletonMonoBehaviour<Dissolver>
{
    private List<GameObject> maskObjects = new List<GameObject>();

    private float moveY;
    private float delay;
    private float time;

    private GameObject spawnEffect;

    private Hashtable hash = new Hashtable();

    [SerializeField, Header("移動時間")]
    private float DeffaltTime;

    [SerializeField, Header("待機時間")]
    private float DeffaltDelay;

    [SerializeField, Header("移動量")]
    private float DeffaltMoveY;



    private bool complite = false;
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
        transform.parent =  null;
        transform.position = EvacuationPosition;
    }

    private void initilize()
    {
        maskObjects[0].transform.localPosition = EvacuationPosition;
        hash.Clear();
        isStarting = true;
    }

    public void begin()
    {
        if(isStarting)
        {
            initilizeAttach();
        }

        isStarting = true;
        complite   = false;

        moveY = DeffaltMoveY;
        delay = DeffaltDelay;
        time  = DeffaltTime;

        // 位置初期化
        transform.localPosition                = Vector3.zero;
        maskObjects[0].transform.localPosition = Vector3.zero;

        hash.Add("y", moveY);
        hash.Add("delay", delay);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInBack);

        hash.Add("oncomplete", "CompleteHandler");
        hash.Add("oncompletetarget", gameObject);

        iTween.MoveAdd(maskObjects[0], hash);

    }

    public void initilizeAttach()
    {
      //  iTween.Stop(maskObjects[0], "move");
        maskObjects.Clear();
        hash.Clear();

        maskObjects.Add(transform.GetChild(0).gameObject);
        isStarting = false;
        complite   = false;
    }
}
