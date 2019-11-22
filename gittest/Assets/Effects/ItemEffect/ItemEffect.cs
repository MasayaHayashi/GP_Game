using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : SingletonMonoBehaviour<ItemEffect>
{
    [SerializeField, Header("アイテムに親子付けする用のパーティクル")]
    private List<GameObject> itemParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
