using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpPower = 0.5f;
    public float groundCheckRadius = 0.12f;
    public Transform footPoint;
    public LayerMask groundMask;
    public Rigidbody2D rb;
    public float rayLength = 0.2f;

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
    }

    void Jump()
    {
        if (footPoint != null)
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

        if (isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0.0f);
            rb.AddForce(new Vector2(0.0f, jumpPower), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (footPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(footPoint.position, groundCheckRadius);
        }
    }
}
