using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分析エフェクト管理
/// 加藤　遼
/// </summary>
public class AnalysisEffectManager : MonoBehaviour
{
    [SerializeField] private List<Dialog> m_Dialogs = new List<Dialog>();

    [SerializeField] private List<GameObject> m_DialogsObj;

    [SerializeField] private GameObject m_Completed;


    private bool m_isEffect;
    public bool IsEffect{ get { return m_isEffect; } }


    // Start is called before the first frame update
    void Start()
    {
        // ----- ダイアログ取得 -----
        for (int i = 0; i < m_DialogsObj.Count; i++)
        {
            m_Dialogs.Add(new Dialog());
            m_Dialogs[i].m_Instance = m_DialogsObj[i];
            m_Dialogs[i].Set();
            m_Dialogs[i].m_Instance.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !m_isEffect)
        {
            StartCoroutine(AnalysisStart());
        }

        if (Input.GetKeyDown(KeyCode.W) && !m_isEffect)
        {
            StartCoroutine(AnalysisEnd());
        }
    }


    public IEnumerator AnalysisStart()
    {
        // ----- 演出中 -----
        m_isEffect = true;
       
        //　数秒毎にダイアログを出現させる
        for (int i = 0; i < m_Dialogs.Count; i++)
        {
            m_Dialogs[i].m_Instance.SetActive(true);
            m_Dialogs[i].StartEffect();

            yield return new WaitForSeconds(0.2f);
        }

        while (m_Dialogs[m_Dialogs.Count - 1].m_DialogEffect.IsEffect)
        {
            yield return new WaitForSeconds(0);
        }

        m_Completed.SetActive(true);

        // ----- 演出終了 -----
        m_isEffect = false;
    }

    public IEnumerator AnalysisEnd()
    {
        // ----- 演出中 -----
        m_isEffect = true;

        //　数秒毎に後ろから順番にダイアログを消す
        for (int i = m_Dialogs.Count-1; i >=0; i--)
        {
            m_Dialogs[i].EndEffect();

            yield return new WaitForSeconds(0.2f);
        }

        // ----- 演出終了 -----
        m_isEffect = false;
    }





}
