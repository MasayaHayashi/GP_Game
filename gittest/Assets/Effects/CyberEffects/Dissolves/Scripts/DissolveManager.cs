using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
    // Author : MasayaHayashi
    // ディゾルブ管理

    [SerializeField, Header("Mask用プレハブ")]
    private GameObject maskPrefab;

    // Start is called before the first frame update
    void Start()
    {
        instantateMaskObj();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void instantateMaskObj()
    {
        Instantiate(maskPrefab, gameObject.transform);
    }
}
