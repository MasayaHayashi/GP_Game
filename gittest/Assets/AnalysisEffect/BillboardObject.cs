using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビルボード
/// 加藤　遼
/// </summary>
public class BillboardObject : MonoBehaviour
{
    public bool reversX;
    public bool reversY;
    public bool reversZ;

    int x, y, z;

    // Start is called before the first frame update
    void Start()
    {
       
        // --- 反転処理 ---
        x = reversX ? -1 : 1;
        y = reversY ? -1 : 1;
        z = reversZ ? -1 : 1;

        transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z * z);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z * z);

        // --- ビルボード処理 ---
        Vector3 p = Camera.main.transform.position;
         p.x = transform.position.x;
        transform.LookAt(p);
    }
}
