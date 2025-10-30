using UnityEngine;

/// <summary>
/// PlayerAnimator
/// 기능:
///   - Rigidbody2D 속도와 바닥 감지(Raycast)를 기반으로 Animator 파라미터를 갱신.
///   - Idle / Move / Jump 상태를 Animator Controller에서 전이 규칙으로 제어할 수 있도록 값만 정확히 공급.
///   - 이동 방향에 따라 SpriteRenderer.flipX 를 갱신.
/// 사용 파라미터(Animator):
///   - float "Speed"          : |velocity.x|
///   - bool  "IsGrounded"     : 바닥 감지 결과.
/// 설계 원칙:
///   - 입력·물리와 시각(Animator)의 분리: 이 스크립트는 "상태 관측 -> 파라미터 반영"만 담당.
///   - 모든 임계값과 레이어는 인스펙터에서 조정 가능하게 만든다.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    [Header("필수 참조")]
    // 상태 전이를 제어할 Animator. Idle/Move/Jump 상태가 들어 있는 Controller가 연결되어야 한다.
    [SerializeField] private Animator animator;

    // 이동 속도를 읽을 Rigidbody2D.
    [SerializeField] private Rigidbody2D rb;

    // 좌우 반전을 적용할 SpriteRenderer.
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("바닥 감지")]
    // 발 밑 기준점 Transform. 발바닥 아래 약간 위치시키세요.
    [SerializeField] private Transform groundCheck;

    // 바닥으로 인식할 레이어 마스크(타일맵/지형 등).
    [SerializeField] private LayerMask groundMask;

    // 발밑으로 쏠 레이 길이(미터). 0.12~0.2 권장.
    [SerializeField] private float groundRayLength = 0.15f;

    [Header("상태 임계값")]
    // 이 값 이상이면 Move 상태로 간주하는 수평 속도 절댓값.
    [SerializeField] private float moveSpeedThreshold = 0.1f;

    // 플립 전환이 너무 자주 튀는 것을 방지하는 최소 속도. 이 값 미만이면 플립을 보류합니다.
    [SerializeField] private float flipDeadZone = 0.02f;

    [Header("Animator 파라미터 이름")]
    // float 속도 파라미터 이름(예: Speed).
    [SerializeField] private string paramSpeed = "Speed";

    // float 수직속도 파라미터 이름(예: VerticalSpeed).
    [SerializeField] private string paramVerticalSpeed = "VerticalSpeed";

    // bool 지상 여부 파라미터 이름(예: IsGrounded).
    [SerializeField] private string paramIsGrounded = "IsGrounded";

    // trigger 점프 파라미터 이름(선택). 점프 입력 시스템과 연동 시 사용.
    [SerializeField] private string paramJumpTrigger = "Jump";

    // 현재 지상에 착지한 상태인지 여부.
    private bool isGrounded = false;

    private void Awake()
    {
        // Animator 캐시.
        Animator a = animator;

        if (a == null)
        {
            a = GetComponentInChildren<Animator>();
        }
        animator = a;

        // Rigidbody2D 캐시.
        Rigidbody2D r = rb;

        if (r == null)
        {
            r = GetComponent<Rigidbody2D>();
        }

        rb = r;

        // SpriteRenderer 캐시.
        SpriteRenderer sr = spriteRenderer;

        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        spriteRenderer = sr;

        if (groundCheck == null)
        {
            groundCheck = transform;
        }
    }

    private void Update()
    {
        // 1) 바닥 감지.
        UpdateGrounded();

        // 2) 속도 샘플링.
        // 현재 속도.
        Vector2 v = rb != null ? rb.linearVelocity : Vector2.zero;

        // 수평 속도.
        float speedX = v.x;

        // 수평 속도의 절댓값.
        float speedXAbs = Mathf.Abs(speedX);

        // 수직 속도.
        float speedY = v.y;

        // 3) Animator 파라미터 갱신.
        ApplyAnimatorParams(speedXAbs, speedY, isGrounded);

        // 4) 좌우 반전 처리.
        UpdateFlipX(speedX);
    }

    /// <summary>
    /// groundCheck 위치에서 아래 방향으로 레이를 발사하여 지면을 감지.
    /// 결과는 isGrounded 필드에 반영.
    /// </summary>
    private void UpdateGrounded()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        // 레이 시작점.
        Vector2 origin = groundCheck.position;

        //아래 방향 벡터.
        Vector2 direction = new Vector2(0.0f, -1.0f);

        // 레이 길이.
        float length = groundRayLength;

        // 레이캐스트 결과.
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, length, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // 필요 시 디버그 레이:
        // Debug.DrawRay(origin, direction * length, isGrounded == true ? Color.green : Color.red);
    }

    /// <summary>
    /// Animator 파라미터들을 안전하게 갱신.
    /// - Speed           : 수평 속도 절댓값.
    /// - VerticalSpeed   : 수직 속도.
    /// - IsGrounded      : 지상 여부.
    /// </summary>
    private void ApplyAnimatorParams(float speedAbs, float verticalSpeed, bool grounded)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetFloat(paramSpeed, speedAbs);
        //animator.SetFloat(paramVerticalSpeed, verticalSpeed);
        animator.SetBool(paramIsGrounded, grounded);
    }

    /// <summary>
    /// 수평 속도를 기준으로 스프라이트 좌우 반전을 갱신.
    /// flipDeadZone 미만의 미세 속도에서는 반전을 갱신하지 않아 깜빡임을 방지.
    /// </summary>
    private void UpdateFlipX(float speedX)
    {
        if (spriteRenderer == null)
        {
            return;
        }

        float ax = Mathf.Abs(speedX);

        if (ax < flipDeadZone)
        {
            return;
        }

        // 오른쪽 이동이면 flipX=false, 왼쪽 이동이면 flipX=true.
        if (speedX > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// 외부(입력 시스템)에서 점프가 개시되었을 때 호출해 줄 수 있는 보조 메서드입니다.
    /// Animator에 Jump 트리거를 발화합니다.
    /// </summary>
    public void FireJumpTrigger()
    {
        if (animator == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(paramJumpTrigger) == false)
        {
            animator.ResetTrigger(paramJumpTrigger);
            animator.SetTrigger(paramJumpTrigger);
        }
    }
}
