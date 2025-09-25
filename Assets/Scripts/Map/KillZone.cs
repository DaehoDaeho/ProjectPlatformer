using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            PlayerRespawn pr = collision.GetComponent<PlayerRespawn>();
            if (pr != null)
            {
                pr.Respawn();
            }
        }
    }
}
