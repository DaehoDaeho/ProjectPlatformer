using UnityEngine;

// 파일: SfxOneShotPlayer.cs
// 설치: GameManager 등 씬에 1개.
public class SfxOneShotPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private float volume = 0.9f;

    private void OnEnable()
    {
        GameplayEvents.OnCoinCollected += HandleCoin;
        GameplayEvents.OnPlayerHit += HandleHit;
    }

    private void OnDisable()
    {
        GameplayEvents.OnCoinCollected -= HandleCoin;
        GameplayEvents.OnPlayerHit -= HandleHit;
    }

    private void HandleCoin(Vector3 pos, int value)
    {
        if (coinClip != null)
        {
            AudioSource.PlayClipAtPoint(coinClip, pos, volume);
        }
    }

    private void HandleHit(Vector3 pos)
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, pos, volume);
        }
    }
}
