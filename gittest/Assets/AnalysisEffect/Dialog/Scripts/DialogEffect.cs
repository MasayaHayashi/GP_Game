using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ウイルス解析ダイアログ演出
/// </summary>
public class DialogEffect : MonoBehaviour
{
    // ---- Private ----
    private Vector3 m_DialogSize;                   // ダイアログのサイズ保持用
    private bool m_isEffect;                        // 演出中かどうか
    private TextEffect m_TextMeshEffect;        // テキストメッシュ演出処理
    private RealtimeGIController m_GIController;    // GIの更新

    // ---- Property ----
    public Vector2 DialogSize { set { m_DialogSize = value; } get { return m_DialogSize; } }
    public bool IsEffect { get { return m_isEffect; } }

    private void Awake()
    {
        // ---- 各種コンポーネントを取得 ---- 
        m_TextMeshEffect = GetComponent<TextEffect>();
        m_GIController = GetComponent<RealtimeGIController>();

        // ---- ダイアログのサイズを保持 ----
        m_DialogSize = gameObject.transform.localScale;

        // ---- ダイアログのスケールを0で初期化 ----
        gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        #region デバッグ用処理
#if UNITY_EDITOR
        //    if (Input.GetKeyDown(KeyCode.Q) && !m_isEffect)
        //    {
        //        StartCoroutine(Popup(0.1f));
        //    }

        //    if (Input.GetKeyDown(KeyCode.W) && !m_isEffect)
        //    {
        //        StartCoroutine(Popdown(0.1f));
        //    }
        //}
#endif
        #endregion
    }

    //　呼び出し用
    public void Popup()
    {
        StartCoroutine(popup(1.0f));
    }　
    public void Popdown()
    {
        StartCoroutine(popdown(1.0f));
    }

    // 演出処理
    public IEnumerator popdown(float time)
    {
        m_isEffect = true;

        Vector3 scale = new Vector3(0, 0, 0);

        iTween.ScaleTo(this.gameObject, scale, time);

        m_GIController.OnDisable();

        while (gameObject.transform.localScale != scale)
        {
            yield return new WaitForSeconds(0);
        }

        m_isEffect = false;

        gameObject.SetActive(false);
    }
    public IEnumerator popup(float time)
    {
        m_isEffect = true;
        m_GIController.OnEnable();

        Vector3 scale = m_DialogSize;

        iTween.ScaleTo(this.gameObject, scale, time);

        while (gameObject.transform.localScale != scale)
        {
            yield return new WaitForSeconds(0);
        }

        m_TextMeshEffect.Typing();

        while (m_TextMeshEffect.IsEffect)
        {
            yield return new WaitForSeconds(0);
        }
        m_isEffect = false;
    }



}
