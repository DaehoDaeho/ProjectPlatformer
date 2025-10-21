using UnityEngine;
using UnityEngine.EventSystems;

// 파일: UIButtonSound.cs
// 설치: 버튼, 카드 등 UI 루트에 부착(오디오 소스는 없어도 동작).
// 역할: 호버/클릭 시 효과음을 간단히 재생합니다.
public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("효과음 클립")]
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;

    [Header("볼륨(0~1)")]
    [SerializeField] private float volume = 0.8f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverClip != null)
        {
            AudioSource.PlayClipAtPoint(hoverClip, Vector3.zero, Mathf.Clamp01(volume));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickClip != null)
        {
            AudioSource.PlayClipAtPoint(clickClip, Vector3.zero, Mathf.Clamp01(volume));
        }
    }
}
