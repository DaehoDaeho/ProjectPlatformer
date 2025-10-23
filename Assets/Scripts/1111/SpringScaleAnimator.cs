using UnityEngine;

// 파일: SpringScaleAnimator.cs
// 설치: 카드 프리팹 루트(Transform)에 부착.
// 역할: 선택 트리거 시 스프링 느낌의 스케일 애니메이션을 재생.
public class SpringScaleAnimator : MonoBehaviour
{
    [Header("기본/목표 스케일")]
    [SerializeField] private float baseScale = 1.0f;
    [SerializeField] private float peakScale = 1.12f;

    [Header("스프링 파라미터")]
    [SerializeField] private float frequency = 3.0f;        // 초당 진동수.
    [SerializeField] private float damping = 3.5f;          // 감쇠 계수(클수록 빨리 가라앉음)
    [SerializeField] private float duration = 0.6f;

    [Header("강도 커브(0~1 t)")]
    [SerializeField] private AnimationCurve intensityCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float elapsed = 0.0f;
    private bool playing = false;

    private void Awake()
    {
        Vector3 s = new Vector3(baseScale, baseScale, 1.0f);
        transform.localScale = s;
    }

    public void Play()
    {
        playing = true;
        elapsed = 0.0f;
    }

    private void Update()
    {
        if (playing == false)
        {
            return;
        }

        elapsed = elapsed + Time.unscaledDeltaTime;

        float t = elapsed / duration;

        if (t > 1.0f)
        {
            t = 1.0f;
        }

        // 진동(사인) + 감쇠(지수) + 커브 강도.
        float omega = 2.0f * Mathf.PI * frequency;
        float sinTerm = Mathf.Sin(omega * elapsed);
        float decay = Mathf.Exp(-damping * elapsed);
        float intensity = intensityCurve.Evaluate(t);

        // base -> peak 방향으로 출발했다가 점차 base로 감쇠.
        float overshoot = (peakScale - baseScale) * sinTerm * decay * intensity;

        float scaleNow = baseScale + overshoot;

        if (scaleNow < 0.1f)
        {
            scaleNow = 0.1f;
        }

        Vector3 s = new Vector3(scaleNow, scaleNow, 1.0f);
        transform.localScale = s;

        if (t >= 1.0f && Mathf.Abs(overshoot) < 0.001f)
        {
            playing = false;
            Vector3 reset = new Vector3(baseScale, baseScale, 1.0f);
            transform.localScale = reset;
        }
    }
}
