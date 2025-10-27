using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    // 게임 상태를 표현하는 간단한 열거형.
    // Playing: 정상 진행.
    // Paused: 일시정지. timeScale = 0.0f.
    // SlowMo: 슬로모션. timeScale 이 0과 1 사이의 값.
    public enum GameState
    {
        Playing = 0,
        Paused = 1,
        SlowMo = 2
    }

    // 전역 접근이 필요할 수 있으므로 싱글톤처럼 접근할 수 있는 참조 제공.
    // (교육용 간편 패턴. 대규모 프로젝트에서는 의존성 주입을 고려.)
    public static GameStateManager Instance;

    // 상태 변경을 알리는 이벤트.
    public event Action OnPaused;
    public event Action OnResumed;

    // 현재 상태를 보관하는 필드.
    [SerializeField] private GameState currentState = GameState.Playing;

    // 슬로모션 중인지 판단하는 내부 플래그.
    private bool inSlowMotion = false;

    private void Awake()
    {
        // 단일 인스턴스 보장. 중복 생성 시 기존 인스턴스를 유지하고 자신을 제거.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        // 시작 시에는 반드시 정상 속도로 초기화.
        Time.timeScale = 1.0f;
        currentState = GameState.Playing;
        inSlowMotion = false;
    }

    // 외부에서 현재 상태를 읽을 때 사용할 수 있는 Getter.
    public GameState GetState()
    {
        return currentState;
    }

    // 외부에서 Pause 토글을 요청할 때 호출.
    public void TogglePauseRequested()
    {
        if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // 실제로 일시정지를 수행.
    public void PauseGame()
    {
        // 이미 일시정지라면 중복 수행 방지.
        if (currentState == GameState.Paused)
        {
            return;
        }

        // 슬로모션 중이라면 먼저 정상 속도로 되돌린 뒤 일시정지.
        if (inSlowMotion == true)
        {
            EndSlowMotion();
        }

        Time.timeScale = 0.0f;
        currentState = GameState.Paused;

        // UI 등에 알림.
        Action handler = OnPaused;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    // 일시정지 해제.
    public void ResumeGame()
    {
        // 일시정지가 아니라면 무시.
        if (currentState != GameState.Paused)
        {
            return;
        }

        Time.timeScale = 1.0f;
        currentState = GameState.Playing;

        // UI 등에 알림.
        Action handler = OnResumed;
        if (handler != null)
        {
            handler.Invoke();
        }
    }

    // 슬로모션 시작. 외부에서 intensity(0~1), duration(초)을 지정.
    // paused 상태에서는 실행하지 않음.
    public void StartSlowMotion(float timeScaleValue, float durationSeconds)
    {
        // 일시정지 상태에서는 슬로모션을 실행하지 않는다.
        if (currentState == GameState.Paused)
        {
            return;
        }

        // timeScaleValue 가 유효한 범위인지 점검.
        if (timeScaleValue <= 0.0f)
        {
            timeScaleValue = 0.2f;
        }

        if (timeScaleValue > 1.0f)
        {
            timeScaleValue = 1.0f;
        }

        if (durationSeconds <= 0.0f)
        {
            durationSeconds = 0.15f;
        }

        // 슬로모션 적용.
        Time.timeScale = timeScaleValue;
        currentState = GameState.SlowMo;
        inSlowMotion = true;

        // 코루틴으로 unscaled 시간 기준 타이머를 돌려 원복.
        StartCoroutine(SlowMoRoutine(durationSeconds));
    }

    private System.Collections.IEnumerator SlowMoRoutine(float durationSeconds)
    {
        float elapsed = 0.0f;

        // Time.unscaledDeltaTime 을 사용하여 타임스케일과 무관하게 경과 시간을 측정.
        while (elapsed < durationSeconds)
        {
            elapsed = elapsed + Time.unscaledDeltaTime;
            yield return null;
        }

        EndSlowMotion();
    }

    private void EndSlowMotion()
    {
        // 슬로모션을 끝내고 정상 속도로 복귀.
        Time.timeScale = 1.0f;
        currentState = GameState.Playing;
        inSlowMotion = false;
    }
}
