using UnityEngine;
using TMPro;

// 파일: ClearStarPresenter.cs
// 설치: 클리어 패널.
// 역할: 패널이 켜질 때 별 계산 → UI 갱신.
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
        // 의존성 자동 탐색(실무에서는 명시 주입을 선호하지만, 교육에서는 안전한 자동 탐색도 활용한다.)
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
            starText.text = string.Format(format, stars);
        }
        else
        {
            Debug.Log("ClearStarPresenter: 별 점수 = " + stars.ToString());
        }
    }
}
