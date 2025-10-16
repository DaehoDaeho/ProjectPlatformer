using UnityEngine;

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
