using UnityEngine;

// 파일: UnlockDatabase.cs
// 역할: 여러 LevelUnlockRule을 묶어 관리하고, levelId로 규칙을 찾아준다.
[CreateAssetMenu(fileName = "UnlockDatabase", menuName = "Game/Unlock Database", order = 1)]
public class UnlockDatabase : ScriptableObject
{
    [Header("레벨 해금 규칙 목록")]
    public LevelUnlockRule[] rules;

    public LevelUnlockRule FindRule(string levelId)
    {
        if (rules == null)
        {
            return null;
        }

        for (int i = 0; i < rules.Length; i = i + 1)
        {
            LevelUnlockRule r = rules[i];

            if (r == null)
            {
                continue;
            }

            if (string.Equals(r.levelId, levelId) == true)
            {
                return r;
            }
        }

        return null;
    }
}
