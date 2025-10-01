using UnityEngine;

public class EnemyShooter2D : MonoBehaviour
{
    [SerializeField]
    private EnemySight2D sight;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float shootRange = 6.0f;

    [SerializeField]
    private float shootCooldown = 1.0f;

    [SerializeField]
    private LayerMask groundMask;

    private float cooldownTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if(sight == null || muzzle == null || projectilePrefab == null)
        {
            return;
        }

        bool canSee = sight.CanSeePlayer();
        if(canSee == false)
        {
            return;
        }

        Vector2 playerPos = sight.GetPlayerPosition();
        Vector2 muzzlePos = muzzle.position;
        Vector2 toPlayer = playerPos - muzzlePos;
        float distance = toPlayer.magnitude;

        if(distance > shootRange)
        {
            return;
        }

        if(cooldownTimer <= 0.0f)
        {
            Fire(toPlayer.normalized);
            cooldownTimer = shootCooldown;
        }
    }

    void Fire(Vector2 dir)
    {
        if(dir.sqrMagnitude > 0.0f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector3 e = muzzle.eulerAngles;
            e.z = angle;
            muzzle.eulerAngles = e;
        }

        GameObject go = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

        Collider2D myCol = GetComponent<Collider2D>();
        Collider2D projCol = go.GetComponent<Collider2D>();
        if(myCol != null && projCol != null)
        {
            Physics2D.IgnoreCollision(projCol, myCol, true);
        }
    }
}
