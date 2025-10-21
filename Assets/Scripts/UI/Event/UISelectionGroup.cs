using UnityEngine;
using System.Collections.Generic;

public class UISelectionGroup : MonoBehaviour
{
    [SerializeField]
    private UISelectableCard initialSelected;

    private List<UISelectableCard> cards = new List<UISelectableCard>();
    private UISelectableCard current;

    public void Init()
    {
        cards.Clear();

        UISelectableCard[] found = GetComponentsInChildren<UISelectableCard>(true);

        for (int i = 0; i < found.Length; ++i)
        {
            cards.Add(found[i]);
            found[i].selectionGroup = this;
        }

        if (initialSelected != null)
        {
            NotifySelected(initialSelected);
        }
    }

    public void NotifySelected(UISelectableCard card)
    {
        if(card == null)
        {
            return;
        }

        if (current == card)
        {
            return;
        }

        if(current != null)
        {
            current.Deselect();
        }

        current = card;
        current.Select();
    }
}
