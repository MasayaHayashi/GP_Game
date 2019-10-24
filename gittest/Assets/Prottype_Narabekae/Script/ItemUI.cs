using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    //--- 定数 ---
    const int ITEM_NUM = 4;

    //--- 変数 ---
    [SerializeField] Image[] itemImages;
    [SerializeField] LaneControl laneControlClass;
    [SerializeField] SankakuUi sankakuUiClass;
    Color[] uiItemColors = new Color[(int)FlowItem.eItemType.MAX];

    FlowItem.eItemType[] itemTypes = new FlowItem.eItemType[ITEM_NUM];
    int itemIndex;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        itemIndex = 0;

        //color
        uiItemColors[(int)FlowItem.eItemType.pink] = Color.magenta;
        uiItemColors[(int)FlowItem.eItemType.white] = Color.white;
        uiItemColors[(int)FlowItem.eItemType.black] = Color.black;
        uiItemColors[(int)FlowItem.eItemType.red] = Color.red;
        uiItemColors[(int)FlowItem.eItemType.green] = Color.green;
        uiItemColors[(int)FlowItem.eItemType.blue] = Color.blue;

        //初回レシピ作成
        CreateRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateRecipe()
    {
        for(int i = 0; i < ITEM_NUM; i++)
        {
            itemTypes[i] = (FlowItem.eItemType)Random.Range(0, (int)FlowItem.eItemType.MAX);
            itemImages[i].color = uiItemColors[(int)itemTypes[i]];
        }

        //出現順序のシャッフル
        FlowItem.eItemType[] workTypes = new FlowItem.eItemType[ITEM_NUM];
        bool[] workFlag = new bool[ITEM_NUM];
        int workRand = 0;
        for (int i = 0; i < ITEM_NUM; i++)
        {
            do
            {
                workRand = Random.Range(0, ITEM_NUM);
            } while (workFlag[workRand]);
            workFlag[workRand] = true;
            workTypes[i] = itemTypes[workRand];
        }

        //シャッフルされた結果をレーン側に送る
        for (int i = 0; i < ITEM_NUM; i++)
            laneControlClass.AddCreateList(workTypes[i]);
    }

    public void ItemGoal(FlowItem.eItemType type)
    {
        //UIのアニメーション
        sankakuUiClass.StartAnim(type == itemTypes[itemIndex]);

        //正解の場合
        if (type == itemTypes[itemIndex])
        {
            itemIndex++;

            //全問正解で次へ
            if(itemIndex > 3)
            {
                itemIndex = 0;
                CreateRecipe();
                sankakuUiClass.SetPosX(itemImages[itemIndex].transform.position.x);
                return;
            }

            //UIのカーソル位置を変更
            sankakuUiClass.SetPosX(itemImages[itemIndex].transform.position.x);
        }
        //不正解の場合はもう一度アイテムを再生成
        else
            laneControlClass.AddCreateList(type);
    }
}
