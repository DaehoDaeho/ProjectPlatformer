using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectableCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image border;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Color normalBorderColor = new Color(1, 1, 1, 0.2f);

    [SerializeField]
    private Color selectedBorderColor = new Color(1, 0.85f, 0.2f, 1.0f);

    [SerializeField]
    private Color normalBackgroundColor = new Color(1, 1, 1, 0.05f);

    [SerializeField]
    private Color selectedBackgroundColor = new Color(1, 0.98f, 0.7f, 0.2f);

    [SerializeField]
    UISelectionBridge bridge;

    public UISelectionGroup selectionGroup;

    private bool selected = false;

    void Awake()
    {
        ApplyVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectionGroup != null)
        {
            selectionGroup.NotifySelected(this);
        }
        else
        {
            Select();
        }
    }

    void ApplyVisual()
    {
        if(border != null)
        {
            if(selected == true)
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

    public void Select()
    {
        if(selected == true)
        {
            return;
        }

        selected = true;
        ApplyVisual();

        bridge.OnSelected();
    }

    public void Deselect()
    {
        if(selected == false)
        {
            return;
        }

        selected = false;
        ApplyVisual();

        bridge.OnDeselected();
    }
}
