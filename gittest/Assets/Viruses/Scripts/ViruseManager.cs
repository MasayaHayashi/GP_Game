using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 進化後ウィルス
// Author : MasayaHayashi
public class ViruseManager : MonoBehaviour
{
    [SerializeField, Header("初期ウィルス")]
    public GameObject baseViruse;

    private ViruseBase viruseBaseScript;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        viruseBaseScript = baseViruse.GetComponent<ViruseBase>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 演出開始
            StartCoroutine(viruseBaseScript.StartEvolution((int)ViruseData.EvolutionType.GOOD));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            viruseBaseScript = baseViruse.GetComponent<ViruseBase>();

            viruseBaseScript.changeState("Damage");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            viruseBaseScript = baseViruse.GetComponent<ViruseBase>();

            viruseBaseScript.changeState("Idol");
        }
    }
}
