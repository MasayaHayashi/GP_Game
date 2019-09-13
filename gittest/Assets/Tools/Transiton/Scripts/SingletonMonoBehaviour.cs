using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviourを継承し、初期化メソッドを備えたシングルトンなクラス
/// 注意　初期化処理はInitをオーバーライドして記述　StartやAwakeで記述しない
/// 初期化を、インスタンス取得時に確実に行う
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviourWithInit where T : MonoBehaviourWithInit
{

    //インスタンス
    private static T m_Instance;

    //インスタンスを外部から参照する用(getter)
    public static T Instance {
        get {
            //インスタンスがまだ作られていない
            if (m_Instance == null)
            {

                //シーン内からインスタンスを取得
                m_Instance = (T)FindObjectOfType(typeof(T));

                //シーン内に存在しない場合はエラー
                if (m_Instance == null)
                {
                    Debug.LogError(typeof(T) + " is nothing");
                }
                //発見した場合は初期化
                else
                {
                    m_Instance.InitIfNeeded();
                }
            }
            return m_Instance;
        }
    }

    //=================================================================================
    //初期化
    //=================================================================================

    protected sealed override void Awake()
    {
        //存在しているインスタンスが自分であれば問題なし
        if (this == Instance)
        {
            return;
        }

        //自分じゃない場合は重複して存在しているので、エラー
        Debug.LogError(typeof(T) + " is duplicated");
    }

}

/// <summary>
/// 初期化メソッドを備えたMonoBehaviour
/// </summary>
public class MonoBehaviourWithInit : MonoBehaviour
{

    //初期化したかどうかのフラグ(一度しか初期化が走らないようにするため)
    private bool m_isInitialized = false;

    /// <summary>
    /// 必要なら初期化する
    /// </summary>
    public void InitIfNeeded()
    {
        if (m_isInitialized)
        {
            return;
        }
        Init();
        m_isInitialized = true;
    }

    /// <summary>
    /// 初期化(Awake時かその前の初アクセス、どちらかの一度しか行われない)
    /// </summary>
    protected virtual void Init() { }

    //sealed overrideするためにvirtualで作成
    protected virtual void Awake() { }

}