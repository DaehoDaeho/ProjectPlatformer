using UnityEngine;

// ����: SpringPad.cs
// ��ġ ��ġ: '������ ������' ������Ʈ�� ����.
// ��� ���: �÷��̾ �����뿡 ������ �������� ƨ�� �÷��ش�.
// ���� �ǵ�:
//  - �������� ü��: ���� �ӵ��� ���� ����Ἥ ��� ���� ����.
//  - ������ ����: ���̾� ����ũ�� ��� ����, ��ٿ����� ���� Ʈ���� ����.
//  - Ʃ�� ����Ʈ: ���� ��ǥ �ӵ�, ���� ����, �ִ� ���� �ӵ�, ��ٿ�, ����.
[RequireComponent(typeof(Collider2D))]
public class SpringPad : MonoBehaviour
{
    [Header("�÷��̾� ���̾� ����")]
    [Tooltip("�������� �����ؾ� �ϴ� ���(���� Player ���̾�)")]
    [SerializeField] private LayerMask triggerMask;

    [Header("���� Ʃ��")]
    [Tooltip("ƨ�� �� ��ǥ�� ���� ���� �ӵ�(m/s). ���� �ӵ��� �̺��� ������ �÷��ݴϴ�.")]
    [SerializeField] private float targetUpwardSpeed = 12.0f;

    [Tooltip("���� ���� �� �߰��� �� ����� ���� �ӵ�(m/s). ���� ���ϸ� ����� �����ϰ� Ƣ��ϴ�.")]
    [SerializeField] private float extraUpwardBonusIfFalling = 3.0f;

    [Tooltip("ƨ�� �� ����� �ִ� ���� �ӵ� ĸ. ������ ���� ����.")]
    [SerializeField] private float maxUpwardSpeed = 16.0f;

    [Header("��Ʈ���� ����")]
    [Tooltip("���� ��ü�� �ٽ� Ʈ���ŵǱ���� ��� �ð�(��)")]
    [SerializeField] private float cooldownSeconds = 0.15f;

    [Header("ȿ����")]
    [Tooltip("ƨ�� �� ����� ���� Ŭ��")]
    [SerializeField] private AudioClip bounceSfx;

    [Tooltip("ȿ���� ����(0~1)")]
    [SerializeField] private float sfxVolume = 0.8f;

    [Header("�ִϸ��̼�(����)")]
    [Tooltip("����/���� �ִϸ��̼��� ���� Animator. ���� ����.")]
    [SerializeField] private Animator springAnimator;

    [Tooltip("ƨ�� �� ȣ���� Ʈ���� �̸�. ���� ����.")]
    [SerializeField] private string triggerName = "Bounce";


    // === [���� ����] ��Ÿ�� ����� ===

    // �ֱٿ� ƨ�ܳ� Rigidbody�� ����Ͽ� ª�� �ð� ��Ʈ���Ÿ� ���� ���� Ű
    private Rigidbody2D lastBouncedBody;

    // ������ ƨ�� �ð��� unscaled �ð����� �����Ͽ�, Ÿ�ӽ����ϰ� �����ϰ� ��ٿ� ���
    private float lastBounceTimeUnscaled = -9999.0f;


    // === Unity �����ֱ� ===

    private void Reset()
    {
        // �Լ� ����:
        //  - �����Ϳ��� ������Ʈ�� �߰����� �� �⺻ ������ ����ϴ�.
        // ���� ����:
        //  - col: ���� ������Ʈ�� Collider2D ����.
        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            // ������� �밳 Ʈ���ŷ� ����մϴ�.
            col.isTrigger = true;
        }
    }

    private void Awake()
    {
        // �Լ� ����:
        //  - �ݶ��̴� ����� ���̾� ������ �⺻ ��ȿ���� �����մϴ�.
        // ���� ����:
        //  - col: ���� ������Ʈ�� Collider2D ����.
        Collider2D col = GetComponent<Collider2D>();

        if (col == null)
        {
            Debug.LogWarning("SpringPad: Collider2D�� �ʿ��մϴ�.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �Լ� ����:
        //  - Ʈ���� ������� ������ ����� �˻��ϰ�, �÷��̾� ���̾�� ƨ��ϴ�.
        // ���� ����:
        //  - otherBody: ������ ����� Rigidbody2D.
        //  - layerBit: ����� ���̾ ��Ʈ����ũ�� ��ȯ�� ��.
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
        // �Լ� ����:
        //  - Ʈ���� ��� �浹 �ݶ��̴��� ����ϴ� ��츦 �����մϴ�.
        // ���� ����:
        //  - layerBit: �浹�� ����� ���̾� ��Ʈ.
        //  - body: �浹 ����� Rigidbody2D.
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


    // === �ٽ� ���� ===

    /// <summary>
    /// ��� Rigidbody2D�� ƨ�� ȿ���� �����մϴ�.
    /// ����/���ۿ�:
    ///  - ��ٿ� ���̸� �����մϴ�.
    ///  - ����� ���� �ӵ��� ��ǥġ �̻����� �����մϴ�.
    ///  - ȿ������ �ִϸ��̼� Ʈ���Ÿ� ȣ���մϴ�.
    /// �Է�:
    ///  - body: ƨ�� ����� Rigidbody2D. null�̸� ����.
    /// ��ȯ:
    ///  - ����.
    /// ����:
    ///  - ����(���� ���� ����).
    /// ����:
    ///  - O(1). ������ ����� ���Ը� ����.
    /// </summary>
    private void TryBounce(Rigidbody2D body)
    {
        if (body == null)
        {
            return;
        }

        // ���� ����:
        //  - nowUnscaled: ���� unscaled �ð�.
        //  - onCooldown: ��ٿ� �������� ����.
        float nowUnscaled = Time.unscaledTime;

        bool onCooldown = false;

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
    /// ���� ���� �ӵ� ������ �����մϴ�.
    /// ����:
    ///  - ���� ���� �ӵ�(vy)�� ��ǥġ���� ������, vy�� �÷��ݴϴ�.
    ///  - ���� ��(vy < 0)�� ��� �߰� ���ʽ��� ����ϴ�.
    ///  - �ִ� ���� �ӵ� ĸ���� ���� ��ġ.
    /// �Է�:
    ///  - body: ��� Rigidbody2D.
    /// ��ȯ:
    ///  - ����.
    /// </summary>
    private void ApplyBounce(Rigidbody2D body)
    {
        // ���� ����:
        //  - v: ���� �ӵ� ���͸� ������ ��.
        //  - vy: ���� ���� �ӵ� ��.
        Vector2 v = body.linearVelocity;

        float vy = v.y;

        // ���� ���̸� �߰� �������� �� ���ϰ� ����ø�.
        float desired = targetUpwardSpeed;

        if (vy < 0.0f)
        {
            desired = desired + extraUpwardBonusIfFalling;
        }

        // ��ǥġ���� ������ ����ø���, �̹� ������ �״�� �Ӵϴ�.
        if (vy < desired)
        {
            vy = desired;
        }

        // ������ ���� �ӵ� ����.
        if (vy > maxUpwardSpeed)
        {
            vy = maxUpwardSpeed;
        }

        v.y = vy;

        body.linearVelocity = v;
    }

    /// <summary>
    /// ����� �ִϸ��̼� Ʈ���� �� �ǵ���� �����մϴ�.
    /// �Է�/��ȯ:
    ///  - ����.
    /// ���ۿ�:
    ///  - ����� ���. Animator Ʈ���� ȣ��.
    /// </summary>
    private void PlayFeedback()
    {
        // ���� ����:
        //  - vol: 0~1�� Ŭ������ ���� ��.
        float vol = Mathf.Clamp01(sfxVolume);

        if (bounceSfx != null)
        {
            // UI SFXó�� ������ ��� ������ ���� ���.
            AudioSource.PlayClipAtPoint(bounceSfx, transform.position, vol);
        }

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
