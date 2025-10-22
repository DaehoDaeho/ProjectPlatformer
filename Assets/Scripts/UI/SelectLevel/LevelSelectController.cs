using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField] private LevelMeta[] levelMetas;
    [SerializeField] private Transform cardsParent;
    [SerializeField] private GameObject levelCardPrefab;
    [SerializeField] private UISelectionGroup selectionGroup;

    //==========================================================
    [SerializeField] private UnlockDatabase unlockDatabase;
    private string[] allLevelIds;

    private void Awake()
    {
        BuildAllLevelIds();
    }

    private void BuildAllLevelIds()
    {
        if (levelMetas == null)
        {
            allLevelIds = new string[0];
            return;
        }

        allLevelIds = new string[levelMetas.Length];

        for (int i = 0; i < levelMetas.Length; i = i + 1)
        {
            LevelMeta meta = levelMetas[i];

            if (meta == null)
            {
                allLevelIds[i] = "";
            }
            else
            {
                allLevelIds[i] = meta.levelId;
            }
        }
    }

    //==========================================================

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
                //view.SetData(meta);
                view.SetData(meta, unlockDatabase, allLevelIds);
            }
        }

        if(selectionGroup != null)
        {
            selectionGroup.Init();
        }
    }
}
