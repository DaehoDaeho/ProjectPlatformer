using UnityEngine;
using UnityEngine.UI;

// 파일: CurveColorFader.cs
// 설치: 배경 Image가 있는 오브젝트에 부착.
// 역할: 지정한 목표 색상으로 곡선 기반 페이드를 수행.
public class CurveColorFader : MonoBehaviour
{
    [Header("대상")]
    [SerializeField] private Graphic targetGraphic;

    [Header("시간/커브")]
    [SerializeField] private float fadeSeconds = 0.35f;
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Color startColor;
    private Color goalColor;
    private float elapsed = 0.0f;
    private bool running = false;

    private void Awake()
    {
        if (targetGraphic == null)
        {
            targetGraphic = GetComponent<Graphic>();
        }

        if (targetGraphic != null)
        {
            startColor = targetGraphic.color;
            goalColor = startColor;
        }
    }

    public void PlayTo(Color target)
    {
        if (targetGraphic == null)
        {
            return;
        }

        startColor = targetGraphic.color;
        goalColor = target;
        elapsed = 0.0f;
        running = true;
    }

    private void Update()
    {
        if (running == false)
        {
            return;
        }

        elapsed = elapsed + Time.unscaledDeltaTime;

        float t = 0.0f;

        if (fadeSeconds > 0.0f)
        {
            t = elapsed / fadeSeconds;
        }

        if (t > 1.0f)
        {
            t = 1.0f;
        }

        float k = curve.Evaluate(t);

        Color c = Color.Lerp(startColor, goalColor, k);
        targetGraphic.color = c;

        if (t >= 1.0f)
        {
            running = false;
        }
    }
}
