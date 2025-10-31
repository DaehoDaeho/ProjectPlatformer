using System;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int startScore = 0;

    private int currentScore = 0;
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
    }

    public int maxScore = 10;
    public GameObject goalPortal;

    public AudioClip changeAudio;
    public BgmController bgmController;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        ResetScore();

        // 유효성 검사. null 체크.
        if(goalPortal != null)
        {
            goalPortal.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount; // currentScore = currentScore + amount;

        if(OnScoreChanged != null)
        {
            OnScoreChanged.Invoke(currentScore);
        }

        if(currentScore >= maxScore)
        {
            if(goalPortal != null)
            {
                goalPortal.SetActive(true);
            }

            //if (bgmController != null)
            //{
            //    bgmController.CrossfadeTo(changeAudio, 0.8f, 1.2f);
            //}
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = startScore;
    }
}
