using UnityEngine;
using TMPro;

// 파일: LevelTimer.cs
// 설치: GameManager 또는 빈 오브젝트에 부착.
// 역할: 레벨 경과 시간을 측정하여 UI에 표시하고, 클리어 순간에 최단 기록을 저장합니다.
public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;   // 시간 표시 UI(TextMeshPro)
    [SerializeField] private string levelId = "Level1"; // 레벨 식별자(씬 이름 또는 직접 지정)
    [SerializeField] private bool autoShowBestOnStart = true;

    private float elapsed = 0.0f;
    private bool running = true;

    private void Start()
    {
        elapsed = 0.0f;
        running = true;

        if ((timeText != null) && (autoShowBestOnStart == true))
        {
            float best = LevelSaveSystem.GetBestTime(levelId);
            if (best >= 0.0f)
            {
                timeText.text = "Time: 00:00.00  |  Best: " + FormatTime(best);
            }
            else
            {
                timeText.text = "Time: 00:00.00  |  Best: --:--.--";
            }
        }
    }

    private void Update()
    {
        if (running == true)
        {
            // 일반적으로 Time.deltaTime 사용. 클리어 시 Time.timeScale이 0이 되면 Update가 멈춥니다.
            // 저장은 별도의 메서드 FinishAndSave에서 수행합니다.
            elapsed = elapsed + Time.deltaTime;

            if (timeText != null)
            {
                timeText.text = "Time: " + FormatTime(elapsed);
            }
        }
    }

    // LevelFlowController의 DoClear() 전에 이 메서드를 호출하면 좋습니다.
    // (버튼 OnClick에서 호출하거나, GoalTrigger에서 클리어 판단 직후 호출하는 방식으로 연결)
    public void FinishAndSave()
    {
        if (running == false)
        {
            return;
        }

        running = false;

        // 최단 기록 저장
        LevelSaveSystem.SaveBestTime(levelId, elapsed);

        // UI에 최종/최단 기록 표시
        if (timeText != null)
        {
            float best = LevelSaveSystem.GetBestTime(levelId);
            string bestText = (best >= 0.0f) ? FormatTime(best) : "--:--.--";
            timeText.text = "Time: " + FormatTime(elapsed) + "  |  Best: " + bestText;
        }
    }

    private string FormatTime(float seconds)
    {
        int totalMs = Mathf.RoundToInt(seconds * 1000.0f);
        int minutes = totalMs / 60000;
        int msLeft = totalMs % 60000;
        int secs = msLeft / 1000;
        int ms = msLeft % 1000;

        // 예: 01:23.45  (소수점 두 자리 표시)
        string ms2 = (ms / 10).ToString("00");
        return minutes.ToString("00") + ":" + secs.ToString("00") + "." + ms2;
    }
}
