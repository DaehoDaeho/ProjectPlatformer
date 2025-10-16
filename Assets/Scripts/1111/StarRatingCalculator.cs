using UnityEngine;

// 파일: StarRatingCalculator.cs
// 역할: 남은 시간과 데스로 1~3개의 별을 계산.
public class StarRatingCalculator : MonoBehaviour
{
    [Header("Time Threshold(Sec)")]
    [SerializeField] private float twoStarTime = 20.0f;
    [SerializeField] private float threeStarTime = 40.0f;

    [Header("Death Threshold(count)")]
    [SerializeField] private int twoStarDeaths = 3;
    [SerializeField] private int threeStarDeaths = 1;

    // 반환: 1, 2, 3
    // 실무 팁: 임계값은 기획 변경이 잦으므로 인스펙터로 관리하고, 비교는 가독성이 높은 순서로 적는다.
    public int CalculateStars(float remainingTime, int deaths)
    {
        bool enoughForThreeByTime = (remainingTime >= threeStarTime);
        bool enoughForThreeByDeaths = (deaths <= threeStarDeaths);

        if ((enoughForThreeByTime == true) && (enoughForThreeByDeaths == true))
        {
            return 3;
        }

        bool enoughForTwoByTime = (remainingTime >= twoStarTime);
        bool enoughForTwoByDeaths = (deaths <= twoStarDeaths);

        if ((enoughForTwoByTime == true) && (enoughForTwoByDeaths == true))
        {
            return 2;
        }

        return 1;
    }

    // 임계값을 런타임에 바꾸고 싶다면 공개 Setter 메서드를 추가할 수 있다.
    public void SetThresholds(float twoTime, float threeTime, int twoDeaths, int threeDeaths)
    {
        twoStarTime = twoTime;
        threeStarTime = threeTime;
        twoStarDeaths = twoDeaths;
        threeStarDeaths = threeDeaths;
    }
}
