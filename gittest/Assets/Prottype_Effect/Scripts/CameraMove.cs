using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラ移動
/// 加藤　遼
/// </summary>
public class CameraMove : MonoBehaviour
{

    private Vector3 m_DefPos;
    private Vector3 m_DefRot;
    public Vector3 m_ViewDispPos;
    public Vector3 m_ViewDispRot;

    private bool m_Zoom;

    // Start is called before the first frame update
    void Start()
    {
        m_DefPos = gameObject.transform.position;
        m_DefRot = gameObject.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            Move();
        }

    }

    public void Move()
    {
        if (!m_Zoom)
        {
            iTween.MoveTo(gameObject, m_ViewDispPos, 1.0f);
            iTween.RotateTo(gameObject, m_ViewDispRot, 1.0f);
            m_Zoom = true;
        }
        else
        {
            iTween.MoveTo(gameObject, m_DefPos, 1.0f);
            iTween.RotateTo(gameObject, m_DefRot, 1.0f);
            m_Zoom = false;
        }
    }
}
