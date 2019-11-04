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

    private GameObject  nextViruseGameObj;          // 次の進化先

    private const float EffectTime  = 2.0f;         // 演出時間
    private bool        isEndEffect = false;        // 進化演出が終了したか

    private Animator[] childAnims;
    private List<RuntimeAnimatorController> nextChildAnimControllers = new List<RuntimeAnimatorController>();

    private void Awake()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            childAnims = GetComponentsInChildren<Animator>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
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
        nextViruseGameObj = viruseObj.nextEvolutions[(int)index];



        viruseObj = nextViruseGameObj.GetComponent<ViruseBase>().viruseObj;

        // 次のアニメーター登録
        registerNextAnimator();


        // 画像変更
        changeSprite();

        // スクリプト更新
        //gameObject.AddComponent<>
    }

    private void changeSprite()
    {

        foreach (Animator childAnim in childAnims)
        {
            foreach (RuntimeAnimatorController nextChildAnim in nextChildAnimControllers)
            {
                childAnim.runtimeAnimatorController = null;
                childAnim.runtimeAnimatorController = nextChildAnim;
            }
        }
    }

    private void registerNextAnimator()
    {
        Animator[] nextAnimators = nextViruseGameObj.GetComponentsInChildren<Animator>();

        foreach (Animator childAnim in nextAnimators)
        {
            nextChildAnimControllers.Add(childAnim.runtimeAnimatorController);
        }
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
