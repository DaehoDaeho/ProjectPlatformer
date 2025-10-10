using UnityEngine;

public class ScorePopupSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject popupPrefab;

    [SerializeField]
    private Camera cameraForWorldCanvas;

    private void OnEnable()
    {
        GameplayEvents.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        GameplayEvents.OnCoinCollected -= HandleCoinCollected;
    }

    void HandleCoinCollected(Vector3 worldPos, int value)
    {
        if(popupPrefab != null)
        {
            GameObject go = Instantiate(popupPrefab, worldPos, Quaternion.identity);
            if(go != null)
            {
                FloatingText ft = go.GetComponent<FloatingText>();
                if(ft != null)
                {
                    ft.SetText("+" + value.ToString());
                }
            }

            WorldSpaceCanvasBinder binder = go.GetComponent<WorldSpaceCanvasBinder>();
            if(binder != null)
            {
                binder.BindCamera(cameraForWorldCanvas);
            }
        }
    }
}
