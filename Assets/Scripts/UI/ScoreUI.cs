using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public GameScore gameScore;
    public TMP_Text textScore;

    // Update is called once per frame
    void Update()
    {
        int score = gameScore.GetScore();
        textScore.text = "Score : " + score.ToString();
    }
}
