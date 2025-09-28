using UnityEngine;

// 파일: DamageOnTouch.cs
// 설치: 적(또는 위험물)에 부착. Is Trigger 여부에 따라 useTrigger 선택.
public class DamageOnTouch : MonoBehaviour
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
            PlayerRespawn pr = collision.collider.GetComponent<PlayerRespawn>();
            if (pr != null)
            {
                pr.Respawn();
            }
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
            PlayerRespawn pr = other.GetComponent<PlayerRespawn>();
            if (pr != null)
            {
                pr.Respawn();
            }
        }
    }
}
