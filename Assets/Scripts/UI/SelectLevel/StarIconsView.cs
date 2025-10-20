using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarIconsView : MonoBehaviour
{
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private TMP_Text starText;

    public void SetStars(int stars)
    {
        if (stars < 0)
        {
            stars = 0;
        }
        if (stars > 3)
        {
            stars = 3;
        }

        if (star1 != null)
        {
            star1.enabled = (stars >= 1);
        }
        if (star2 != null)
        {
            star2.enabled = (stars >= 2);
        }
        if (star3 != null)
        {
            star3.enabled = (stars >= 3);
        }
        if (starText != null)
        {
            starText.text = "Stars: " + stars.ToString() + " / 3";
        }
    }
}
