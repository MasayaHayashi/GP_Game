using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneControl : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Material[] itemMaterials;
    [SerializeField] Transform SpawnPosition;
    [SerializeField] GameObject _mainCameraGo;

    float timeCnt;
    float TIME_INTERVAL_CREATE = 3.0f;
    static GameObject mainCameraGo;

    List<FlowItem.eItemType> createWaitItems = new List<FlowItem.eItemType>();
    public void AddCreateList(FlowItem.eItemType type) { createWaitItems.Add(type); }

    //おじゃまアイテム
    public const float FREEZE_TIME = 3.0f;
    static float bugFreezeTime = 0.0f;
    public static bool BugFreezeFlag() { return bugFreezeTime > 0.0f; }
    public static void StartBug() {
        bugFreezeTime = FREEZE_TIME;
        iTween.ShakePosition(mainCameraGo, iTween.Hash("x", 0.5f,"time", 3.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCameraGo = _mainCameraGo;
        timeCnt = 0.0f;
        FlowItem.laneSpeedUpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //**debug
        if (DebugCanvas.debugCanvas)
            TIME_INTERVAL_CREATE = DebugCanvas.createIntervalSlider.value;

        //--- FPSの加速 ---
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 5.0f;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Time.timeScale = 1.0f;
        }

        //--- レーンスピードの加速 ---
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FlowItem.laneSpeedUpFlag ^= true;
        }

        //--- バグフリーズ処理 ---
        if (BugFreezeFlag())
        {
            bugFreezeTime -= Time.deltaTime;
            if (bugFreezeTime <= 0.0f)
                bugFreezeTime = 0.0f;
            return;
        }

        //--- アイテムの生成 ---
        timeCnt += Time.deltaTime;
        if (timeCnt >= TIME_INTERVAL_CREATE)
        {
            timeCnt -= TIME_INTERVAL_CREATE;
            if (createWaitItems.Count > 0)
            {
                Instantiate(items[Random.Range(0, items.Length)], SpawnPosition.position, Quaternion.identity).GetComponent<FlowItem>().FirstSet(
                    this, createWaitItems[0], itemMaterials[(int)createWaitItems[0]]);
                createWaitItems.RemoveAt(0);
            }
        }
    }



}
