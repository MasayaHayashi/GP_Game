using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ウイルス解析ダイアログ演出
/// </summary>
public class DialogEffect : MonoBehaviour
{
  
    private Vector3 m_DialogSize; 
    public Vector2 DialogSize { set { m_DialogSize = value; } get { return m_DialogSize; } }

    private bool m_isEffect;   // 演出中かどうか
    public bool IsEffect { get { return m_isEffect; } }

    private TextEffect m_TextEffect;

    private RealtimeGIController m_GIController;


    private void Awake()
    {
        m_TextEffect = GetComponent<TextEffect>();
        m_GIController = GetComponent<RealtimeGIController>();

        m_DialogSize = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //    if (Input.GetKeyDown(KeyCode.Q) && !m_isEffect)
        //    {
        //        StartCoroutine(Popup(0.1f));
        //    }

        //    if (Input.GetKeyDown(KeyCode.W) && !m_isEffect)
        //    {
        //        StartCoroutine(Popdown(0.1f));
        //    }
        //}
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

        m_TextEffect.Typing();

        while (m_TextEffect.IsEffect)
        {
            yield return new WaitForSeconds(0);
        }
        m_isEffect = false;
    }



}
