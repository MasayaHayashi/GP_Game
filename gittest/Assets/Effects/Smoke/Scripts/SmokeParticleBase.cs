using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticleBase : MonoBehaviour
{
    private Hashtable hash = new Hashtable();

    private Vector3 startPosition;
    private Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initilize()
    {
        
    }
}
