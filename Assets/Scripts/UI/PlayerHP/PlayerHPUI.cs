using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    public Image[] hpImage;

    private void OnEnable()
    {
        GameplayEvents.OnChagnedPlayerHP += UpdateHPUI;
    }

    private void OnDisable()
    {
        GameplayEvents.OnChagnedPlayerHP -= UpdateHPUI;
    }

    public void UpdateHPUI(int currentHP)
    {
        if(hpImage != null && hpImage.Length != 0.0f)
        {
            for(int i=0; i<hpImage.Length; ++i)
            {
                hpImage[i].gameObject.SetActive(false);
            }

            for(int i=0; i<currentHP; ++i)
            {
                hpImage[i].gameObject.SetActive(true);
            }
        }
    }
}
