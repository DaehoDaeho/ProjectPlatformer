using UnityEngine;

/// <summary>
/// PlayerAnimController
/// 기능: 플레이어의 이동/점프 상태를 단순 규칙으로 판정하고,
///       SpriteAnimator 에 Idle/Move/Jump 중 하나를 재생하도록 지시합니다.
/// 상태 판정:
///  - Jump: 바닥이 아니거나, 수직 속도가 일정 임계 이상 위쪽이면 점프로 간주.
///  - Move: 바닥 위이며, 수평 속도의 절댓값이 이동 임계 이상.
///  - Idle: 그 외의 경우.
/// 의존:
///  - Rigidbody2D: 속도 측정.
///  - 바닥 감지: 발밑에서 짧은 Raycast로 Ground 레이어 검사.
/// 안전:
///  - 모든 임계값과 레이어 마스크는 인스펙터에서 조정 가능.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimController : MonoBehaviour
{
    [Header("참조")]
    [Tooltip("스프라이트 프레임 애니메이터(필수).")]
    [SerializeField] private SpriteAnimator animator;

    [Tooltip("속도를 읽을 Rigidbody2D(필수).")]
    [SerializeField] private Rigidbody2D rb;

    [Header("바닥 감지")]
    [Tooltip("발 아래 레이 캐스트의 시작 위치. 플레이어 발 위치 근처로 배치하세요.")]
    [SerializeField] private Transform groundCheck;

    [Tooltip("바닥 감지에 사용할 레이어 마스크(타일맵/지형).")]
    [SerializeField] private LayerMask groundMask;

    [Tooltip("발밑 아래로 쏠 레이 길이(미터). 0.1~0.2 권장.")]
    [SerializeField] private float groundRayLength = 0.15f;

    [Header("상태 임계값")]
    [Tooltip("수평 속도의 절댓값이 이 값 이상이면 Move 상태.")]
    [SerializeField] private float moveSpeedThreshold = 0.1f;

    [Tooltip("수직 속도가 이 값 이상이면 Jump 상태로 간주(양수=상향).")]
    [SerializeField] private float jumpUpwardThreshold = 0.2f;

    [Header("클립 이름")]
    [Tooltip("Idle 상태에서 재생할 클립 이름.")]
    [SerializeField] private string idleClipName = "Idle";

    [Tooltip("Move 상태에서 재생할 클립 이름.")]
    [SerializeField] private string moveClipName = "Move";

    [Tooltip("Jump 상태에서 재생할 클립 이름.")]
    [SerializeField] private string jumpClipName = "Jump";

    // 내부 상태 캐시: 바닥 접지 여부.
    private bool isGrounded = false;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<SpriteAnimator>();
        }

        if (groundCheck == null)
        {
            // 발밑 기준점이 없으면 자신의 Transform 을 사용(덜 정확).
            groundCheck = transform;
        }
    }

    private void Update()
    {
        // 1) 바닥 감지 업데이트.
        UpdateGrounded();

        // 2) 현재 속도 측정.
        // 현재 속도.     
        Vector2 v = rb != null ? rb.linearVelocity : Vector2.zero;
        // 수평 속도의 절댓값.
        float speedXAbs = Mathf.Abs(v.x);
        // 수직 속도.
        float vy = v.y;

        // 3) 상태 결정: Jump > Move > Idle 우선순위
        // 점프 상태로 볼지 여부.
        bool shouldJump = false;

        if (isGrounded == false)
        {
            shouldJump = true;
        }
        else
        {
            if (vy >= jumpUpwardThreshold)
            {
                shouldJump = true;
            }
        }

        if (shouldJump == true)
        {
            PlaySafe(jumpClipName);
            return;
        }

        if (speedXAbs >= moveSpeedThreshold)
        {
            PlaySafe(moveClipName);
            return;
        }

        PlaySafe(idleClipName);
    }

    /// <summary>
    /// groundCheck 위치에서 아래로 짧은 레이를 쏴 Ground 레이어와 접촉하는지 검사.
    /// </summary>
    private void UpdateGrounded()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        // 레이 시작점(groundCheck 위치).
        Vector2 origin = groundCheck.position;
        
        // 아래 방향 벡터(명시적 값 사용).
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

        // 디버그(원하면 주석 해제)
         Debug.DrawRay(origin, direction * length, isGrounded == true ? Color.green : Color.red);
    }

    /// <summary>
    /// 애니메이터가 존재하고 클립 이름이 비어있지 않을 때만 안전하게 재생을 지시.
    /// </summary>
    private void PlaySafe(string clipName)
    {
        if (animator == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(clipName) == true)
        {
            return;
        }

        animator.Play(clipName);
    }
}
