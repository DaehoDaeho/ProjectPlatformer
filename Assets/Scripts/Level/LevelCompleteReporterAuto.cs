using UnityEngine;

// 파일: LevelCompleteReporterAuto.cs
// 설치: 플레이 씬의 GameManager에 부착.
// 역할: LevelFlowController.OnStageCleared를 구독해 자동으로 시간·별을 계산하여 저장.
[RequireComponent(typeof(LevelFlowController))]
public class LevelCompleteReporterAuto : MonoBehaviour
{
    [Header("선택 참조(자동 탐색 지원)")]
    [SerializeField] private LevelTimer levelTimer;
    [SerializeField] private LevelConstraints constraints;
    [SerializeField] private StarRatingCalculator calculator;

    private LevelFlowController flow;
    private bool savedOnce = false;

    private void Awake()
    {
        flow = GetComponent<LevelFlowController>();
        if (levelTimer == null)
        {
            levelTimer = FindAnyObjectByType<LevelTimer>();
        }
        if (constraints == null)
        {
            constraints = FindAnyObjectByType<LevelConstraints>();
        }
        if (calculator == null)
        {
            calculator = FindAnyObjectByType<StarRatingCalculator>();
            if (calculator == null)
            {
                calculator = gameObject.AddComponent<StarRatingCalculator>();
            }
        }
    }

    private void OnEnable()
    {
        if (flow != null)
        {
            flow.OnStageCleared += HandleStageCleared;
        }
    }

    private void OnDisable()
    {
        if (flow != null)
        {
            flow.OnStageCleared -= HandleStageCleared;
        }
    }

    private void HandleStageCleared()
    {
        if (savedOnce == true)
        {
            return;
        }

        savedOnce = true;

        float elapsed = 0.0f;
        string levelId = "Level1";
        if (levelTimer != null)
        {
            elapsed = levelTimer.GetElapsedSeconds();
            levelId = levelTimer.GetLevelIdForSave();
            levelTimer.StopTimer();
        }

        float remaining = 0.0f;
        int deaths = 0;
        if (constraints != null)
        {
            remaining = constraints.GetRemainingTime();
            deaths = constraints.GetDeaths();
        }

        int stars = 1;
        if (calculator != null)
        {
            stars = calculator.CalculateStars(remaining, deaths);
        }

        LevelSaveSystemPlus.SaveBestTime(levelId, elapsed);
        LevelSaveSystemPlus.SaveBestStars(levelId, stars);

        Debug.Log("ReporterAuto: 저장 완료. level=" + levelId + ", time=" + elapsed.ToString("0.00") + ", stars=" + stars.ToString());
    }
}
