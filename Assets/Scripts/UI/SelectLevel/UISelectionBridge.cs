using UnityEngine;

// 파일: UISelectionBridge.cs
// 설치: 카드 프리팹 루트에 부착.
// 역할: 카드가 선택될 때 스프링/컬러 애니메이션을 트리거.
public class UISelectionBridge : MonoBehaviour
{
    [Header("연동 대상")]
    [SerializeField] private SpringScaleAnimator spring;
    [SerializeField] private CurveColorFader colorFader;

    [Header("선택/해제 색상")]
    [SerializeField] private Color selectedColor = new Color(1.0f, 0.98f, 0.7f, 0.35f);
    [SerializeField] private Color deselectedColor = new Color(1.0f, 1.0f, 1.0f, 0.05f);

    // UISelectableCard의 Select/Deselect에서 이 메서드를 호출.
    public void OnSelected()
    {
        if (spring != null)
        {
            spring.Play();
        }

        if (colorFader != null)
        {
            colorFader.PlayTo(selectedColor);
        }
    }

    public void OnDeselected()
    {
        if (colorFader != null)
        {
            colorFader.PlayTo(deselectedColor);
        }
    }
}
