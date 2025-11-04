using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.collider.CompareTag("Player") == true;
        if(isPlayer == true)
        {
            //PlayerRespawn pr = collision.collider.GetComponent<PlayerRespawn>();
            //if(pr != null)
            //{
            //    pr.Respawn();
            //}

            PlayerHealth ph = collision.collider.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.ApplyDamage();
            }
        }
    }
}
