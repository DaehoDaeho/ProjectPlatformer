using UnityEngine;

/// <summary>
/// 플레이어의 점프 공격 처리.
/// 플레이어의 발 밑으로 Ray를 쏴서 Enemy 레이어로 지정된 오브젝트가 맞았을 경우 공격 판정.
/// 우선은 적 오브젝트는 파괴시키고, 플레이어는 다시 위로 오른다.
/// </summary>
public class PlayerJumpAttack : MonoBehaviour
{
    public Rigidbody2D rb;  // 플레이어의 Rigidbody
    public Transform footPoint; // 플레이어의 발 위치로 지정된 자식 오브젝트.
    public LayerMask enemyMask; // Ray로 체크할 대상의 Layer.

    public float rayLength = 0.2f;  // 광선의 길이.
    public float targetUpwardSpeed = 6.0f;  // 튕긴 뒤 목표로 삼을 위쪽 속도. 현재 속도가 이보다 낮으면 올려준다.
    public float extraUpwardBonusIfFalling = 3.0f;  // 낙하 중일 때 추가로 더 얹어줄 상향 속도. 음수 낙하를 상쇄해 깔끔하게 튕긴다.
    public float maxUpwardSpeed = 8.0f; // 튕긴 뒤 허용할 최대 상향 속도. 과도한 가속 방지.

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)footPoint.position, Vector2.down, rayLength, enemyMask);
        if (hit.collider != null)
        {
            ApplyJumpAttack(hit);
        }
    }

    /// <summary>
    /// 밟기 공격 적용.
    /// </summary>
    private void ApplyJumpAttack(RaycastHit2D hit)
    {
        if (rb == null)
        {
            return;
        }

        // 카메라 흔들림 추가.
        GameplayEvents.RaiseEnemyDefeated(hit.collider.transform.position);

        EnemyState state = hit.collider.GetComponent<EnemyState>();
        if(state != null)
        {
            state.PlayHitAnimation();
            state.SetAlive(false);
        }

        // 밟힌 적을 파괴.
        Destroy(hit.collider.gameObject, 1.0f);

        // 플레이어의 현재 속도를 가져와서 저장한다.
        Vector2 v = rb.linearVelocity;

        // 플레이ㅓ의 현재 속도 중 Y 축 속도를 저장한다.
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
