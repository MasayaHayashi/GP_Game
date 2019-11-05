using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 進化後ウィルス
// Author : MasayaHayashi

public class EvolutionViruse : ViruseBase
{
    private List<GameObject> childParts = new List<GameObject>();

    private void Awake()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            childParts.Add(transform.GetChild(index).gameObject);
        }
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
