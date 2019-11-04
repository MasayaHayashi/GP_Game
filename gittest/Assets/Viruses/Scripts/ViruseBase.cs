using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ウィルス
// Author : MasayaHayashi

public class ViruseBase : MonoBehaviour
{
    [SerializeField, Header("ウィルス用登録データ（スクリプタブルオブジェクト")]
    private ViruseData  viruseObj;

    private GameObject  nextViruseObj;              // 次の進化先

    private const float EffectTime  = 2.0f;         // 演出時間
    private bool        isEndEffect = false;        // 進化演出が終了したか
    
    
    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        changeSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 進化開始
    public IEnumerator StartEvolution(ViruseData.EvolutionType index)
    {
        Debug.Log("演出中");

        yield return new WaitForSeconds(EffectTime);

        // 次の進化先登録
        nextViruseObj = viruseObj.nextEvolutions[(int)index];

        viruseObj = nextViruseObj.GetComponent<ViruseBase>().viruseObj;

        
        // 画像変更
        changeSprite();

        // スクリプト更新
        //gameObject.AddComponent<>
    }

    private void changeSprite()
    {
       
    }

    // Sprite状態変更
    public void changeState(string changeName)
    {
        Animator[] childAnims = GetComponentsInChildren<Animator>();

        foreach(Animator anim in childAnims)
        {
            anim.SetTrigger(changeName);
        }
    }
}
