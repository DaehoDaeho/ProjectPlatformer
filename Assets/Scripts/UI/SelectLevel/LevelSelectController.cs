using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField] private LevelMeta[] levelMetas;
    [SerializeField] private Transform cardsParent;
    [SerializeField] private GameObject levelCardPrefab;

    private void Start()
    {
        if (cardsParent == null)
        {
            Debug.LogWarning("LevelSelectController: cardsParent 누락");
            return;
        }
        if (levelCardPrefab == null)
        {
            Debug.LogWarning("LevelSelectController: levelCardPrefab 누락");
            return;
        }
        if (levelMetas == null)
        {
            Debug.LogWarning("LevelSelectController: levelMetas 비어 있음");
            return;
        }

        for (int i = 0; i < levelMetas.Length; i = i + 1)
        {
            LevelMeta meta = levelMetas[i];
            if (meta == null)
            {
                continue;
            }

            GameObject go = Instantiate(levelCardPrefab, cardsParent);
            LevelCardView view = go.GetComponent<LevelCardView>();
            if (view != null)
            {
                view.SetData(meta);
            }
        }
    }
}
