using UnityEngine;

public static class LevelSaveSystemPlus
{
    private static string MakeBestTimeKey(string levelId)
    {
        return "best_time_" + levelId;
    }

    private static string MakeBestStarsKey(string levelId)
    {
        return "best_stars_" + levelId;
    }

    public static void SaveBestTime(string levelId, float seconds)
    {
        if (string.IsNullOrEmpty(levelId) == true)
        {
            Debug.LogWarning("SaveBestTime: levelId 비어 있음");
            return;
        }

        if (seconds < 0.0f)
        {
            return;
        }

        string key = MakeBestTimeKey(levelId);
        float old = PlayerPrefs.GetFloat(key, -1.0f);

        if ((old < 0.0f) || (seconds < old))
        {
            PlayerPrefs.SetFloat(key, seconds);
            PlayerPrefs.Save();
        }
    }

    public static float GetBestTime(string levelId)
    {
        if (string.IsNullOrEmpty(levelId) == true)
        {
            return -1.0f;
        }

        return PlayerPrefs.GetFloat(MakeBestTimeKey(levelId), -1.0f);
    }

    public static void SaveBestStars(string levelId, int stars)
    {
        if (string.IsNullOrEmpty(levelId) == true)
        {
            Debug.LogWarning("SaveBestStars: levelId 비어 있음");
            return;
        }

        if (stars < 0)
        {
            stars = 0;
        }

        if (stars > 3)
        {
            stars = 3;
        }

        string key = MakeBestStarsKey(levelId);
        int old = PlayerPrefs.GetInt(key, -1);

        if (stars > old)
        {
            PlayerPrefs.SetInt(key, stars);
            PlayerPrefs.Save();
        }
    }

    public static int GetBestStars(string levelId)
    {
        if (string.IsNullOrEmpty(levelId) == true)
        {
            return -1;
        }

        return PlayerPrefs.GetInt(MakeBestStarsKey(levelId), -1);
    }

    public static string FormatTime(float seconds)
    {
        if (seconds < 0.0f)
        {
            return "--:--.--";
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
}
