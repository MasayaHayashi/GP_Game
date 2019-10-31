using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ウィルス用データ
// Author:MasayaHayashi

[CreateAssetMenu(menuName = "MyScriptable/Create ViruseData")]
public class ViruseData : ScriptableObject
{
    [SerializeField, Header("ウィルス名")]
    public string name;

    [SerializeField, Header("ウィルス画像")]
    public List<Sprite> sprites;

    [SerializeField, Header("次の進化先")]
    public List<ViruseData> nextEvolutions;

    public enum EvolutionType
    {
        GOOD = 0,
        VERY_GOOD,
        PARFECT,
    }


}
