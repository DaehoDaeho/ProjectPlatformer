using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;

    private float moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        // 좌측 화살표 키나 A 키를 눌렀을 경우 0보다 작은 값이 반환,
        // 우측 화살표 키나 D 키를 눌렀을 경우 0보다 큰 값이 반환.
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += new Vector2((moveInput * moveSpeed * Time.fixedDeltaTime), rb.linearVelocity.y);
    }
}
