using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン管理　シングルトン
/// 加藤　遼
/// </summary>
public class TransitionManager : SingletonMonoBehaviour<TransitionManager>
{
    public enum FadeType
    {
        //NONE,
        FADE,
        MASK_01,
    }

    // マスクトランジション機能
    private MaskTransition m_MaskTransition;

    // フェード機能
    private FadeTransition m_FadeTransition;

    //　読み込み率を表示するスライダー
    [SerializeField] private Slider slider;

    public bool m_isTransition;

    // 初期化メソッド(初アクセスまたはAwake時のどちらか一度だけ実行される)
    protected override void Init()
    {
        base.Init();

        // ---------- オブジェクトが破棄されないように --------------
        DontDestroyOnLoad(this.gameObject);

        // ---------- コンポーネント取得 ----------
        m_MaskTransition = GetComponent<MaskTransition>();
        m_FadeTransition = GetComponent<FadeTransition>();

        // ---------- スライダーを隠す ------------
        slider.gameObject.SetActive(false);
    }

    /// <summary>
    /// シーン切り替え
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="time"></param>
    public void ChangeSceneTransiton(string sceneName,float time,FadeType type)
    {
        if (!m_MaskTransition.Running)
            StartCoroutine(LoadScene(sceneName, time, type));
    }

    IEnumerator LoadScene(string sceneName, float time,FadeType type)
    {

        m_isTransition = true;

        TransitonBase transition = null;
        switch (type)
        {
            case FadeType.FADE:
                transition = m_FadeTransition;
                break;
            case FadeType.MASK_01:
                transition = m_MaskTransition;
                break;
        }


        // ----- フェードイン -----
        transition.BeginTransition_In(time);

        // ----- プログレスバー -----
        slider.gameObject.SetActive(true);

        // ----- フェードイン終了まで待つ ----
        while (transition.Running)
        {
            yield return new WaitForEndOfFrame();
        }

        // ----- シーンのロード非同期 -----
        AsyncOperation async =  SceneManager.LoadSceneAsync(sceneName);

        // ----- シーン遷移をストップ ----
        async.allowSceneActivation = false;

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (true)
        {
            yield return null;

            Debug.Log(async.progress);
            slider.value = async.progress;

            if (async.progress >= 0.9f)
            {
                
                slider.value = 1.0f;

                // ----- シーン遷移を許可 ----
                async.allowSceneActivation = true;

                yield return new WaitForSeconds(1.0f);

                break;
            }

        }

        // ----- プログレスバー -----
        slider.gameObject.SetActive(false);

        // ----- フェーアウト -----
        transition.BeginTransition_Out(time);

        // ----- フェードイン終了まで待つ ----
        while (transition.Running)
        {
            yield return new WaitForEndOfFrame();
        }

        m_isTransition = false;
        yield return null;
    }



}
