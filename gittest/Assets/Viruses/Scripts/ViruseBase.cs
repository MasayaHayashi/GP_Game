using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ウィルス
// Author : MasayaHayashi

public class ViruseBase : MonoBehaviour
{
    [SerializeField, Header("ウィルス用オブジェクト")]
    private ViruseData viruseObj;

    private ViruseData nextViruseObj;               // 次の進化先

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
        if(Input.GetKeyDown(KeyCode.A))
        {
            // 演出開始
            StartCoroutine(StartEvolution(0));
        }
    }

    // 進化開始
    private IEnumerator StartEvolution(ViruseData.EvolutionType index)
    {
        Debug.Log("演出中");

        yield return new WaitForSeconds(EffectTime);

        // 次の進化先登録
        nextViruseObj = viruseObj.nextEvolutions[(int)index];
        viruseObj = nextViruseObj;

        // 画像変更
        changeSprite();
    }

    private void changeSprite()
    {
        gameObject.GetComponent<Image>().sprite = viruseObj.sprites[0];
    }

}
