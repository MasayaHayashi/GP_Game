using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
   // public string[] scenarios;
    [SerializeField] Text m_UIText;

    [SerializeField]
    private float m_ViewTime;


    private string m_OriginalText;
    private int m_CharacterTotal;

   
    private double m_TimeElapsed;

    private bool m_isEffect;
    public bool IsEffect { get { return m_isEffect; } }



    void Start()
    {
        //SetNextLine();
        m_OriginalText = m_UIText.text;
        m_CharacterTotal = m_UIText.text.Length;
        
 

        m_UIText.text = m_OriginalText.Substring(0, 0);
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && !m_isEffect)
        {
            StartCoroutine(typing());
        }


    }


    public void Typing()
    {
        StartCoroutine(typing());
    }


    public IEnumerator typing()
    {
        m_isEffect = true;

           

        while (m_ViewTime >= m_TimeElapsed)
        {
            m_UIText.text = m_OriginalText.Substring(0, (int)((float)m_CharacterTotal * (m_TimeElapsed / m_ViewTime)));
            m_TimeElapsed += Time.deltaTime;

            yield return new WaitForSeconds(0);

        }

        m_UIText.text = m_OriginalText.Substring(0, m_CharacterTotal);
        m_isEffect = false;
    }



}
