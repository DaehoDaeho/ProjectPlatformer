using UnityEngine;

// 파일: CameraShake2D.cs
// 설치: Camera 오브젝트에 부착.
// 역할: 비Cinemachine 방식으로 감쇠되는 스크린 셰이크를 구현.
public class CameraShake2D : MonoBehaviour
{
    [Header("기본 값")]
    [SerializeField] private float defaultIntensity = 0.25f;    // 흔들림 강도.
    [SerializeField] private float defaultDuration = 0.2f;  // 흔들림 시간.
    [SerializeField] private float defaultDamping = 8.0f;

    private Vector3 originalLocalPos;
    private float timeLeft = 0.0f;
    private float intensity = 0.0f;
    private float damping = 8.0f;
    private float seedX = 0.0f;
    private float seedY = 0.0f;

    private void Awake()
    {
        originalLocalPos = transform.localPosition;
        seedX = Random.value * 1000.0f;
        seedY = Random.value * 2000.0f;
    }

    private void OnDisable()
    {
        transform.localPosition = originalLocalPos;
        timeLeft = 0.0f;
    }

    public void ShakeOnce(float reqIntensity, float reqDuration, float reqDamping)
    {
        originalLocalPos = transform.localPosition;

        if (reqIntensity <= 0.0f)
        {
            reqIntensity = defaultIntensity;
        }

        if (reqDuration <= 0.0f)
        {
            reqDuration = defaultDuration;
        }

        if (reqDamping <= 0.0f)
        {
            reqDamping = defaultDamping;
        }

        intensity = reqIntensity;
        timeLeft = reqDuration;
        damping = reqDamping;
    }

    private void LateUpdate()
    {
        if (timeLeft > 0.0f)
        {
            timeLeft = timeLeft - Time.unscaledDeltaTime;

            float t = timeLeft;
            float decay = Mathf.Exp(-damping * (1.0f - (t / Mathf.Max(t, 0.0001f))));

            float nx = Mathf.PerlinNoise(seedX, Time.unscaledTime * 50.0f);
            float ny = Mathf.PerlinNoise(seedY, Time.unscaledTime * 50.0f);
            float offsetX = (nx - 0.5f) * 2.0f * intensity * decay;
            float offsetY = (ny - 0.5f) * 2.0f * intensity * decay;

            transform.localPosition = originalLocalPos + new Vector3(offsetX, offsetY, 0.0f);

            if (timeLeft <= 0.0f)
            {
                transform.localPosition = originalLocalPos;
            }
        }
    }
}
