using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeControl : MonoBehaviour
{
    [SerializeField] GameObject recipeUiPrefav;
    [SerializeField] LaneControl laneControlClass;

    struct tRecipeLevel
    {
        public int recipeNum;   //何個のレシピがあるか
        public int itemNum;     //何個のアイテムが出現するか
        public bool disturb;      //おじゃまが登場するか
    }

    //仮定数　ウイルスのレベル部分とマージしたらそっちに
    const int maxLv = 3;
    int recipeLv = 0;       //レシピレベル　ウイルスのレベルと一緒

    //レシピテーブル
    tRecipeLevel[] recipeTable = new tRecipeLevel[maxLv];
    Color[] uiItemColors = new Color[(int)FlowItem.eItemType.MAX];  //からーてーぶる

    //作成されたレシピ
    List<FlowItem.eItemType[]> recipes = new List<FlowItem.eItemType[]>();
    int recipeIndex;    //挑戦中のレシピの番号
    int itemIndex;

    //表示UI
    List<Recipe> recipeUis = new List<Recipe>();
    
    //正誤判定をとっておく
    //レシピリストを更新するタイミングで情報は失われます。
    List<int> correctDatas = new List<int>();
    int GetCorrectData() { return correctDatas[recipeIndex - 1]; }  //4個中何個正解したか
    List<int> GetAllCorrectDatas() { return correctDatas; }

    //正誤演出
    int upperIndex = 0;
    bool isScrollWait;

    // Start is called before the first frame update
    void Start()
    {
        recipeIndex = 0;
        itemIndex = 0;
        isScrollWait = false;

        CreateRecipeTable();    //とりあえずここで。

        CreateRecipe();     //レシピの作成
    }

    // Update is called once per frame
    void Update()
    {
        if (isScrollWait)
        {
            if (recipeUis[upperIndex].FinCallFlashAnim())
            {
                isScrollWait = false;
                for (int i = 0; i < recipeUis.Count; i++)
                {
                    recipeUis[i].UpPos();
                }
            }
        }
    }

    void CreateRecipeTable()
    {
        recipeTable[0].recipeNum = 5;
        recipeTable[0].itemNum = 4;
        recipeTable[0].disturb = false;
        recipeTable[1].recipeNum = 10;
        recipeTable[1].itemNum = 5;
        recipeTable[1].disturb = false;
        recipeTable[2].recipeNum = 15;
        recipeTable[2].itemNum = 6;
        recipeTable[2].disturb = true;

        //color
        uiItemColors[(int)FlowItem.eItemType.pink] = Color.magenta;
        uiItemColors[(int)FlowItem.eItemType.white] = Color.white;
        uiItemColors[(int)FlowItem.eItemType.black] = Color.black;
        uiItemColors[(int)FlowItem.eItemType.red] = Color.red;
        uiItemColors[(int)FlowItem.eItemType.green] = Color.green;
        uiItemColors[(int)FlowItem.eItemType.blue] = Color.blue;
    }

    void CreateRecipe()
    {
        recipes.Clear();
        correctDatas.Clear();

        //レシピの作成
        for (int i = 0; i < recipeTable[recipeLv].recipeNum; i++)
        {
            recipes.Add(new FlowItem.eItemType[4]);
            correctDatas.Add(0);
            for (int j = 0; j < 4; j++)
            {
                recipes[i][j] = (FlowItem.eItemType)Random.Range(0, recipeTable[recipeLv].itemNum);
            }
        }

        //初期表示のレシピの作成(下に待機させる分を含めて4つ)
        GameObject go;
        float posy = 2.3f;
        for(int i = 0; i < 4; i++)
        {
            if (recipes.Count - 1 < i)
                return;
            go = Instantiate(recipeUiPrefav, Vector3.zero, Quaternion.identity);
            go.transform.parent = transform;
            go.transform.localScale = new Vector3(7.0f, 1.0f, 1.0f);
            go.transform.localPosition = new Vector3(0.0f, posy, 0.0f);
            go.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            recipeUis.Add(go.GetComponent<Recipe>());
            recipeUis[i].SetColors(uiItemColors[(int)recipes[i][0]], uiItemColors[(int)recipes[i][1]], uiItemColors[(int)recipes[i][2]], uiItemColors[(int)recipes[i][3]]);
            posy -= 2.3f;
        }

        //初回の情報をレーンに送る
        ItemLaneAdd();
    }

    void ItemLaneAdd()
    {
        int num = 4;

        //おじゃまアイテムの出現
        int rand = 0;
        if (recipeTable[recipeLv].disturb)
        {
            rand = Random.Range(0, 3);
            //rand = 1;
            num += rand;
        }

        //出現順序のシャッフル
        FlowItem.eItemType[] workTypes = new FlowItem.eItemType[num];
        FlowItem.eItemType[] workTypesDat = new FlowItem.eItemType[num];
        bool[] workFlag = new bool[num];

        for (int i = 0; i < 4; i++)
            workTypesDat[i] = recipes[recipeIndex][i];
        for (int i = 0; i < rand; i++)  //おじゃまアイテムをリストに追加
            workTypesDat[4 + i] = FlowItem.eItemType.disturb;

        int workRand = 0;
        for (int i = 0; i < num; i++)
        {
            do
            {
                workRand = Random.Range(0, num);
            } while (workFlag[workRand]);
            workFlag[workRand] = true;
            workTypes[i] = workTypesDat[workRand];
        }

        //シャッフルされた結果をレーン側に送る
        for (int i = 0; i < num; i++)
            laneControlClass.AddCreateList(workTypes[i]);
    }

    public bool ItemGoal(FlowItem.eItemType type)
    {
        bool retVal = false;
        //正解かどうかの情報をとっておく
        if (type == recipes[recipeIndex][itemIndex]) { 
            correctDatas[recipeIndex]++;
            retVal = true;
        }
        else if(type == FlowItem.eItemType.disturb)
        {
            //お邪魔アイテムの場合はフリーズ
            LaneControl.StartBug();
        }

        //正解の場合
        //if (type == recipes[recipeIndex][itemIndex])
        //{
        itemIndex++;

            //4つそろったら次へ
            if (itemIndex > 3)
            {
                itemIndex = 0;
                recipeIndex++;
                if (recipeIndex >= recipes.Count)
                {
                    Debug.Log("ウイルス退治演出へ");
                }
                else
                {
                    if (recipeIndex <= recipes.Count - 4)
                    {
                        GameObject go;
                        go = Instantiate(recipeUiPrefav, Vector3.zero, Quaternion.identity);
                        go.transform.parent = transform;
                        go.transform.localScale = new Vector3(7.0f, 1.0f, 1.0f);
                        go.transform.localPosition = new Vector3(0.0f, -4.6f, 0.0f);
                        go.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                        recipeUis.Add(go.GetComponent<Recipe>());
                        recipeUis[recipeUis.Count - 1].SetColors(uiItemColors[(int)recipes[recipeIndex + 3][0]], 
                            uiItemColors[(int)recipes[recipeIndex + 3][1]], uiItemColors[(int)recipes[recipeIndex + 3][2]], uiItemColors[(int)recipes[recipeIndex + 3][3]]);
                    }

                    //次のアイテムを生成
                    ItemLaneAdd();

                    //レシピリストのクリーン
                    for (int i = 0; i < recipeUis.Count; i++)
                    {
                        if (!recipeUis[i])
                        {
                            recipeUis.RemoveAt(i);
                        }
                    }

                    //--- レシピを上にあげる ---
                    // ここでレシピの演出よんで、演出終わったら上にあげるこの処理呼ぶのがよさそう
                    //一番上にあるレシピに点滅演出を呼ぶ
                    float workY = 0.0f;
                    upperIndex = 0;
                    for(int i = 0; i < recipeUis.Count; i++)
                    {
                        if(workY < recipeUis[i].gameObject.transform.position.y)
                        {
                            workY = recipeUis[i].gameObject.transform.position.y;
                            upperIndex = i;
                        }
                    }
                    recipeUis[upperIndex].StartFlash(GetCorrectData() >= 4);

                    //上にあげる
                    isScrollWait = true;
                    //----------------------
                }
                return retVal;
            }

        return retVal;
        //}
        //不正解の場合はもう一度アイテムを再生成
        //else
        //   laneControlClass.AddCreateList(type);
    }
}
