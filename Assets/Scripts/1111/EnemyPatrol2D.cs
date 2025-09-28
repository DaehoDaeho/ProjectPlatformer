using UnityEngine;

// 파일: EnemyPatrol2D.cs
// 설치: 적 오브젝트(스프라이트 + Rigidbody2D + Collider2D)에 부착.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyPatrol2D : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private int startDirection = 1; // 1=오른쪽, -1=왼쪽

    [Header("감지 설정")]
    [SerializeField] private Transform sensorPoint;           // 몸 앞·아래 기준점(자식)
    [SerializeField] private float wallCheckDistance = 0.25f; // 전방 벽 감지 거리
    [SerializeField] private float ledgeCheckDistance = 0.35f;// 아래 낭떠러지 감지 거리
    [SerializeField] private LayerMask groundMask;

    [Header("시각 반전 선택")]
    [SerializeField] private bool useFlipX = true;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private int direction = 1;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        direction = (startDirection >= 0) ? 1 : -1;

        if (sensorPoint != null)
        {
            AlignSensorToDirection(direction);
        }

        ApplyVisualFacing();
    }

    private void FixedUpdate()
    {
        Vector2 v = body.linearVelocity;
        v.x = direction * moveSpeed;
        body.linearVelocity = v;

        if (sensorPoint == null)
        {
            return;
        }

        Vector2 origin = sensorPoint.position;
        Vector2 dirForward = sensorPoint.right;                 // 회전에 따라 '앞'이 결정
        Vector2 dirDown = (Vector2)(-sensorPoint.up);           // Transform에는 down이 없으므로 -up 사용

        RaycastHit2D hitWall = Physics2D.Raycast(origin, dirForward, wallCheckDistance, groundMask);
        bool hasWall = (hitWall.collider != null);

        RaycastHit2D hitDown = Physics2D.Raycast(origin, dirDown, ledgeCheckDistance, groundMask);
        bool hasGround = (hitDown.collider != null);

        if ((hasWall == true) || (hasGround == false))
        {
            TurnAround();
        }
    }

    private void TurnAround()
    {
        direction = direction * -1;

        if (sensorPoint != null)
        {
            // 위치 반전: 몸 앞쪽을 유지
            Vector3 p = sensorPoint.localPosition;
            p.x = p.x * -1.0f;
            sensorPoint.localPosition = p;

            // 방향 반전: Y축 180도 토글로 sensorPoint.right 반전
            Vector3 e = sensorPoint.localEulerAngles;
            e.y = (e.y + 180.0f) % 360.0f;
            sensorPoint.localEulerAngles = e;
        }

        ApplyVisualFacing();
    }

    private void AlignSensorToDirection(int dir)
    {
        Vector3 p = sensorPoint.localPosition;
        p.x = Mathf.Abs(p.x) * (dir > 0 ? 1.0f : -1.0f);
        sensorPoint.localPosition = p;

        Vector3 e = sensorPoint.localEulerAngles;
        e.y = (dir > 0) ? 0.0f : 180.0f;
        sensorPoint.localEulerAngles = e;
    }

    private void ApplyVisualFacing()
    {
        if (useFlipX == true)
        {
            if (sprite != null)
            {
                bool facingLeft = (direction < 0);
                sprite.flipX = facingLeft;
            }
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (direction > 0 ? 1.0f : -1.0f);
            transform.localScale = s;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (sensorPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(sensorPoint.position, sensorPoint.position + (Vector3)(sensorPoint.right * wallCheckDistance));

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(sensorPoint.position, sensorPoint.position + (Vector3)((-sensorPoint.up) * ledgeCheckDistance));
        }
    }
}
