using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// トランジションべース
/// 加藤　遼
/// </summary>
public class TransitonBase : MonoBehaviour
{

    #region ----- Inspector -----
    [SerializeField] protected Image m_TransitionImage;
    #endregion

    protected bool m_running;
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


    virtual protected IEnumerator TransIn(float time)
    {
        yield return null;
    }

    virtual protected IEnumerator TransOut(float time)
    {
        yield return null;
    }
}
