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

    [SerializeField] private List<GameObject> m_DialogsTestObj;
    // Start is called before the first frame update
    void Start()
    {
  

        for (int i = 0; i < m_DialogsTestObj.Count; i++)
        {
            m_Dialogs.Add(new Dialog());
            m_Dialogs[i].m_Instance = m_DialogsTestObj[i];
            m_Dialogs[i].Set();
            m_Dialogs[i].m_Instance.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(AnalysisStart());
        }
    }


    public IEnumerator AnalysisStart()
    {
        Debug.Log("演出テスト");

        //　数秒毎にダイアログを出現させる
        for (int i = 0; i < m_Dialogs.Count; i++)
        {
            m_Dialogs[i].m_Instance.SetActive(true);
            StartCoroutine(m_Dialogs[i].startEffect());

            yield return new WaitForSeconds(0.5f);
        }

      //  yield return new WaitForSeconds(0);
    }





}
