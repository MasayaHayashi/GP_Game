using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// uGui Textのタイピングエフェクト
/// 加藤遼
/// </summary>
public class TextEffect : MonoBehaviour
{
    // ---- Inspecter ----
    [SerializeField] Text m_UIText;
    [SerializeField] private float m_ViewTime;　// すべてのテキストが表示し終わるまでの時間

    // ---- Private ----
    private string m_OriginalText;
    private int m_CharacterTotal;
    private double m_TimeElapsed;
    private bool m_isEffect;
    public bool IsEffect { get { return m_isEffect; } }

    void Start()
    {
        // --- 原文を保存 ---
        m_OriginalText = m_UIText.text;

        // --- 合計文字数を格納 ---
        m_CharacterTotal = m_UIText.text.Length;

        // --- テキストをクリア ----
        m_UIText.text = m_OriginalText.Substring(0, 0);
    }

    void Update()
    {
        #region デバッグ用処理
#if UNITY_EDITOR

        //if (Input.GetKeyDown(KeyCode.E) && !m_isEffect)
        //{
        //    StartCoroutine(typing());
        //}

#endif
        #endregion
    }

    // --- 呼び出し用関数群 ---
    public void Typing()
    {
        StartCoroutine(typing());
    }

    // --- 演出処理 ---
    public IEnumerator typing()
    {
        // ---- エフェクト実行中 ----
        m_isEffect = true;

        while (m_ViewTime >= m_TimeElapsed)
        {
            // 経過時間のViewTimeの割合によって、表示するテキストを決定
            m_UIText.text = m_OriginalText.Substring(0, (int)((float)m_CharacterTotal * (m_TimeElapsed / m_ViewTime)));

            // 経過時間を加算
            m_TimeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();

        }

        m_UIText.text = m_OriginalText.Substring(0, m_CharacterTotal);

        // ---- エフェクト終了 ----
        m_isEffect = false;
    }



}
