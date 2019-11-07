using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompletedEffect : MonoBehaviour
{
    public Text m_Text;
    private Material m_Material;
    public bool m_isFlashing=true;
    public bool m_Plus;
    public double m_Elapsed;
    public float m_FlashRate=2.0f;

    private Color m_TextColor;
    private Color m_MatColor;

    // Start is called before the first frame update
    void Start()
    {
        
        m_Material = GetComponent<Renderer>().material;
        m_TextColor = m_Text.color;
        m_MatColor = m_Material.color;
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFlashing)
        {
          
            if (m_Plus)
            {
                m_Elapsed += Time.deltaTime;
                if(m_Elapsed>= m_FlashRate)
                {
                    m_Plus = false; 
                    m_Text.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, 0);
                    m_Material.EnableKeyword("_Color"); //キーワードの有効化を忘れずに
                    m_Material.SetColor("_Color", new Color(m_MatColor.r, m_MatColor.g, m_MatColor.b, 0));
                    //m_Material.color = new Color(m_MatColor.r, m_MatColor.g, m_MatColor.b, 0);




                }
            }
            else
            {
                m_Elapsed -= Time.deltaTime;
                if (m_Elapsed <= 0)
                {
                    m_Plus = true;
                    m_Text.color = new Color(m_TextColor.r, m_TextColor.g, m_TextColor.b, 1);
                    m_Material.EnableKeyword("_Color"); //キーワードの有効化を忘れずに
                    m_Material.SetColor("_Color", new Color(m_MatColor.r, m_MatColor.g, m_MatColor.b, 1)) ;
                    //m_Material.color = new Color(m_MatColor.r, m_MatColor.g, m_MatColor.b, 1);

                }
            }

           

        }
        else
        {
            m_Text.color = new Color(1, 1, 1, 1);
        }
    }

    public void Flashing()
    {

    }

}
