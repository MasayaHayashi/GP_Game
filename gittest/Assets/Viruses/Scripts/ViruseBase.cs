using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ウィルス
// Author : MasayaHayashi

public class ViruseBase : MonoBehaviour
{
    [SerializeField, Header("ウィルス用登録データ（スクリプタブルオブジェクト")]
    private ViruseData viruseObj;

    private GameObject nextViruseGameObj;          // 次の進化先

    private const float EffectTime = 2.0f;         // 演出時間
    private bool isEndEffect = false;              // 進化演出が終了したか

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

        if (!nextViruseGameObj)
        {
            yield break;
        }

        viruseObj = nextViruseGameObj.GetComponent<ViruseBase>().viruseObj;

        // 次のアニメーター登録
        registerNextAnimator();

        // 画像変更
        changeSprite();

        // スクリプト変更
        changeScript();

        // 名前変更
        changeName();
    }

    private void changeSprite()
    {

        foreach (var childAnim in childAnims)
        {
            foreach (var nextChildAnim in nextChildAnimControllers)
            {
                childAnim.runtimeAnimatorController = nextChildAnim;
            }
        }
    }

    private void changeScript()
    {
        // スクリプト変更
        var baseScript = nextViruseGameObj.GetComponent<ViruseBase>();
        var attachedScript = gameObject.AddComponent(baseScript.GetType()) as ViruseBase;
        attachedScript.viruseObj = baseScript.viruseObj;

        // 不要なスクリプト削除
        Destroy(this);
    }

    private void changeName()
    {
        gameObject.name = nextViruseGameObj.name;
    }

    private void registerNextAnimator()
    {
        Animator[] nextAnimators = nextViruseGameObj.GetComponentsInChildren<Animator>();

        foreach (var childAnim in nextAnimators)
        {
            nextChildAnimControllers.Add(childAnim.runtimeAnimatorController);
        }
    }

    // Sprite状態変更
    public void changeState(string changeName)
    {
        Animator[] childAnims = GetComponentsInChildren<Animator>();

        foreach (var anim in childAnims)
        {
            anim.SetTrigger(changeName);
        }

    }
}
