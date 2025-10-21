using UnityEngine;
using System.Collections.Generic;

// 파일: UISelectionGroup.cs
// 설치: 카드들을 담는 부모 오브젝트에 부착.
// 역할: 그룹 내에서 단 하나의 카드만 선택되도록 관리합니다.
public class UISelectionGroup : MonoBehaviour
{
    [Header("초기 선택(선택)")]
    [SerializeField] private UISelectableCard initialSelected;

    private List<UISelectableCard> cards = new List<UISelectableCard>();
    private UISelectableCard current;

    private void Awake()
    {
        cards.Clear();

        UISelectableCard[] found = GetComponentsInChildren<UISelectableCard>(true);

        for (int i = 0; i < found.Length; i = i + 1)
        {
            cards.Add(found[i]);
        }
    }

    private void Start()
    {
        if (initialSelected != null)
        {
            NotifySelected(initialSelected);
        }
    }

    public void NotifySelected(UISelectableCard card)
    {
        if (card == null)
        {
            return;
        }

        if (current == card)
        {
            return;
        }

        if (current != null)
        {
            current.Deselect();
        }

        current = card;
        current.Select();
    }
}
