using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GI操作
/// 加藤遼
/// </summary>
public class RealtimeGIController : MonoBehaviour
{

    [SerializeField] bool m_Sprite;

    [SerializeField] private Renderer m_Renderer;
    private Color m_EmissiveColor = new Color(1, 1, 1);
    private float m_Intensity = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (!m_Sprite)
            m_Renderer = GetComponent<MeshRenderer>();
       

        // エディターでセットした値を取得
        m_EmissiveColor = m_Renderer.material.GetColor("_EmissionColor");
    }

    public void OnEnable()
    {
        // エミッションを有効にして、ライトマップ更新
        if (!m_Sprite)
            m_Renderer = GetComponent<MeshRenderer>();
        m_Renderer.material.SetColor("_EmissionColor", m_EmissiveColor * m_Intensity);
        m_Renderer.material.EnableKeyword("_EMISSION");
        m_Renderer.UpdateGIMaterials();
    }

    public void OnDisable()
    {
        // エミッションを無効にして、ライトマップ更新
        if (!m_Sprite)
            m_Renderer = GetComponent<MeshRenderer>();
        m_Renderer.material.SetColor("_EmissionColor", m_EmissiveColor * m_Intensity);
        m_Renderer.material.DisableKeyword("_EMISSION");
        m_Renderer.UpdateGIMaterials();
    }

}
