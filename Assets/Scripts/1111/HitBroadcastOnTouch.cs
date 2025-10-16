using UnityEngine;

// 파일: HitBroadcastOnTouch.cs
// 설치: 적/함정 오브젝트에 기존 DamageOnTouch와 함께 부착.
// 충돌 시 PlayerHit 이벤트만 방송합니다. 리스폰은 DamageOnTouch가 처리합니다.
public class HitBroadcastOnTouch : MonoBehaviour
{
    [SerializeField] private bool useTrigger = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTrigger == true)
        {
            return;
        }

        bool isPlayer = (collision.collider.CompareTag("Player") == true);
        if (isPlayer == true)
        {
            GameplayEvents.RaisePlayerHit(collision.contacts[0].point);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (useTrigger == false)
        {
            return;
        }

        bool isPlayer = (other.CompareTag("Player") == true);
        if (isPlayer == true)
        {
            GameplayEvents.RaisePlayerHit(other.transform.position);
        }
    }
}
