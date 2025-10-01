using UnityEngine;

// 파일: EnemyShooter2D.cs
// 설치: 적 오브젝트에 부착. sight, muzzle, projectilePrefab 연결.
public class EnemyShooter2D : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private EnemySight2D sight;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject projectilePrefab;

    [Header("발사 설정")]
    [SerializeField] private float shootRange = 6.0f;
    [SerializeField] private float shootCooldown = 1.0f;

    [Header("환경 마스크(차단 확인 용)")]
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
        // 1) 조준 회전: muzzle.right가 dir이 되도록
        if (dir.sqrMagnitude > 0.0f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector3 e = muzzle.eulerAngles;
            e.z = angle;
            muzzle.eulerAngles = e;
        }

        // 2) 발사체 생성
        GameObject go = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

        // 3) 발사체에 "스폰한 적"의 콜라이더와 IgnoreCollision 설정(자기타격 방지)
        Collider2D myCol = GetComponent<Collider2D>();
        Collider2D projCol = go.GetComponent<Collider2D>();
        if ((myCol != null) && (projCol != null))
        {
            Physics2D.IgnoreCollision(projCol, myCol, true);
        }
    }
}
