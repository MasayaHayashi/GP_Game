using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// エディター拡張用
// Author MasayaHayashi

[CustomEditor(typeof(ViruseData))]
public class MyEditorExpansion : Editor
{
    //　カスタマイズするクラスを設定
//    [CustomEditor(typeof(ViruseData))]
    // OnEnableメソッド内等で記述し、MyDataのhpプロパティの取得
//    serializedObject.FindProperty("hp");
 
    //　OnInspectorGUIメソッド内で記述し、プロパティ値をインスペクタに表示
//    EditorGUILayout.PropertyField(hp);
}