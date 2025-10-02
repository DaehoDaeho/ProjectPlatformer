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

        if(currentScore >= maxScore)
        {
            if(goalPortal != null)
            {
                goalPortal.SetActive(true);
            }
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
