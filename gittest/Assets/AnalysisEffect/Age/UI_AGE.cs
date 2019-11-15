using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 年代表示
/// 加藤　遼
/// </summary>
public class UI_AGE : MonoBehaviour
{

    [SerializeField] private Text m_AgeText;
    [SerializeField] private int m_CurrentAge = 2019;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentAge++;


        // テキスト更新
        m_AgeText.text = m_CurrentAge.ToString();

    }
}
