using UnityEngine;

// 파일: LevelUnlockRule.cs
// 역할: 특정 레벨을 해금하기 위한 "필요 총 별 수" 규칙을 정의하는 데이터 자산.
[CreateAssetMenu(fileName = "LevelUnlockRule", menuName = "Game/Level Unlock Rule", order = 0)]
public class LevelUnlockRule : ScriptableObject
{
    [Header("레벨 고유 ID(카드·세이브와 일치)")]
    public string levelId;

    [Header("해금에 필요한 '전체 최고 별 합계'")]
    public int requiredTotalStars = 0;
}
