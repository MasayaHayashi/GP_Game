using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneControl : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Material[] itemMaterials;
    [SerializeField] Transform SpawnPosition;
    [SerializeField] GameObject _mainCameraGo;
    [SerializeField] TextAsset[] stageDatas;

    [SerializeField] GameObject lanePrefav;

    [SerializeField] GameObject[] pressPosObj;
    [SerializeField] GameObject spawnPosObj;
    [SerializeField] GameObject goalPosObj;
    [SerializeField] GameObject wallObj;
    [SerializeField] GameObject pressMachineObj;

    [SerializeField] GameObject dissolveObj;

    //読み込みステート
    enum eStageDataLoadState
    {
        lane,
        lanePos,
        laneRot,
        laneSize,
        laneVel,

        presspos,
        pressposPos,
        pressposRot,
        pressposSize,

        spwanpos,
        spwanposPos,

        goalpos,
        goalposPos,
        goalposRot,
        goalposSize,

        wall,
        wallPos,
        wallRot,
        wallSize,

        pressmachine,
        pressmachinePos,
        pressmachineRot,
        pressmachineSize,

        fin,
    }
    struct tTransData
    {
        public Vector3 pos;
        public Vector3 scale;
        public Quaternion rot;
    }
    struct tStageData
    {
        public List<tTransData> laneTransform;
        public List<Vector3> laneVelocity;
        public List<tTransData> pressPosTransform;
        public tTransData spawnPosTransform;
        public tTransData goalPosTransform;
        public tTransData wallTransform;
        public tTransData pressMachineTransform;
    }
    List<tStageData> loadStageDatas = new List<tStageData>();

    float timeCnt;
    float TIME_INTERVAL_CREATE = 3.0f;
    static GameObject mainCameraGo;


    bool isLaneActive;      //レーンが動いているかどうかのフラグ
    public bool GetLaneActive() { return isLaneActive; }
    public void SetLaneActive(bool flag) { isLaneActive = flag; }

    List<FlowItem.eItemType> createWaitItems = new List<FlowItem.eItemType>();
    public void AddCreateList(FlowItem.eItemType type) { createWaitItems.Add(type); }

    //おじゃまアイテム
    public const float FREEZE_TIME = 3.0f;
    static float bugFreezeTime = 0.0f;
    public static bool BugFreezeFlag() { return bugFreezeTime > 0.0f; }
    public static void StartBug() {
        bugFreezeTime = FREEZE_TIME;
        //iTween.ShakePosition(mainCameraGo, iTween.Hash("x", 0.5f,"time", 3.0f));
        //iTween.ShakePosition(mainCameraGo, iTween.Hash("y", 0.5f,"time", 3.0f));
        //iTween.ShakePosition(mainCameraGo, iTween.Hash("z", 0.5f,"time", 3.0f));
        iTween.ShakePosition(mainCameraGo, iTween.Hash("x", 0.5f, "y", 0.5f, "time", 3.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCameraGo = _mainCameraGo;
        timeCnt = 0.0f;
        FlowItem.laneSpeedUpFlag = false;
        isLaneActive = true;   //アイテムの生成

        //--- ステージデータの読み込み ---
        for(int i = 0; i < stageDatas.Length; i++)
            LoadTextAsset(i);
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
        if(isLaneActive)
            CreateDragItem();

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ReCreateStage(0);
        }
    }

    //---- ステージの再編成 ----
    public void ReCreateStage(int lapNum)
    {
        //--- 既存のレーンの削除 ---
        Lane.LanesClose();

        //--- レーンを新規作成 ---
        for(int i = 0; i < loadStageDatas[lapNum].laneTransform.Count; i++)
        {
            GameObject go = Instantiate(lanePrefav, loadStageDatas[lapNum].laneTransform[i].pos, 
                loadStageDatas[lapNum].laneTransform[i].rot);
            go.name = i.ToString("D2");
            go.transform.parent = this.transform;
            go.transform.localScale = loadStageDatas[lapNum].laneTransform[i].scale;
            go.GetComponent<Lane>().laneVelocity = loadStageDatas[lapNum].laneVelocity[i];
        }

        //--- 他の位置を調整 ---
        //プレスポジション
        for (int i = 0; i < loadStageDatas[lapNum].pressPosTransform.Count; i++)
        {
            pressPosObj[i].transform.position = loadStageDatas[lapNum].pressPosTransform[i].pos;
            pressPosObj[i].transform.rotation = loadStageDatas[lapNum].pressPosTransform[i].rot;
            pressPosObj[i].transform.localScale = loadStageDatas[lapNum].pressPosTransform[i].scale;
        }
        //スポーン地点
        spawnPosObj.transform.position = loadStageDatas[lapNum].spawnPosTransform.pos;
        spawnPosObj.transform.rotation = loadStageDatas[lapNum].spawnPosTransform.rot;
        spawnPosObj.transform.localScale = loadStageDatas[lapNum].spawnPosTransform.scale;
        //ゴール地点
        goalPosObj.transform.position = loadStageDatas[lapNum].goalPosTransform.pos;
        goalPosObj.transform.rotation = loadStageDatas[lapNum].goalPosTransform.rot;
        goalPosObj.transform.localScale = loadStageDatas[lapNum].goalPosTransform.scale;
        //仕切り
        wallObj.transform.position = loadStageDatas[lapNum].wallTransform.pos;
        wallObj.transform.rotation = loadStageDatas[lapNum].wallTransform.rot;
        wallObj.transform.localScale = loadStageDatas[lapNum].wallTransform.scale;
        //プレスマシーン
        pressMachineObj.transform.position = loadStageDatas[lapNum].pressMachineTransform.pos;
        pressMachineObj.transform.rotation = loadStageDatas[lapNum].pressMachineTransform.rot;
        pressMachineObj.transform.localScale = loadStageDatas[lapNum].pressMachineTransform.scale;
    }

    //---- 薬のアイテムを生成 ---
    void CreateDragItem()
    {
        timeCnt += Time.deltaTime;
        if (timeCnt >= TIME_INTERVAL_CREATE)
        {
            timeCnt -= TIME_INTERVAL_CREATE;
            if (createWaitItems.Count > 0)
            {
                GameObject go = Instantiate(items[Random.Range(0, items.Length)], SpawnPosition.position, Quaternion.identity);
                go.GetComponent<FlowItem>().FirstSet(this, createWaitItems[0], itemMaterials[(int)createWaitItems[0]]);
                go.GetComponent<FlowItem>().dissolvClass = dissolveObj.GetComponent<Dissolver>();
                dissolveObj.transform.parent = go.transform;
                createWaitItems.RemoveAt(0);
            }
        }
    }

    //---- テキストデータの読み込み ----
    void LoadTextAsset(int post)
    {
        string loadText;
        string[] splitText;
        List<string[]> liSplit = new List<string[]>();
        tStageData data = new tStageData();
        data.laneTransform = new List<tTransData>();
        data.pressPosTransform = new List<tTransData>();
        data.laneVelocity = new List<Vector3>();

        //--- テキストデータの基礎読み込み ---
        loadText = stageDatas[post].text;

        //--- テキストデータの分割 ---
        splitText = loadText.Split(char.Parse("\n"));
        for (int index = 0; index < splitText.Length; index++)
        {
            //最後に改行コードがはいっているので無視する。
            splitText[index] = splitText[index].Replace("\r", "");

            //()を無視する
            splitText[index] = splitText[index].Replace("(", "");
            splitText[index] = splitText[index].Replace(")", "");

            //半角スペースを無視する
            splitText[index] = splitText[index].Replace(" ", "");

            //さらに,で区切って収納
            string[] work = splitText[index].Split(char.Parse(","));
            liSplit.Add(work);
        }

        //--- テキストデータをステージデータとして読み込む ---
        eStageDataLoadState state = eStageDataLoadState.lane;
            tTransData trans = new tTransData();
        for (int index = 0; index < splitText.Length; index++)
        {
            //改行はスルー
            if (splitText[index] == "")
                continue;

            //ステートチェック
            state = CheckState(splitText[index], state);

            //各ステートごとに入力
            switch (state)
            {
                case eStageDataLoadState.lanePos:
                    index++;
                    trans.pos = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;
                case eStageDataLoadState.laneRot:
                    index++;
                    trans.rot = new Quaternion(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), 
                        float.Parse(liSplit[index][2]), float.Parse(liSplit[index][3]));
                    break;
                case eStageDataLoadState.laneSize:
                    index++;
                    trans.scale = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    data.laneTransform.Add(trans);
                    break;
                case eStageDataLoadState.laneVel:
                    index++;
                    data.laneVelocity.Add(new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2])));
                    break;

                case eStageDataLoadState.pressposPos:
                    index++;
                    trans.pos = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;
                case eStageDataLoadState.pressposRot:
                    index++;
                    trans.rot = new Quaternion(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]),
                        float.Parse(liSplit[index][2]), float.Parse(liSplit[index][3]));
                    break;
                case eStageDataLoadState.pressposSize:
                    index++;
                    trans.scale = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    data.pressPosTransform.Add(trans);
                    break;

                case eStageDataLoadState.spwanposPos:
                    index++;
                    data.spawnPosTransform.pos = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;

                case eStageDataLoadState.goalposPos:
                    index++;
                    data.goalPosTransform.pos = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;
                case eStageDataLoadState.goalposRot:
                    index++;
                    data.goalPosTransform.rot = new Quaternion(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]),
                        float.Parse(liSplit[index][2]), float.Parse(liSplit[index][3]));
                    break;
                case eStageDataLoadState.goalposSize:
                    index++;
                    data.goalPosTransform.scale = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;

                case eStageDataLoadState.wallPos:
                    index++;
                    data.wallTransform.pos = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;
                case eStageDataLoadState.wallRot:
                    index++;
                    data.wallTransform.rot = new Quaternion(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]),
                        float.Parse(liSplit[index][2]), float.Parse(liSplit[index][3]));
                    break;
                case eStageDataLoadState.wallSize:
                    index++;
                    data.wallTransform.scale = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;

                case eStageDataLoadState.pressmachinePos:
                    index++;
                    data.pressMachineTransform.pos = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;
                case eStageDataLoadState.pressmachineRot:
                    index++;
                    data.pressMachineTransform.rot = new Quaternion(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]),
                        float.Parse(liSplit[index][2]), float.Parse(liSplit[index][3]));
                    break;
                case eStageDataLoadState.pressmachineSize:
                    index++;
                    data.pressMachineTransform.scale = new Vector3(float.Parse(liSplit[index][0]), float.Parse(liSplit[index][1]), float.Parse(liSplit[index][2]));
                    break;
            }
        }

        loadStageDatas.Add(data);
    }

    eStageDataLoadState CheckState(string dat, eStageDataLoadState now)
    {
        if (dat == "<LANE>")
            return eStageDataLoadState.lane;
        else if (dat == "<PRESSPOS>")
            return eStageDataLoadState.presspos;
        else if (dat == "<SPAWNPOS>")
            return eStageDataLoadState.spwanpos;
        else if (dat == "<PRESSMACHINE>")
            return eStageDataLoadState.pressmachine;
        else if (dat == "<WALL>")
            return eStageDataLoadState.wall;
        else if (dat == "<GOALPOINT>")
            return eStageDataLoadState.goalpos;

        else if (dat == "<pos>")
        {
            if(now == eStageDataLoadState.lane || now == eStageDataLoadState.laneVel)
                return eStageDataLoadState.lanePos;
            if (now == eStageDataLoadState.presspos || now == eStageDataLoadState.pressposSize)
                return eStageDataLoadState.pressposPos;
            if (now == eStageDataLoadState.spwanpos)
                return eStageDataLoadState.spwanposPos;
            if (now == eStageDataLoadState.pressmachine)
                return eStageDataLoadState.pressmachinePos;
            if (now == eStageDataLoadState.wall)
                return eStageDataLoadState.wallPos;
            if (now == eStageDataLoadState.goalpos)
                return eStageDataLoadState.goalposPos;
        }
        else if (dat == "<rot>")
        {
            if (now == eStageDataLoadState.lanePos)
                return eStageDataLoadState.laneRot;
            if (now == eStageDataLoadState.pressposPos)
                return eStageDataLoadState.pressposRot;
            if (now == eStageDataLoadState.pressmachinePos)
                return eStageDataLoadState.pressmachineRot;
            if (now == eStageDataLoadState.wallPos)
                return eStageDataLoadState.wallRot;
            if (now == eStageDataLoadState.goalposPos)
                return eStageDataLoadState.goalposRot;
        }
        else if (dat == "<scale>")
        {
            if (now == eStageDataLoadState.laneRot)
                return eStageDataLoadState.laneSize;
            if (now == eStageDataLoadState.pressposRot)
                return eStageDataLoadState.pressposSize;
            if (now == eStageDataLoadState.pressmachineRot)
                return eStageDataLoadState.pressmachineSize;
            if (now == eStageDataLoadState.wallRot)
                return eStageDataLoadState.wallSize;
            if (now == eStageDataLoadState.goalposRot)
                return eStageDataLoadState.goalposSize;
        }
        else if (dat == "<velocity>")
            return eStageDataLoadState.laneVel;

            return now;
    }


}
