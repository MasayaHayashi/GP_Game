using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ゲーム進行管理
/// 加藤　遼
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // --- ゲームの状態 ---
    public enum GameState
    {
        Set,
        Analysis,
        Game,
        VirusEvolution,
        Pause,
    }

    [SerializeField] private AnalysisEffectManager m_AnalysisManger;
    [SerializeField] private CameraManager  m_CameraManager;
    [SerializeField] private AgeController m_AgeController;
    [SerializeField] private Character m_ChacterController;
    [SerializeField] private LaneControl m_LaneController;
    [SerializeField] private GameObject m_Recepi;

    private bool m_isEffect; // 演出中

    private GameState m_State = GameState.Set;

    private bool m_DebugMode=true;


#if UNITY_EDITOR
    [SerializeField] Text m_DebugText;

    bool charaMove = true;
    bool laneMove = true;
#endif

    // Start is called before the first frame update
    void Start()
    {
        GameProgressDisable();
    }

    // Update is called once per frame
    void Update()
    {

        switch (m_State)
        {
            case GameState.Set:
                if (!TransitionManager.Instance.m_isTransition)
                    ChangeState(GameState.Analysis);
                break;
            case GameState.Analysis:              
                break;
            case GameState.Game:
                if (m_LaneController.oneGameFin)
                    ChangeState(GameState.VirusEvolution);
                break;
            case GameState.Pause:
                break;
        }



#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_DebugMode = m_DebugMode ? false : true;
            if (m_DebugMode)
                m_DebugText.gameObject.SetActive(true);
            else
                m_DebugText.gameObject.SetActive(false);
        }
    
        m_DebugText.text = "Z Key On/Off:" +"\n"+
                           "GameState:" + m_State.ToString() + "\n"+
                           "PlayerMove:" + charaMove+"\n" +
                           "LaneMove:"+ laneMove+"\n";

        

        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState(GameState.VirusEvolution);
        }

#endif


    }


    // --- 演出処理 ---
    private IEnumerator analysis()
    {
        // --- 演出中 ---
        m_isEffect = true;
        GameProgressDisable();

        // --- カメラの移動 ---
        m_CameraManager.m_Move.ZoomInOut();
        yield return new WaitForSeconds(1.0f);

        // --- 分析開始ダイアログ --- 
        m_AnalysisManger.AnalysisStart();
   
        // --- 演出待ち ---
        while (m_AnalysisManger.IsEffect) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);


        // --- 分析終了ダイアログ --- 
        m_AnalysisManger.AnalysisEnd();
        // --- 演出待ち ---
        while (m_AnalysisManger.IsEffect)
        {
            yield return null;
        }

        // --- カメラの移動 ---
        m_CameraManager.m_Move.ZoomInOut();
        yield return new WaitForSeconds(1.0f);

        // --- 演出終了 ---
        GameProgressEnable();
        ChangeState(GameState.Game);
        m_isEffect = false;
        yield return null;

    }
    private IEnumerator virusEvolution()
    {
        // --- 演出中 ---
        m_isEffect = true;
        GameProgressDisable();

        // --- カメラの移動 ---
        m_CameraManager.m_Move.ZoomInOut();
        yield return new WaitForSeconds(1.0f);

        // --- 分析開始ダイアログ --- 
        m_CameraManager.m_Effect.TimeTravel();
        m_AgeController.ChangeAge(3.0f);

        yield return new WaitForSeconds(3.0f);
        // --- カメラの移動 ---
        m_CameraManager.m_Move.ZoomInOut();

        // --- 演出終了 ---
        m_isEffect = false;
        yield return null;
        ChangeState(GameState.Analysis);
        
    }

    public void ChangeState(GameState state)
    {
        m_State = state;

        // --- 切り替え時一度のみ呼び出し ---
        switch (state)
        {
            case GameState.Set:
                break;
            case GameState.Analysis:
                StartCoroutine(analysis());
                break;
            case GameState.Game:
                m_LaneController.NextStage(0);      //ここで送る数字でどれだけ進化するか変わる 
                                                                    //初回は絶対0を入れる
                                                                    //それ以降は成功度に応じて1~3の数字を入れる
                break;
            case GameState.VirusEvolution:
                StartCoroutine(virusEvolution());
                break;
            case GameState.Pause:
                break;
        }

    }

    // ---- ゲーム進行中かどうか ---
    public void GameProgressEnable()
    {
        m_ChacterController.SetInputActive(true);
        m_LaneController.SetLaneActive(true);
        m_Recepi.SetActive(true);

        charaMove = true;
        laneMove = true;
    }

    public void GameProgressDisable()
    {
        m_ChacterController.SetInputActive(false);
        m_LaneController.SetLaneActive(false);
        m_Recepi.SetActive(false);

        charaMove = false;
        laneMove = false;
    }


}
