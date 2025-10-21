using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 파일: UISelectableCard.cs
// 설치: 레벨 카드 프리팹 루트에 부착.
// 역할: 선택/비선택 시 시각 상태(테두리 색, 배경 색, 그림자 등)를 전환합니다.
public class UISelectableCard : MonoBehaviour, IPointerClickHandler
{
    [Header("시각 요소")]
    [SerializeField] private Image border;
    [SerializeField] private Image background;

    [Header("색상 테마")]
    [SerializeField] private Color normalBorderColor = new Color(1, 1, 1, 0.2f);
    [SerializeField] private Color selectedBorderColor = new Color(1, 0.85f, 0.2f, 1.0f);
    [SerializeField] private Color normalBackgroundColor = new Color(1, 1, 1, 0.05f);
    [SerializeField] private Color selectedBackgroundColor = new Color(1, 0.98f, 0.7f, 0.2f);

    [Header("선택 그룹(필수)")]
    [SerializeField] private UISelectionGroup selectionGroup;

    private bool selected = false;

    private void Awake()
    {
        ApplyVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectionGroup != null)
        {
            selectionGroup.NotifySelected(this);
        }
        else
        {
            Select();
        }
    }

    public void Select()
    {
        if (selected == true)
        {
            return;
        }

        selected = true;
        ApplyVisual();
    }

    public void Deselect()
    {
        if (selected == false)
        {
            return;
        }

        selected = false;
        ApplyVisual();
    }

    private void ApplyVisual()
    {
        if (border != null)
        {
            if (selected == true)
            {
                border.color = selectedBorderColor;
            }
            else
            {
                border.color = normalBorderColor;
            }
        }

        if (background != null)
        {
            if (selected == true)
            {
                background.color = selectedBackgroundColor;
            }
            else
            {
                background.color = normalBackgroundColor;
            }
        }
    }
}
