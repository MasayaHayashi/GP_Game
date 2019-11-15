using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshEffect : MonoBehaviour
{
    [SerializeField] TextMesh m_TextMesh;
    [SerializeField] private float m_ViewTime;　// すべてのテキストが表示し終わるまでの時間

    // ---- Private ----
    private string m_OriginalText;
    private int m_CharacterTotal;
    private double m_TimeElapsed;
    private bool m_isEffect;
    public bool IsEffect { get { return m_isEffect; } }

    // Start is called before the first frame update
    void Start()
    {
        // --- 原文を保存 ---
        m_OriginalText = m_TextMesh.text;

        // --- 合計文字数を格納 ---
        m_CharacterTotal = m_TextMesh.text.Length;

        // --- テキストをクリア ----
        m_TextMesh.text = m_OriginalText.Substring(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        #region デバッグ用処理
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.E) && !m_isEffect)
        {
            StartCoroutine(typing());
        }

#endif
        #endregion

     //   m_TextMesh.transform.LookAt(Camera.main.transform);
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
            m_TextMesh.text = m_OriginalText.Substring(0, (int)((float)m_CharacterTotal * (m_TimeElapsed / m_ViewTime)));

            // 経過時間を加算
            m_TimeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();

        }

        m_TextMesh.text = m_OriginalText.Substring(0, m_CharacterTotal);

        // ---- エフェクト終了 ----
        m_isEffect = false;
    }
}
