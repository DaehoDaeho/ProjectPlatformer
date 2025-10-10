using UnityEngine;

// 파일: ScorePopupSpawner.cs
// 설치: GameManager 등 씬에 1개. popupPrefab은 '월드 스페이스' TMP 프리팹.
public class ScorePopupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Camera cameraForWorldCanvas;

    private void OnEnable()
    {
        GameplayEvents.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        GameplayEvents.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(Vector3 worldPos, int value)
    {
        if (popupPrefab == null)
        {
            return;
        }

        GameObject go = Instantiate(popupPrefab, worldPos, Quaternion.identity);
        FloatingText ft = go.GetComponent<FloatingText>();
        if (ft != null)
        {
            ft.SetText("+" + value.ToString());
        }

        WorldSpaceCanvasBinder binder = go.GetComponent<WorldSpaceCanvasBinder>();
        if (binder != null)
        {
            binder.BindCamera(cameraForWorldCanvas);
        }
    }
}
