using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y , transform.localScale.z * -1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.transform.position;
         p.x = transform.position.x;
       
        transform.LookAt(p);

      

        // Quaternion rot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z,transform.rotation.w);

        // transform.rotation = rot ;
    }
}
