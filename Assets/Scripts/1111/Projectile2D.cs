using UnityEngine;

// ����: Projectile2D.cs
// ��ġ: ����ü �����տ� ����. Rigidbody2D(Kinematic) + Collider2D(Is Trigger) �ʿ�.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile2D : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float lifeTime = 3.0f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D body;
    private float spawnTime = 0.0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        Vector2 dir = transform.right;
        body.linearVelocity = dir * speed;
    }

    private void Update()
    {
        if ((Time.time - spawnTime) >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isPlayer = (other.CompareTag("Player") == true);
        if (isPlayer == true)
        {
            PlayerRespawn pr = other.GetComponent<PlayerRespawn>();
            if (pr != null)
            {
                pr.Respawn();
            }
            Destroy(gameObject);
            return;
        }

        // ȯ�� �浹: groundMask ���
        int otherLayer = other.gameObject.layer;
        bool isGround = ((groundMask.value & (1 << otherLayer)) != 0);
        if (isGround == true)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ȥ�� Ʈ���� ��� �ݶ��̴� ���� �� ���� ����� ��ȣ ����
        int otherLayer = collision.gameObject.layer;
        bool isGround = ((groundMask.value & (1 << otherLayer)) != 0);
        if (isGround == true)
        {
            Destroy(gameObject);
            return;
        }
    }
}
