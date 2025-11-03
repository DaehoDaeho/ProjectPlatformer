using UnityEngine;

public class PlayerJumpAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform footPoint;
    public LayerMask enemyMask;

    public float rayLength = 0.2f;
    public float targetUpwardSpeed = 6.0f;
    public float extraUpwardBonusIfFalling = 3.0f;
    public float maxUpwardSpeed = 8.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)footPoint.position, Vector2.down, 0.2f, enemyMask);
        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
            ApplyJumpAttack();
        }
    }

    /// <summary>
    /// 밟기 공격 적용.
    /// </summary>
    private void ApplyJumpAttack()
    {
        if (rb == null)
        {
            return;
        }

        // 대상의 현재 속도를 가져와서 저장한다.
        Vector2 v = rb.linearVelocity;

        // 대상의 현재 속도 중 Y 축 속도를 저장한다.
        float vy = v.y;

        // 낙하 중이면 추가 보정으로 더 강하게 끌어올림.
        float desired = targetUpwardSpeed;

        if (vy < 0.0f)
        {
            desired = desired + extraUpwardBonusIfFalling;
        }

        // 목표치보다 낮으면 끌어올리고, 이미 높으면 현상태를 유지한다.
        if (vy < desired)
        {
            vy = desired;
        }

        // 과도한 상향 속도 방지.
        if (vy > maxUpwardSpeed)
        {
            vy = maxUpwardSpeed;
        }

        v.y = vy;

        // 최종 이동속도 세팅.
        rb.linearVelocity = v;
    }
}
