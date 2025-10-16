using UnityEngine;
using TMPro;

// 파일: LevelConstraints.cs
// 설치: GameManager.
// 역할: 제한 시간과 데스 카운트 관리. 시간 초과 시 Fail 처리.
public class LevelConstraints : MonoBehaviour
{
    [Header("시간 제한")]
    [SerializeField] private float startTimeSeconds = 90.0f;

    [Header("UI 참조")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text deathsText;

    [Header("Fail 패널")]
    [SerializeField] private GameObject failPanel;

    // 내부 상태
    private float remainingTime = 0.0f;
    private int deaths = 0;
    private bool failed = false;
    private bool cleared = false;

    private void Awake()
    {
        remainingTime = startTimeSeconds;

        if (failPanel != null)
        {
            failPanel.SetActive(false);
        }

        UpdateTimeUI();
        UpdateDeathsUI();
    }

    private void OnEnable()
    {
        // 피격 이벤트 구독. 실무에서는 Enable/Disable에 맞춰 구독/해제를 관리한다.
        GameplayEvents.OnPlayerHit += HandlePlayerHit;
    }

    private void OnDisable()
    {
        GameplayEvents.OnPlayerHit -= HandlePlayerHit;
    }

    private void Update()
    {
        if (failed == true)
        {
            return;
        }

        if (cleared == true)
        {
            return;
        }

        // 카운트다운
        remainingTime = remainingTime - Time.deltaTime;

        if (remainingTime < 0.0f)
        {
            remainingTime = 0.0f;
        }

        UpdateTimeUI();

        if (remainingTime <= 0.0f)
        {
            DoFail();
        }
    }

    private void HandlePlayerHit(Vector3 hitWorldPosition)
    {
        if (failed == true)
        {
            return;
        }

        if (cleared == true)
        {
            return;
        }

        deaths = deaths + 1;
        UpdateDeathsUI();
    }

    private void DoFail()
    {
        if (failed == true)
        {
            return;
        }

        failed = true;

        if (failPanel != null)
        {
            failPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("LevelConstraints: failPanel이 연결되지 않았습니다.");
        }

        // 게임 정지
        Time.timeScale = 0.0f;
        Debug.Log("LevelConstraints: 시간 초과로 실패 처리되었습니다.");
    }

    private void UpdateTimeUI()
    {
        if (timeText == null)
        {
            return;
        }

        timeText.text = "Time Left: " + FormatTime(remainingTime);
    }

    private void UpdateDeathsUI()
    {
        if (deathsText == null)
        {
            return;
        }

        deathsText.text = "Deaths: " + deaths.ToString();
    }

    private string FormatTime(float seconds)
    {
        if (seconds < 0.0f)
        {
            seconds = 0.0f;
        }

        int totalMs = Mathf.RoundToInt(seconds * 1000.0f);
        int minutes = totalMs / 60000;
        int msLeft = totalMs % 60000;
        int secs = msLeft / 1000;
        int ms = msLeft % 1000;

        string twoDigitMs = (ms / 10).ToString("00");
        string result = minutes.ToString("00") + ":" + secs.ToString("00") + "." + twoDigitMs;
        return result;
    }

    // 클리어 직전에 외부에서 호출하면 시간 0 엣지 케이스를 예방할 수 있다.
    public void MarkCleared()
    {
        cleared = true;
    }

    // 별 계산에서 읽을 값들
    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public int GetDeaths()
    {
        return deaths;
    }
}
