using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    private AudioClip hoverClip;

    [SerializeField]
    private AudioClip clickClip;

    [SerializeField]
    private float volume = 0.8f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(hoverClip != null)
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
