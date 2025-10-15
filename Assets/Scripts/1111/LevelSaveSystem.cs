using UnityEngine;

// 파일: LevelSaveSystem.cs
// 설치: 아무 오브젝트에도 부착하지 않는 정적 클래스.
// 역할: PlayerPrefs를 사용하여 레벨별 최단 시간을 저장/불러오기합니다.
public static class LevelSaveSystem
{
    // 저장 키를 만들 때 접두사를 붙여 충돌을 방지합니다.
    private static string MakeBestTimeKey(string levelId)
    {
        return "best_time_" + levelId;
    }

    // 최단 기록을 초 단위로 저장합니다. 더 작은 값이면 갱신합니다.
    public static void SaveBestTime(string levelId, float seconds)
    {
        if (string.IsNullOrEmpty(levelId) == true)
        {
            return;
        }

        string key = MakeBestTimeKey(levelId);
        float old = PlayerPrefs.GetFloat(key, -1.0f); // -1: 아직 기록 없음

        if ((old < 0.0f) || (seconds < old))
        {
            PlayerPrefs.SetFloat(key, seconds);
            PlayerPrefs.Save();
        }
    }

    // 최단 기록 불러오기. 기록이 없으면 음수 반환.
    public static float GetBestTime(string levelId)
    {
        if (string.IsNullOrEmpty(levelId) == true)
        {
            return -1.0f;
        }

        string key = MakeBestTimeKey(levelId);
        return PlayerPrefs.GetFloat(key, -1.0f);
    }
}
