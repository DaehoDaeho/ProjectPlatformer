using UnityEngine;

public class SfxOneShotPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinClip;

    [SerializeField]
    private float volume = 0.9f;

    private void OnEnable()
    {
        GameplayEvents.OnCoinCollected += HandleCoin;
    }

    private void OnDisable()
    {
        GameplayEvents.OnCoinCollected -= HandleCoin;
    }

    private void HandleCoin(Vector3 pos, int value)
    {
        if(coinClip != null)
        {
            AudioSource.PlayClipAtPoint(coinClip, pos, volume);
        }
    }
}
