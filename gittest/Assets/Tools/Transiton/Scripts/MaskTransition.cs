﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マスク画像によるトランジション
/// 加藤　遼
/// </summary>
public class MaskTransition : TransitonBase
{

    override protected IEnumerator TransIn(float time)
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

    override protected IEnumerator TransOut(float time)
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
