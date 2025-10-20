using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private string levelId = "Level1";
    [SerializeField] private bool autoShowBestOnStart = true;

    private float elapsed = 0.0f;
    private bool running = true;

    private void Start()
    {
        elapsed = 0.0f;
        running = true;

        if ((timeText != null) && (autoShowBestOnStart == true))
        {
            float best = LevelSaveSystemPlus.GetBestTime(levelId);
            if (best >= 0.0f)
            {
                timeText.text = "Time: 00:00.00  |  Best: " + LevelSaveSystemPlus.FormatTime(best);
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
            elapsed = elapsed + Time.deltaTime;

            if (timeText != null)
            {
                timeText.text = "Time: " + LevelSaveSystemPlus.FormatTime(elapsed);
            }
        }
    }

    public void StopTimer()
    {
        running = false;
    }

    public float GetElapsedSeconds()
    {
        return elapsed;
    }

    public string GetLevelIdForSave()
    {
        return levelId;
    }
}
