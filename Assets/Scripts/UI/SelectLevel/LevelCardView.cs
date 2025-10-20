using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCardView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private Image previewImage;
    [SerializeField] private Button playButton;
    [SerializeField] private StarIconsView starIcons;

    private LevelMeta currentMeta;

    public void SetData(LevelMeta meta)
    {
        currentMeta = meta;

        if (titleText != null)
        {
            titleText.text = meta.displayName;
        }

        if (previewImage != null)
        {
            if (meta.preview != null)
            {
                previewImage.sprite = meta.preview;
            }
        }

        float bestSec = LevelSaveSystemPlus.GetBestTime(meta.levelId);
        int bestStars = LevelSaveSystemPlus.GetBestStars(meta.levelId);

        if (bestTimeText != null)
        {
            bestTimeText.text = "Best: " + LevelSaveSystemPlus.FormatTime(bestSec);
        }

        if (starIcons != null)
        {
            int shown = (bestStars >= 0) ? bestStars : 0;
            starIcons.SetStars(shown);
        }

        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(OnClickedPlay);
        }
    }

    private void OnClickedPlay()
    {
        if (currentMeta == null)
        {
            return;
        }

        SceneTransitionController trans = FindAnyObjectByType<SceneTransitionController>();
        if (trans != null)
        {
            trans.LoadSceneByName(currentMeta.sceneName);
            return;
        }

        if (string.IsNullOrEmpty(currentMeta.sceneName) == false)
        {
            SceneManager.LoadScene(currentMeta.sceneName);
        }
    }
}
