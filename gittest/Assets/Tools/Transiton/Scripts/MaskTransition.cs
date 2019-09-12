using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マスク画像によるトランジション
/// 加藤　遼
/// </summary>
public class MaskTransition : MonoBehaviour
{

    #region ----- Inspector -----
    [SerializeField] private Image m_TransitionImage;
    #endregion

    private bool m_running; 
    public bool Running { get { return m_running; } }

    /// <summary>
    /// トランジション　イン
    /// </summary>
    /// <param name="time"></param>
    public void BeginTransition_In(float time)
    {
        m_TransitionImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        StartCoroutine(TransIn(time));
    }

    /// <summary>
    /// トランジション　アウト
    /// </summary>
    public void BeginTransition_Out(float time)
    {
        m_TransitionImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        StartCoroutine(TransOut(time));
    }

    IEnumerator TransIn(float time)
    {

        // -------- 実行中 ----------
        m_running = true;

        // -------- マテリアルを取得 ------------
        Material mat = m_TransitionImage.material;

        mat.SetFloat("_Alpha", 0);

        float current = 0;
        while (current < time)
        {
            mat.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }

        mat.SetFloat("_Alpha", 1);

        // ---------- 実行終了 ---------
        m_running = false;
        yield return null;
    }

    IEnumerator TransOut(float time)
    {

        // -------- 実行中 ----------
        m_running = true;

        // -------- マテリアルを取得 ------------
        Material mat = m_TransitionImage.material;

        mat.SetFloat("_Alpha", 1);

        float current = 0;
        while (current < time)
        {
            mat.SetFloat("_Alpha", 1-current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }

        mat.SetFloat("_Alpha", 0);

        // ---------- 実行終了 ---------
        m_running = false;
        yield return null;
    }

}
