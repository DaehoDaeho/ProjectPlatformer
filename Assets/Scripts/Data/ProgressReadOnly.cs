using UnityEngine;

// 파일: ProgressReadOnly.cs
// 역할: PlayerPrefs 기반의 저장을 읽어 누적 별 합계 등을 돌려주는 읽기 전용 유틸.
// LevelSaveSystemPlus를 그대로 활용.
public static class ProgressReadOnly
{
    // 모든 레벨들의 최고 별 합계를 구한다.
    public static int GetTotalBestStars(string[] allLevelIds)
    {
        if (allLevelIds == null)
        {
            return 0;
        }

        int total = 0;

        for (int i = 0; i < allLevelIds.Length; i = i + 1)
        {
            string id = allLevelIds[i];

            if (string.IsNullOrEmpty(id) == true)
            {
                continue;
            }

            int stars = LevelSaveSystemPlus.GetBestStars(id);

            if (stars < 0)
            {
                stars = 0;
            }

            total = total + stars;
        }

        return total;
    }

    // 단일 레벨의 최고 별 수를 가져온다.
    public static int GetBestStars(string levelId)
    {
        int s = LevelSaveSystemPlus.GetBestStars(levelId);

        if (s < 0)
        {
            s = 0;
        }

        return s;
    }
}
