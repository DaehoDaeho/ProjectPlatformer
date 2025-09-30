using UnityEngine;

public class EnemyPatrol2D : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public int startDirection = 1;  // 1 : 오른쪽, -1 : 왼쪽.

    public Transform sensorPoint;
    public float wallCheckDistance = 0.25f;
    public float ledgeCheckDistance = 0.35f;
    public LayerMask groundMask;

    public bool useFlipX = true;
    public Rigidbody2D body;
    public SpriteRenderer sprite;

    private int direction = 1;

    private void Awake()
    {
        direction = (startDirection >= 0) ? 1 : -1; // 3항 연산.
        //if(startDirection >= 0)
        //{
        //    direction = 1;
        //}
        //else
        //{
        //    direction = -1;
        //}

        if(sensorPoint != null)
        {
            AlignSensorToDirection(direction);
        }

        ApplyVisualFacing();
    }

    private void FixedUpdate()
    {
        Vector2 v = body.linearVelocity;
        v.x = direction * moveSpeed;
        body.linearVelocity = v;

        Vector2 origin = sensorPoint.position;
        Vector2 dirForward = sensorPoint.right;
        Vector2 dirDown = (Vector2)(-sensorPoint.up);

        RaycastHit2D hitWall = Physics2D.Raycast(origin, dirForward, wallCheckDistance, groundMask);
        bool hasWall = (hitWall.collider != null);

        RaycastHit2D hitDown = Physics2D.Raycast(origin, dirDown, ledgeCheckDistance, groundMask);
        bool hasGround = (hitDown.collider != null);
        if(hasWall == true || hasGround == false)
        {
            TurnAround();
        }
    }

    void TurnAround()
    {
        direction = direction * -1;
        //direction *= -1;

        if(sensorPoint != null)
        {
            Vector3 p = sensorPoint.localPosition;
            p.x = p.x * -1.0f;
            sensorPoint.localPosition = p;

            Vector3 e = sensorPoint.localEulerAngles;
            e.y = (e.y + 180.0f) % 360.0f;
            sensorPoint.localEulerAngles = e;
        }

        ApplyVisualFacing();
    }

    void AlignSensorToDirection(int dir)
    {
        Vector3 p = sensorPoint.localPosition;
        p.x = Mathf.Abs(p.x) * (dir > 0 ? 1.0f : -1.0f);
        sensorPoint.localPosition = p;

        Vector3 e = sensorPoint.localEulerAngles;
        e.y = (dir > 0) ? 0.0f : 180.0f;
        sensorPoint.localEulerAngles = e;
    }

    /// <summary>
    /// 캐릭터의 방향을 전환하기 위한 함수.
    /// </summary>
    /// <param name="dir"></param>
    private void ApplyVisualFacing()
    {
        if(useFlipX == true)
        {
            bool facingLeft = (direction < 0);
            sprite.flipX = facingLeft;
        }
        else
        {
            Vector3 s = transform.localScale;

            // MathF.Abs : 절대값을 반환해주는 함수.
            s.x = Mathf.Abs(s.x) * (direction > 0 ? 1.0f : -1.0f);
            transform.localScale = s;
        }
    }

    public void Face(int dir)
    {
        if(useFlipX == true)
        {
            if(sprite != null)
            {
                bool facingLeft = (dir < 0);
                sprite.flipX = facingLeft;
            }
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (dir > 0 ? 1.0f : -1.0f);
            transform.localScale = s;
        }
        
        if(sensorPoint != null)
        {
            Vector3 p = sensorPoint.localPosition;
            p.x = Mathf.Abs(p.x) * (dir > 0 ? 1.0f : -1.0f);
            sensorPoint.localPosition = p;

            Vector3 e = sensorPoint.localEulerAngles;
            e.y = (dir > 0) ? 0.0f : 180.0f;
            sensorPoint.localEulerAngles = e;
        }
    }
}
