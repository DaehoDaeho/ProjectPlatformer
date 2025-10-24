using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// 파일: UISfxVolumeSettings.cs
// 설치: 설정 메뉴의 루트(슬라이더가 있는 오브젝트)에 부착.
// 역할: 슬라이더 0~1 값을 AudioMixer dB로 변환하여 SFX 볼륨을 제어하고 저장.
public class UISfxVolumeSettings : MonoBehaviour
{
    [Header("UI/오디오")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string exposedParameter = "SFX_Volume"; // AudioMixer에서 노출한 파라미터 이름.

    [Header("저장 키")]
    [SerializeField] private string playerPrefsKey = "sfx_volume_linear";

    [Header("dB 범위")]
    [SerializeField] private float minDb = -80.0f;
    [SerializeField] private float maxDb = 0.0f;

    private void Start()
    {
        ApplySavedVolume();

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    private void OnDestroy()
    {
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }

    public void OnSliderChanged(float value)
    {
        float v = Mathf.Clamp01(value);
        float db = LinearToDb(v);
        ApplyDb(db);
        PlayerPrefs.SetFloat(playerPrefsKey, v);
        PlayerPrefs.Save();
    }

    private void ApplySavedVolume()
    {
        float v = PlayerPrefs.GetFloat(playerPrefsKey, 0.8f);
        if (sfxSlider != null)
        {
            sfxSlider.value = v;
        }
        float db = LinearToDb(v);
        ApplyDb(db);
    }

    private float LinearToDb(float linear)
    {
        // 아주 작은 값으로 치환.
        if (linear <= 0.0001f)
        {
            return minDb;
        }

        float db = 20.0f * Mathf.Log10(linear);

        if (db < minDb)
        {
            db = minDb;
        }

        if (db > maxDb)
        {
            db = maxDb;
        }

        return db;
    }

    private void ApplyDb(float db)
    {
        if (audioMixer != null)
        {
            bool ok = audioMixer.SetFloat(exposedParameter, db);
            if (ok == false)
            {
                Debug.LogWarning("UISfxVolumeSettings: AudioMixer 파라미터 설정 실패. 이름 확인 필요.");
            }
        }
    }
}
