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
        // ���� ȭ��ǥ Ű�� A Ű�� ������ ��� 0���� ���� ���� ��ȯ,
        // ���� ȭ��ǥ Ű�� D Ű�� ������ ��� 0���� ū ���� ��ȯ.
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += new Vector2((moveInput * moveSpeed * Time.fixedDeltaTime), rb.linearVelocity.y);
    }
}
