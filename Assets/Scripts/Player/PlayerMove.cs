using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpPower = 0.5f;
    public float groundCheckRadius = 0.12f;
    public Transform footPoint;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    public Rigidbody2D rb;
    public float rayLength = 0.2f;
    public bool useRaycast = true;

    public SpriteRenderer sp;

    public float targetUpwardSpeed = 6.0f;
    public float extraUpwardBonusIfFalling = 3.0f;
    public float maxUpwardSpeed = 8.0f;

    private float moveInput;
    private bool isGrounded = false;
    private bool wantsToJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        // 좌측 화살표 키나 A 키를 눌렀을 경우 0보다 작은 값이 반환,
        // 우측 화살표 키나 D 키를 눌렀을 경우 0보다 큰 값이 반환.

        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            ApplyBounce();
        }

        //SetDirection();
    }

    void Jump()
    {
        if (footPoint != null)
        {
            if(useRaycast == true)
            {
                RaycastHit2D hit = Physics2D.Raycast((Vector2)footPoint.position, Vector2.down, 0.2f, groundMask);
                if(hit.collider != null)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
            }
            else
            {
                Collider2D hit = Physics2D.OverlapCircle((Vector2)footPoint.position, groundCheckRadius, groundMask);
                if (hit != null)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
            }
        }

        if (isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0.0f);
            rb.AddForce(new Vector2(0.0f, jumpPower), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)footPoint.position, Vector2.down, 0.2f, enemyMask);
        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
            ApplyBounce();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (footPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(footPoint.position, groundCheckRadius);
        }
    }

    private void SetDirection()
    {
        if(sp != null)
        {
            if(moveInput < 0.0f)
            {
                sp.flipX = true;
            }
            else if(moveInput > 0.0f)
            {
                sp.flipX = false;
            }
        }
    }

    /// <summary>
    /// 실제 튕김 처리를 적용한다.
    /// </summary>
    private void ApplyBounce()
    {
        if(rb == null)
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
