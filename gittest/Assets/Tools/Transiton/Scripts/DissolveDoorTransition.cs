using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トランジション　フェード
/// 加藤　遼
/// </summary>
public class DissolveDoorTransition : TransitonBase
{
    override protected IEnumerator TransIn(float time)
    {
        // -------- 実行中 ----------
        m_running = true;

        // -------- 色パラメーターを取得 ------------
        Color color = m_TransitionImage.color;

        color.a = 0;
        m_TransitionImage.color = color;

        float current = 0;
        while (current < time)
        {
            color.a =  current / time;
            m_TransitionImage.color = color;
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }

        color.a = 1;
        m_TransitionImage.color = color;

        // ---------- 実行終了 ---------
        m_running = false;
        yield return null;
    }

    override protected IEnumerator TransOut(float time)
    {

        // -------- 実行中 ----------
        m_running = true;

        // -------- 色パラメーターを取得 ------------
        Color color = m_TransitionImage.color;

        color.a = 1;
        m_TransitionImage.color = color;

        float current = 0;
        while (current < time)
        {
            color.a = 1 - current / time;
            m_TransitionImage.color = color;
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }

        color.a = 0;
        m_TransitionImage.color = color;

        // ---------- 実行終了 ---------
        m_running = false;
        yield return null;
    }
}
