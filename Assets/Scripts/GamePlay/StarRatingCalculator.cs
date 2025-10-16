using UnityEngine;

public class StarRatingCalculator : MonoBehaviour
{
    [Header("Time Threshold(Sec)")]
    [SerializeField] private float twoStarTime = 20.0f;
    [SerializeField] private float threeStarTime = 40.0f;

    [Header("Death Threshold(count)")]
    [SerializeField] private int twoStarDeaths = 3;
    [SerializeField] private int threeStarDeaths = 1;

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

    public void SetThresholds(float twoTime, float threeTime, int twoDeaths, int threeDeaths)
    {
        twoStarTime = twoTime;
        threeStarTime = threeTime;
        twoStarDeaths = twoDeaths;
        threeStarDeaths = threeDeaths;
    }
}
