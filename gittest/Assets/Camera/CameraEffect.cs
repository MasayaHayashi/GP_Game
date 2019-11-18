using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// カメラエフェクト
/// 加藤　遼
/// </summary>
public class CameraEffect : MonoBehaviour
{
    [Header("色収差強度")]
    [SerializeField]private float m_ChromaticAberrationIntensity;
    [Header("タイムトラベル用エフェクト")]
    [SerializeField] private GameObject m_TravelEffect;

    private PostProcessingBehaviour m_PostProcess;

    private ChromaticAberrationModel.Settings m_ChromaticAberrationSettings;
    private DepthOfFieldModel.Settings m_DepthOfFildSettings;

    private DepthOfFieldModel.Settings m_DepthOfFildAfterEffectSettings;
    private DepthOfFieldModel.Settings m_DepthOfFildBeforeEffectSettings;

    


#if UNITY_EDITOR
    public EditorApplication.CallbackFunction OnPlayModeStateChanged { get; private set; }
#endif

    private void Awake()
    {
        m_PostProcess = GetComponent<PostProcessingBehaviour>();
    }

    private void Reset()
    {
        // --- 色収差 ---
        m_ChromaticAberrationSettings.intensity = 0.0f;

        // --- 被写界深度 ---
        m_DepthOfFildSettings = m_DepthOfFildBeforeEffectSettings;

        // --- 設定を反映 ---
        m_PostProcess.profile.chromaticAberration.settings = m_ChromaticAberrationSettings;
        m_PostProcess.profile.depthOfField.settings = m_DepthOfFildSettings;

        // --- パーティクルoff ---
        m_TravelEffect.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        // --- 被写界深度 ---
        // 演出前
        m_DepthOfFildBeforeEffectSettings.focusDistance = 2.0f;
        m_DepthOfFildBeforeEffectSettings.aperture = 0.3f;
        m_DepthOfFildBeforeEffectSettings.focalLength = 5.8f;
        // 演出後
        m_DepthOfFildAfterEffectSettings.focusDistance = 0.7f;
        m_DepthOfFildAfterEffectSettings.aperture = 0.3f;
        m_DepthOfFildAfterEffectSettings.focalLength = 5.8f;

        // --- リセット ---
        Reset();


#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += LogPlayModeState;
#endif


    }

    // Update is called once per frame
    void Update()
    {
 

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(depthOfFild_Liner(1.5f));
            StartCoroutine(chromaticAberration_Liner(1.5f));
            m_TravelEffect.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(depthOfFild_Liner_Back(1.5f));
            StartCoroutine(chromaticAberration_Liner_Back(1.5f));
        }
#endif
    }


    // --- 演出処理 ---
    public IEnumerator chromaticAberration_Liner(float time)
    {
        float t = 0;
        while (t < 1)
        {
            m_ChromaticAberrationSettings.intensity = Mathf.Lerp(0, 1.3f, t);
            t += Time.deltaTime / time;
            m_PostProcess.profile.chromaticAberration.settings = m_ChromaticAberrationSettings;
            yield return null;
        }
        m_ChromaticAberrationSettings.intensity = 1.3f;
        m_PostProcess.profile.chromaticAberration.settings = m_ChromaticAberrationSettings;

        yield return null;
    }
    public IEnumerator chromaticAberration_Liner_Back(float time)
    {
        float t = 1;
        while (t > 0)
        {
            m_ChromaticAberrationSettings.intensity = Mathf.Lerp(0, 1.3f, t);
            t -= Time.deltaTime / time;
            m_PostProcess.profile.chromaticAberration.settings = m_ChromaticAberrationSettings;
            yield return null;
        }
        m_ChromaticAberrationSettings.intensity = 0.0f;
        m_PostProcess.profile.chromaticAberration.settings = m_ChromaticAberrationSettings;
        m_TravelEffect.SetActive(false);
        yield return null;
    }

    public IEnumerator depthOfFild_Liner(float time)
    {
        float t = 0;
        while (t < 1)
        {
           
            m_DepthOfFildSettings.focusDistance = Mathf.Lerp(m_DepthOfFildBeforeEffectSettings.focusDistance, 
                m_DepthOfFildAfterEffectSettings.focusDistance, t);
            m_DepthOfFildSettings.aperture = 0.3f;
            m_DepthOfFildSettings.focalLength = 5.8f;
            t += Time.deltaTime / time; ;
            m_PostProcess.profile.depthOfField.settings = m_DepthOfFildSettings;
            yield return null;
        }
        m_DepthOfFildSettings.focusDistance = m_DepthOfFildAfterEffectSettings.focusDistance;
        m_PostProcess.profile.depthOfField.settings = m_DepthOfFildSettings;

        yield return null;
    }
    public IEnumerator depthOfFild_Liner_Back(float time)
    {
        float t = 1;
        while (t > 0)
        {
            m_DepthOfFildSettings.focusDistance = Mathf.Lerp(m_DepthOfFildBeforeEffectSettings.focusDistance,
                m_DepthOfFildAfterEffectSettings.focusDistance, t);
            m_DepthOfFildSettings.aperture = 0.3f;
            m_DepthOfFildSettings.focalLength = 5.8f;
            t -= Time.deltaTime / time; ;
            m_PostProcess.profile.depthOfField.settings = m_DepthOfFildSettings;
            yield return null;
        }
        m_DepthOfFildSettings.focusDistance = m_DepthOfFildBeforeEffectSettings.focusDistance;
        m_PostProcess.profile.depthOfField.settings = m_DepthOfFildSettings;

        yield return null;
    }

    // --- 演出処理呼び出し用 ---




#if UNITY_EDITOR
    private void LogPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            Debug.Log("停止状態の終了開始！(実行ボタンを押した)");
        }
        else if (state == PlayModeStateChange.EnteredPlayMode)
        {
            Debug.Log("実行状態になった！");
        }
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            Debug.Log("実行状態の終了開始！(停止ボタンを押した)");
            Reset();
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            Debug.Log("停止状態になった！");
        }
    }
#endif
}
