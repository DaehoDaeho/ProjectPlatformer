using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// 파일: PauseMenuUI.cs
// 설치: Pause 메뉴 루트 패널에 부착.
// 역할: 일시정지 시 패널을 표시하고, 버튼(Resume/Retry/Exit)을 처리한다.
// 주의: CanvasGroup 으로 입력 차단과 투명도 제어. unscaled 시간 기반 애니메이션을 추가해도 안전.
public class PauseMenuUI : MonoBehaviour
{
    [Header("필수 참조")]
    [SerializeField] private CanvasGroup panelGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_Text titleText;

    [Header("선택: 씬 이름")]
    [SerializeField] private string levelSelectSceneName = "LevelSelect";

    private void Awake()
    {
        // 초기에는 숨김 상태로 설정.
        SetVisible(false);

        // 버튼 리스너 연결.
        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(OnClickedResume);
        }

        if (retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(OnClickedRetry);
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(OnClickedExit);
        }
    }

    //private void OnEnable()
    void Start()
    {
        // GameStateManager 의 이벤트를 구독하여 표시/숨김을 자동으로 처리.
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnPaused += HandlePaused;
            GameStateManager.Instance.OnResumed += HandleResumed;
        }
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnPaused -= HandlePaused;
            GameStateManager.Instance.OnResumed -= HandleResumed;
        }
    }

    private void HandlePaused()
    {
        SetVisible(true);
    }

    private void HandleResumed()
    {
        SetVisible(false);
    }

    // 패널 표시/숨김 공통 처리.
    private void SetVisible(bool show)
    {
        if (panelGroup == null)
        {
            return;
        }

        panelGroup.alpha = show == true ? 1.0f : 0.0f;
        panelGroup.blocksRaycasts = show == true ? true : false;
        panelGroup.interactable = show == true ? true : false;

        if (titleText != null)
        {
            titleText.text = show == true ? "PAUSED" : "";
        }
    }

    private void OnClickedResume()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ResumeGame();
        }
    }

    private void OnClickedRetry()
    {
        // 현재 씬을 다시 불러오기.
        SceneUtil.ReloadCurrentScene();
    }

    private void OnClickedExit()
    {
        // 선택 씬으로 돌아가기.
        if (string.IsNullOrEmpty(levelSelectSceneName) == false)
        {
            SceneUtil.LoadSceneByName(levelSelectSceneName);
        }
    }
}
