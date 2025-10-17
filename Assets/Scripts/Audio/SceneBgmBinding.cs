using UnityEngine;

public class SceneBgmBinding : MonoBehaviour
{
    [Header("BGM Clip")]
    [SerializeField] private AudioClip bgmClip;

    [SerializeField] private bool playImmediateOnStart = false;

    [Header("Volume/Fade")]
    [SerializeField] private float targetVolume = 0.8f;
    [SerializeField] private float fadeSeconds = 1.0f;

    private void Start()
    {
        BgmController ctrl = FindAnyObjectByType<BgmController>();
        if (ctrl == null)
        {
            Debug.LogWarning("SceneBgmBinding: BgmController를 찾을 수 없습니다.");
            return;
        }

        if (bgmClip == null)
        {
            Debug.LogWarning("SceneBgmBinding: bgmClip이 지정되지 않았습니다.");
            return;
        }

        if (playImmediateOnStart == true)
        {
            ctrl.PlayImmediate(bgmClip, targetVolume);
        }
        else
        {
            ctrl.CrossfadeTo(bgmClip, targetVolume, fadeSeconds);
        }
    }
}
