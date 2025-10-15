using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeCanvasController : MonoBehaviour
{
    [SerializeField] private float defaultDuration = 0.6f;

    private CanvasGroup group;
    private Coroutine currentRoutine;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        if (group == null)
        {
            group = gameObject.AddComponent<CanvasGroup>();  // 만약 CanvasGroup 컴포넌트가 없다면 새로 추가.
        }
    }

    public void FadeOut()
    {
        StartFade(1.0f, defaultDuration);
    }

    public void FadeIn()
    {
        StartFade(0.0f, defaultDuration);
    }

    public void StartFade(float targetAlpha, float duration)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(FadeRoutine(targetAlpha, duration));
    }

    private IEnumerator FadeRoutine(float targetAlpha, float duration)
    {
        if (duration <= 0.0f)
        {
            duration = 0.01f;
        }

        float startAlpha = group.alpha;
        float elapsed = 0.0f;

        // 페이드 중에는 입력이 통과되지 않도록 blocksRaycasts를 켭니다.
        group.blocksRaycasts = true;
        group.interactable = false;

        while (elapsed < duration)
        {
            elapsed = elapsed + Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        group.alpha = targetAlpha;

        // 완전히 투명해지면 입력 막힘 해제
        if (Mathf.Approximately(targetAlpha, 0.0f) == true)
        {
            group.blocksRaycasts = false;
        }

        currentRoutine = null;
    }

    public float GetDefaultDuration()
    {
        return defaultDuration;
    }
}
