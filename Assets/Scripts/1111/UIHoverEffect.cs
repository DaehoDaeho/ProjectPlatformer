using UnityEngine;
using UnityEngine.EventSystems;

// 파일: UIHoverEffect.cs
// 설치: 버튼, 카드 등 UI 루트에 부착.
// 역할: 마우스가 위에 있을 때 부드럽게 확대하고, 나가면 원래 크기로 복귀합니다.
public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("스케일 설정")]
    [SerializeField] private float normalScale = 1.0f;
    [SerializeField] private float hoverScale = 1.06f;

    [Header("전환 속도")]
    [SerializeField] private float scaleLerpPerSecond = 12.0f;

    [Header("이징 곡선 선택")]
    [SerializeField] private bool useSmoothStep = true;

    private bool hover = false;
    private Vector3 targetScale;

    private void Awake()
    {
        targetScale = new Vector3(normalScale, normalScale, 1.0f);
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

    private void Update()
    {
        float desired = normalScale;

        if (hover == true)
        {
            desired = hoverScale;
        }

        Vector3 current = transform.localScale;
        Vector3 desiredV = new Vector3(desired, desired, 1.0f);

        // 프레임 독립 보간 비율 계산.
        float t = scaleLerpPerSecond * Time.unscaledDeltaTime;

        if (t > 1.0f)
        {
            t = 1.0f;
        }

        if (useSmoothStep == true)
        {
            // t를 0~1로 스무스하게 다듬기.
            float s = Mathf.SmoothStep(0.0f, 1.0f, t);
            Vector3 next = new Vector3(
                Mathf.Lerp(current.x, desiredV.x, s),
                Mathf.Lerp(current.y, desiredV.y, s),
                1.0f
            );
            transform.localScale = next;
        }
        else
        {
            Vector3 next = new Vector3(
                Mathf.Lerp(current.x, desiredV.x, t),
                Mathf.Lerp(current.y, desiredV.y, t),
                1.0f
            );
            transform.localScale = next;
        }
    }
}
