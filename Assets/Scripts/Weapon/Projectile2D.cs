using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 12.0f;

    [SerializeField]
    private float lifeTime = 3.0f;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private Rigidbody2D body;

    private float spawnTime = 0.0f;

    private void OnEnable()
    {
        spawnTime = Time.time;
        body.linearVelocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = (collision.CompareTag("Player") == true);
        if (isPlayer == true)
        {
            //PlayerRespawn pr = collision.GetComponent<PlayerRespawn>();
            //if (pr != null)
            //{
            //    pr.Respawn();
            //}
            PlayerHealth ph = collision.GetComponent<PlayerHealth>();
            if(ph != null)
            {
                ph.ApplyDamage();
            }

            Destroy(gameObject);
            return;
        }

        int otherLayer = collision.gameObject.layer;
        bool isGround = ((groundMask.value & (1 << otherLayer)) != 0);
        if(isGround == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int otherLayer = collision.gameObject.layer;
        bool isGround = ((groundMask.value & (1 << otherLayer)) != 0);
        if (isGround == true)
        {
            Destroy(gameObject);
        }
    }
}
