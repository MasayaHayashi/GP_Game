using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog 
{

    public GameObject m_Instance;
    private DialogEffect m_DialogEffect;
    private TextEffect m_TextEffect;
    private RealtimeGIController m_GIController;

    // 各種コンポーネントセット
    public void Set()
    {
        m_DialogEffect = m_Instance.GetComponent<DialogEffect>();
        m_TextEffect = m_Instance.GetComponent<TextEffect>();
        m_GIController = m_Instance.GetComponent<RealtimeGIController>();
    }


    public void StartEffect()
    {
        // ダイヤログポップアップ処理
        m_DialogEffect.Popup();
      
    }

    public void EndEffect()
    {        
        // ダイヤログポップダウン処理
        m_DialogEffect.Popdown();

    }
 
}
