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


    public IEnumerator startEffect()
    {
        // ダイヤログポップアップ処理
        Debug.Log("Dialogの中StartEffect");
        m_DialogEffect.Popup();
        

        // 処理が終わるまで待機
       // while (m_DialogEffect.IsEffect)
       //     yield return new WaitForSeconds(0);

        // テキストタイピング処理
       // m_TextEffect.Typing();

        // 処理が終わるまで待機
        //while (m_TextEffect.IsEffect)
            yield return new WaitForSeconds(0);
       
    }
 
}
