using UnityEngine;
using TMPro;

public class ClearStarPresenter : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private LevelConstraints constraints;
    [SerializeField] private StarRatingCalculator calculator;
    [SerializeField] private TMP_Text starText;

    [Header("표시 포맷")]
    [SerializeField] private string format = "Stars: {0} / 3";

    private void OnEnable()
    {
        if (constraints == null)
        {
            constraints = FindAnyObjectByType<LevelConstraints>();
        }

        if (calculator == null)
        {
            calculator = GetComponent<StarRatingCalculator>();
            if (calculator == null)
            {
                calculator = gameObject.AddComponent<StarRatingCalculator>();
            }
        }

        if (constraints == null)
        {
            Debug.LogWarning("ClearStarPresenter: LevelConstraints를 찾지 못했습니다.");
            return;
        }

        float remaining = constraints.GetRemainingTime();
        int deaths = constraints.GetDeaths();

        int stars = 1;

        if (calculator != null)
        {
            stars = calculator.CalculateStars(remaining, deaths);
        }

        if (starText != null)
        {
            // "Stars: {0} / 3", stars -> "Stars: 3 / 3"
            starText.text = string.Format(format, stars);
        }
        else
        {
            Debug.Log("ClearStarPresenter: 별 점수 = " + stars.ToString());
        }
    }
}
