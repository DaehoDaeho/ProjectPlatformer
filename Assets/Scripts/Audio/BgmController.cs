using UnityEngine;
using System.Collections;

public class BgmController : MonoBehaviour
{
    [Header("Basic Volume")]
    [SerializeField] private float defaultVolume = 0.8f;

    [Header("Crossfade Basic Time(Sec)")]
    [SerializeField] private float defaultFadeSeconds = 1.2f;

    private AudioSource activeSource;
    private AudioSource idleSource;

    private Coroutine crossfadeRoutine;

    private void Awake()
    {
        activeSource = gameObject.AddComponent<AudioSource>();
        idleSource = gameObject.AddComponent<AudioSource>();

        ConfigureSource(activeSource);
        ConfigureSource(idleSource);
    }

    private void ConfigureSource(AudioSource src)
    {
        src.playOnAwake = false;    // 처음부터 재생할지 여부.
        src.loop = true;    // 반복 재생 여부.
        src.volume = 0.0f;  // 볼륨.
    }

    public void PlayImmediate(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogWarning("BgmController.PlayImmediate: clip이 없습니다.");
            return;
        }

        StopCrossfadeIfRunning();

        activeSource.clip = clip;
        activeSource.volume = Mathf.Clamp01(volume);
        activeSource.Play();

        idleSource.Stop();
        idleSource.clip = null;
        idleSource.volume = 0.0f;
    }

    public void CrossfadeTo(AudioClip clip, float targetVolume, float duration)
    {
        //if (clip == null)
        //{
        //    Debug.LogWarning("BgmController.CrossfadeTo: clip이 없습니다.");
        //    return;
        //}

        StopCrossfadeIfRunning();

        idleSource.clip = clip;
        idleSource.volume = 0.0f;
        idleSource.Play();

        float startVolumeActive = activeSource.volume;
        float endVolumeActive = 0.0f;

        float endVolumeIdle = Mathf.Clamp01(targetVolume);

        crossfadeRoutine = StartCoroutine(CrossfadeRoutine(startVolumeActive, endVolumeActive, endVolumeIdle, duration));
    }

    private IEnumerator CrossfadeRoutine(float startActive, float endActive, float endIdle, float duration)
    {
        if (duration <= 0.0f)
        {
            duration = 0.01f;
        }

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed = elapsed + Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // 사운드의 볼륨을 보간 함수로 서서히 키우거나 줄여줌.
            float vActive = Mathf.Lerp(startActive, endActive, t);
            float vIdle = Mathf.Lerp(0.0f, endIdle, t);

            activeSource.volume = vActive;
            idleSource.volume = vIdle;

            yield return null;
        }

        activeSource.Stop();

        AudioSource temp = activeSource;
        activeSource = idleSource;
        idleSource = temp;

        idleSource.clip = null;
        idleSource.volume = 0.0f;

        crossfadeRoutine = null;
    }

    private void StopCrossfadeIfRunning()
    {
        if (crossfadeRoutine != null)
        {
            StopCoroutine(crossfadeRoutine);
            crossfadeRoutine = null;
        }
    }

    // 편의 메서드: 기본 파라미터 사용
    public void PlayImmediateDefault(AudioClip clip)
    {
        PlayImmediate(clip, defaultVolume);
    }

    public void CrossfadeToDefault(AudioClip clip)
    {
        CrossfadeTo(clip, defaultVolume, defaultFadeSeconds);
    }
}
