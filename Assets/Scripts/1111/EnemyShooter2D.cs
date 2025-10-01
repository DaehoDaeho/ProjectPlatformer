using UnityEngine;

// ����: EnemyShooter2D.cs
// ��ġ: �� ������Ʈ�� ����. sight, muzzle, projectilePrefab ����.
public class EnemyShooter2D : MonoBehaviour
{
    [Header("����")]
    [SerializeField] private EnemySight2D sight;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject projectilePrefab;

    [Header("�߻� ����")]
    [SerializeField] private float shootRange = 6.0f;
    [SerializeField] private float shootCooldown = 1.0f;

    [Header("ȯ�� ����ũ(���� Ȯ�� ��)")]
    [SerializeField] private LayerMask groundMask;

    private float cooldownTimer = 0.0f;

    private void Update()
    {
        if (cooldownTimer > 0.0f)
        {
            cooldownTimer = cooldownTimer - Time.deltaTime;
        }

        if ((sight == null) || (muzzle == null) || (projectilePrefab == null))
        {
            return;
        }

        bool canSee = sight.CanSeePlayer();
        if (canSee == false)
        {
            return;
        }

        Vector2 playerPos = sight.GetPlayerPosition();
        Vector2 muzzlePos = muzzle.position;
        Vector2 toPlayer = playerPos - muzzlePos;
        float distance = toPlayer.magnitude;

        if (distance > shootRange)
        {
            return;
        }

        if (cooldownTimer <= 0.0f)
        {
            Fire(toPlayer.normalized);
            cooldownTimer = shootCooldown;
        }
    }

    private void Fire(Vector2 dir)
    {
        // 1) ���� ȸ��: muzzle.right�� dir�� �ǵ���
        if (dir.sqrMagnitude > 0.0f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector3 e = muzzle.eulerAngles;
            e.z = angle;
            muzzle.eulerAngles = e;
        }

        // 2) �߻�ü ����
        GameObject go = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

        // 3) �߻�ü�� "������ ��"�� �ݶ��̴��� IgnoreCollision ����(�ڱ�Ÿ�� ����)
        Collider2D myCol = GetComponent<Collider2D>();
        Collider2D projCol = go.GetComponent<Collider2D>();
        if ((myCol != null) && (projCol != null))
        {
            Physics2D.IgnoreCollision(projCol, myCol, true);
        }
    }
}
