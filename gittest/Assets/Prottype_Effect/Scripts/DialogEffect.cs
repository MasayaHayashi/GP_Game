using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ウイルス解析ダイアログ演出
/// </summary>
public class DialogEffect : MonoBehaviour
{
  
    private Image m_Background;

    [SerializeField] private Vector2 dialogSize;
   

    private void Awake()
    {
        m_Background = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
