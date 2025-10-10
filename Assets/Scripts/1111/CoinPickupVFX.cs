using UnityEngine;

// 파일: CoinPickupVFX.cs
// 설치: 코인 프리팹(CollectibleCoin과 함께) 부착.
// 점수는 건드리지 않고, 피드백 이벤트만 방송합니다.
[RequireComponent(typeof(Collider2D))]
public class CoinPickupVFX : MonoBehaviour
{
    [SerializeField] private int displayValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isPlayer = (other.CompareTag("Player") == true);
        if (isPlayer == true)
        {
            GameplayEvents.RaiseCoinCollected(transform.position, displayValue);
            // 이 컴포넌트는 코인 파괴를 수행하지 않습니다.
            // 파괴는 기존 CollectibleCoin이 담당합니다.
        }
    }
}
