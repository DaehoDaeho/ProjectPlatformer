using UnityEngine;

// ����: ScorePopupSpawner.cs
// ��ġ: GameManager �� ���� 1��. popupPrefab�� '���� �����̽�' TMP ������.
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
