using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 年代表示
/// 加藤　遼
/// </summary>
public class AgeController : MonoBehaviour
{

    [SerializeField] private Text m_AgeText;
    [SerializeField] private int m_CurrentAge = 2019;

    private int m_AddAge=50;
    //private float m_ChangeTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        m_AgeText.text = m_CurrentAge.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeAge(3.0f);
        }
    }


    // --- 演出処理 ---
    private IEnumerator changeAge(float time)
    {
        // --- 変更年代を設定 ---
        int targetAge = m_AddAge + m_CurrentAge;
        int value = 0;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            float rate = t * t * (3.0f - 2.0f * t);
            value = (int)(Mathf.Lerp(m_CurrentAge, targetAge, rate)) ;
            m_AgeText.text = value.ToString();
            yield return null;
        }
        m_CurrentAge = value;

    }

    // --- 演出呼び出し ---
    public void ChangeAge(float time)
    {
        StartCoroutine(changeAge(time));
    }







}
