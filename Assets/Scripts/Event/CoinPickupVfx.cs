using UnityEngine;

public class CoinPickupVfx : MonoBehaviour
{
    [SerializeField]
    private int displayValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = (collision.CompareTag("Player") == true);
        if(isPlayer == true)
        {
            GameplayEvents.RaiseCoinCollected(transform.position, displayValue);
        }
    }
}
