using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author MasayaHayashi
// ディゾルバー(オブジェクトに親子付けするとディゾルブができます)

public class Dissolver : SingletonMonoBehaviour<Dissolver>
{
    private List<GameObject> childObjects = new List<GameObject>();

    private List<int> maskIndexs     = new List<int>();
    private List<int> particleIndexs = new List<int>();

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

    [SerializeField, Header("パーティクル生成間隔")]
    private float ParticleSpawnTime;

    private bool complite = false;
    public bool isComplite { get { return complite; } }

    private bool isStarting = false;

    private Vector3 EvacuationPosition = new Vector3(0.0f, 500.0f, 0.0f);

    private ParticleSystem childParticle;

    // Start is called before the first frame update
    void Start()
    {
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            childObjects.Add(transform.GetChild(childIndex).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            begin();
        }

        attachParticle();

    }

    void CompleteHandler()
    {
        // パーティクルアタッチ
        childObjects[1].transform.parent = transform.parent;

        complite = true;

        initilize();
        transform.parent =  null;
        transform.position = EvacuationPosition;
    }

    private void initilize()
    {
        childObjects[0].transform.localPosition = EvacuationPosition;
        hash.Clear();
        isStarting = true;
    }

    public void begin()
    {
        if(isStarting)
        {
            initilizeAttach();
        }

        // パーティティクル生成
        StartCoroutine("beginParticle");

        isStarting = true;
        complite   = false;

        moveY = DeffaltMoveY;
        delay = DeffaltDelay;
        time  = DeffaltTime;

        // 位置初期化
        transform.localPosition                 = Vector3.zero;
        childObjects[0].transform.localPosition = Vector3.zero;

        hash.Add("y", moveY);
        hash.Add("delay", delay);
        hash.Add("time", time);
        hash.Add("easeType", iTween.EaseType.easeInBack);

        hash.Add("oncomplete", "CompleteHandler");
        hash.Add("oncompletetarget", gameObject);

        iTween.MoveAdd(childObjects[0], hash);

    }

    public void initilizeAttach()
    {
        hash.Clear();
        
        isStarting = false;
        complite   = false;
    }

    private void sort()
    {
        List<GameObject> tempChildObjects = new List<GameObject>();

        // 各オブジェクトを並び替え
        foreach (GameObject childObject in childObjects)
        {

        }
    }

    private void attachParticle()
    {

        ParticleSystem particle = childObjects[1].GetComponent<ParticleSystem>();

        if(!particle.IsAlive())
        {
            childObjects[1].transform.parent         = transform;
            childObjects[1].transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator beginParticle()
    {
        yield return new WaitForSeconds(ParticleSpawnTime);
        childObjects[1].GetComponent<ParticleSystem>().Play();

    }
}
