using UnityEngine;
using TMPro;
using System;

public class LevelFlowController : MonoBehaviour
{
    [Header("목표 설정")]
    [SerializeField] private int requiredScore = 10;

    [Header("참조")]
    [SerializeField] private GameScore gameScore;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private GameObject clearPanel;

    public event Action OnStageCleared;

    private bool readyToClear = false;
    private int currentScore = 0;
    private bool cleared = false;

    private void Awake()
    {
        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }

        if (gameScore != null)
        {
            currentScore = gameScore.GetScore();
        }
        else
        {
            Debug.LogWarning("LevelFlowController: gameScore가 설정되지 않았습니다.");
        }

        UpdateReadyState();
        UpdateObjectiveUI();
    }

    private void OnEnable()
    {
        if (gameScore != null)
        {
            gameScore.OnScoreChanged += HandleScoreChanged;
        }
    }

    private void OnDisable()
    {
        if (gameScore != null)
        {
            gameScore.OnScoreChanged -= HandleScoreChanged;
        }
    }

    private void HandleScoreChanged(int newScore)
    {
        currentScore = newScore;
        UpdateReadyState();
        UpdateObjectiveUI();
    }

    private void UpdateReadyState()
    {
        if (currentScore >= requiredScore)
        {
            readyToClear = true;
        }
        else
        {
            readyToClear = false;
        }
    }

    private void UpdateObjectiveUI()
    {
        if (objectiveText != null)
        {
            objectiveText.text = "Goal: " + requiredScore.ToString() + "  |  Now: " + currentScore.ToString();
        }
    }

    public void NotifyGoalTouched()
    {
        if (readyToClear == true)
        {
            DoClear();
        }
        else
        {
            Debug.Log("Goal 도달. 아직 목표 점수가 부족합니다. (" + currentScore.ToString() + " / " + requiredScore.ToString() + ")");
        }
    }

    private void DoClear()
    {
        if (cleared == true)
        {
            return;
        }

        cleared = true;

        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }

        // 1) 저장·보고가 먼저 수행될 수 있도록 이벤트를 먼저 발행.
        Action handler = OnStageCleared;
        if (handler != null)
        {
            handler.Invoke();
        }

        // 2) 게임 정지.
        Time.timeScale = 0.0f;
        Debug.Log("Stage Clear!");
    }

    // 공개 Getter (연동용)
    public bool IsReadyToClear()
    {
        return readyToClear;
    }

    public int GetRequiredScore()
    {
        return requiredScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
