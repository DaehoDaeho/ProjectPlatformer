using UnityEngine;

// SpringPad.cs
// 스프링 점프대 오브젝트에 부착.
// 플레이어가 점프대에 닿으면 위쪽으로 튕겨 올라가게 한다.
// 이 스크립트를 동작시키기 위해서는 Collider2D가 필요하다.
[RequireComponent(typeof(Collider2D))]
public class SpringPad : MonoBehaviour
{
    public enum JumpPower
    {
        Weak = 0,
        Middle = 1,
        Strong = 2,
    }

    [SerializeField] private JumpPower jumpPower = JumpPower.Weak;

    // 스프링에 닿았을 때 반응해야 하는 대상(Player).
    [SerializeField] private LayerMask triggerMask;
    
    // 튕긴 뒤 목표로 삼을 위쪽 속도. 현재 속도가 이보다 낮으면 올려준다.
    [SerializeField] private float targetUpwardSpeed = 12.0f;

    // 낙하 중일 때 추가로 더 얹어줄 상향 속도. 음수 낙하를 상쇄해 깔끔하게 튕긴다.
    [SerializeField] private float extraUpwardBonusIfFalling = 3.0f;

    // 튕긴 뒤 허용할 최대 상향 속도. 과도한 가속 방지.
    [SerializeField] private float maxUpwardSpeed = 16.0f;

    // 점프대가 다시 작동하기까지 걸리는 시간.
    [SerializeField] private float cooldownSeconds = 0.15f;

    // 튕길 때 재생할 사운드 파일.
    [SerializeField] private AudioClip bounceSfx;

    // 사운드 볼륨.
    [SerializeField] private float sfxVolume = 0.8f;

    // 점프대가 작동 시 재생할 애니메이션.
    [SerializeField] private Animator springAnimator;

    // 튕길 때 발생시킬 이벤트 이름.
    [SerializeField] private string triggerName = "Bounce";

    // 최근에 튕겨 낸 대상의 Rigidbody2D를 기억해 놓고 재트리거가 곧바로 발생하는 것을 방지하기 위한 변수.
    private Rigidbody2D lastBouncedBody;

    // 마지막으로 튕긴 시간을 저장하여 쿨다운 계산.
    private float lastBounceTimeUnscaled = -9999.0f;

    private void Reset()
    {
        //  - 유니티 에디터에서 컴포넌트를 추가했을 때 자동으로 기본 세팅을 한다.
        //  - col: 현재 오브젝트에 부착되어 있는 Collider2D.
        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            // 트리거를 자동으로 설정.
            col.isTrigger = true;
        }
    }

    private void Awake()
    {
        // 현재 오브젝트에 콜라이더가 컴포넌트로 추가되어 있지 않으면 경고 메시지 출력.
        Collider2D col = GetComponent<Collider2D>();

        if (col == null)
        {
            Debug.LogWarning("SpringPad: Collider2D가 필요합니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트가 플레이어인지 체크해서 플레이어를 위로 튕겨준다.
        // 충돌한 오브젝트의 레이어가 플레이어 레이어인지 체크한다.
        int layerBit = 1 << other.gameObject.layer;

        bool isInMask = (triggerMask.value & layerBit) != 0 ? true : false;

        if (isInMask == false)
        {
            return;
        }

        Rigidbody2D otherBody = other.attachedRigidbody;

        if (otherBody == null)
        {
            return;
        }

        TryBounce(otherBody);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 플레이어인지 체크해서 플레이어를 위로 튕겨준다.
        // 충돌한 오브젝트의 레이어가 플레이어 레이어인지 체크한다.
        int layerBit = 1 << collision.gameObject.layer;

        bool isInMask = (triggerMask.value & layerBit) != 0 ? true : false;

        if (isInMask == false)
        {
            return;
        }

        Rigidbody2D body = collision.rigidbody;

        if (body == null)
        {
            return;
        }

        TryBounce(body);
    }


    /// <summary>
    /// 점프대에 닿은 대상의 Rigidbody2D에 튕김 효과를 적용한다.
    /// </summary>
    /// <param name="body">대상의 Rigidbody2D</param>
    private void TryBounce(Rigidbody2D body)
    {
        if (body == null)
        {
            return;
        }

        //  - nowUnscaled: 현재 시간.        
        float nowUnscaled = Time.unscaledTime;

        //  - onCooldown: 현재 쿨다운 상태인지 여부.
        bool onCooldown = false;

        // 점프대에 동일한 대상이 또 충돌했고, 아직 쿨다운이 다 되지 않았으면 점프대를 작동시키지 않도록 처리.
        if (lastBouncedBody == body)
        {
            float elapsed = nowUnscaled - lastBounceTimeUnscaled;

            if (elapsed < cooldownSeconds)
            {
                onCooldown = true;
            }
        }

        if (onCooldown == true)
        {
            return;
        }

        ApplyBounce(body);

        lastBouncedBody = body;

        lastBounceTimeUnscaled = nowUnscaled;

        PlayFeedback();
    }

    /// <summary>
    /// 실제 튕김 처리를 적용한다.
    /// </summary>
    /// <param name="body">튕길 대상의 Rigidbody2D</param>
    private void ApplyBounce(Rigidbody2D body)
    {
        // 대상의 현재 속도를 가져와서 저장한다.
        Vector2 v = body.linearVelocity;

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
        body.linearVelocity = v;
    }

    /// <summary>
    /// 사운드와 애니메이션 등을 실행.
    /// </summary>
    private void PlayFeedback()
    {
        // 사운드의 볼륨이 0~1 사이의 값으로 고정되도록 한다.
        float vol = Mathf.Clamp01(sfxVolume);

        if (bounceSfx != null)
        {
            // 사운드 파일이 있을 경우 사운드 재생.
            AudioSource.PlayClipAtPoint(bounceSfx, transform.position, vol);
        }

        // 점프대 오브젝트에 애니메이션이 있을 경우 애니메이션 재생.
        if (springAnimator != null)
        {
            if (string.IsNullOrEmpty(triggerName) == false)
            {
                springAnimator.ResetTrigger(triggerName);

                springAnimator.SetTrigger(triggerName);
            }
        }
    }
}
