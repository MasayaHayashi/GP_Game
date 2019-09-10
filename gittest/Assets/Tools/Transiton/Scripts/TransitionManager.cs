using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン管理　シングルトン
/// </summary>
public class TransitionManager : SingletonMonoBehaviour<TransitionManager>
{

    private MaskTransition m_MaskTransition;

    //初期化メソッド(初アクセスまたはAwake時のどちらか一度だけ実行される)
    protected override void Init()
    {
        base.Init();

        // ---------- オブジェクトが破棄されないように --------------
        DontDestroyOnLoad(this.gameObject);

        // ---------- コンポーネント取得 ----------
        m_MaskTransition = GetComponent<MaskTransition>();
    }


    public void ChangeSceneTransiton(string sceneName,float time)
    {
        StartCoroutine(LoadScene(sceneName, time));
    }

    IEnumerator LoadScene(string sceneName, float time)
    {
        // ----- フェードイン -----
        m_MaskTransition.BeginTransition_In(time);

        // ----- フェードイン終了まで待つ ----
        while (m_MaskTransition.Running)
        {
            yield return new WaitForEndOfFrame();
        }


        // ----- シーンのロード -----
        SceneManager.LoadScene(sceneName);


        // ----- フェーアウト -----
        m_MaskTransition.BeginTransition_Out(time);

        // ----- フェードイン終了まで待つ ----
        while (m_MaskTransition.Running)
        {
            yield return new WaitForEndOfFrame();
        }


        yield return null;
    }

}
