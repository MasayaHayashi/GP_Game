using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ウィルス用データ
// Author : MasayaHayashi

[CreateAssetMenu(menuName = "MyScriptable/Create ViruseData")]
public class ViruseData : ScriptableObject
{
    [SerializeField, Header("ウィルス名")]
    public string name;

    [SerializeField, Header("ウィルス画像状態名")]
    public static List<string> spriteNames = new List<string>
    {
        "通常状態",
        "ダメージ状態",
    };

    [SerializeField, Header("次の進化先(プレハブ)")]
    public List<GameObject> nextEvolutions;

    public int id;

    private Dictionary<string, Sprite> spriteDatas = new Dictionary<string,Sprite>();

    private ViruseResultData resultData;
    public ViruseResultData getResultData { get { return resultData; } }

    private void Awake()
    {
        
    }

    public enum EvolutionType
    {
        GOOD = 0,
        VERY_GOOD,
        PARFECT,
    }


}
