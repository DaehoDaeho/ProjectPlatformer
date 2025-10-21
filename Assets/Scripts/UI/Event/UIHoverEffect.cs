using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float normalScale = 1.0f;

    [SerializeField]
    private float hoverScale = 1.3f;

    [SerializeField]
    private float scaleLerpPerSecond = 12.0f;   // 전환 속도.

    [SerializeField]
    private bool useSmoothStep = true;

    private bool hover = false;
    private Vector3 targetScale;

    void Awake()
    {
        targetScale = new Vector3(normalScale, hoverScale, 1.0f);
        transform.localScale = targetScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    void Update()
    {
        float desired = normalScale;

        if(hover == true)
        {
            desired = hoverScale;
        }

        Vector3 current = transform.localScale;
        Vector3 desiredV = new Vector3(desired, desired, 1.0f);

        float t = scaleLerpPerSecond * Time.unscaledDeltaTime;

        if(t > 1.0f)
        {
            t = 1.0f;
        }

        if(useSmoothStep == true)
        {
            float s = Mathf.SmoothStep(0.0f, 1.0f, t);
            Vector3 next = new Vector3(Mathf.Lerp(current.x, desiredV.x, s), Mathf.Lerp(current.y, desiredV.y, s), 1.0f);

            transform.localScale = next;
        }
        else
        {
            Vector3 next = new Vector3(Mathf.Lerp(current.x, desiredV.x, t), Mathf.Lerp(current.y, desiredV.y, t), 1.0f);
            transform.localScale = next;
        }
    }
}
