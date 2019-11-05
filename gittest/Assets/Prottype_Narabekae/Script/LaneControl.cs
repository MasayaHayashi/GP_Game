using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneControl : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Material[] itemMaterials;

    Vector3 CREATE_POS = new Vector3(6.21f, 1.54f, -3.03f);

    float timeCnt;
    float TIME_INTERVAL_CREATE = 3.0f;

    List<FlowItem.eItemType> createWaitItems = new List<FlowItem.eItemType>();
    public void AddCreateList(FlowItem.eItemType type) { createWaitItems.Add(type); }

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = 0.0f;
        FlowItem.laneSpeedUpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //**debug
        if (DebugCanvas.debugCanvas)
            TIME_INTERVAL_CREATE = DebugCanvas.createIntervalSlider.value;

        //--- アイテムの生成 ---
        timeCnt += Time.deltaTime;
        if(timeCnt >= TIME_INTERVAL_CREATE)
        {
            timeCnt -= TIME_INTERVAL_CREATE;
            if (createWaitItems.Count > 0)
            {
                Instantiate(items[Random.Range(0, items.Length)], CREATE_POS, Quaternion.identity).GetComponent<FlowItem>().FirstSet(
                    this, createWaitItems[0], itemMaterials[(int)createWaitItems[0]]);
                createWaitItems.RemoveAt(0);
            }
        }

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
    }


}
