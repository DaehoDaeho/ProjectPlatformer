using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] wayPoints;
    public float moveSpeed = 2.0f;
    public float arriveThreshold = 0.05f;
    public Rigidbody2D rb;

    private int currentIndex = 0;

    public Vector2 CurrentDelta { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private void Start()
    {
        transform.position = wayPoints[0].position;
    }

    private void FixedUpdate()
    {
        CurrentDelta = Vector2.zero;
        CurrentVelocity = Vector2.zero;

        Vector2 before = rb.position;

        Transform target = wayPoints[currentIndex];
        Vector2 current = rb.position;
        Vector2 targetPos = target.position;
        Vector2 toTarget = targetPos - current;
        //float distance = toTarget.magnitude;
        float distance = Vector2.Distance(targetPos, current);
        if(distance <= arriveThreshold)
        {
            ++currentIndex;
            if(currentIndex >= wayPoints.Length)
            {
                currentIndex = 0;
            }
        }

        Vector2 dir = toTarget.normalized;
        Vector2 next = current + dir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(next);

        CurrentDelta = next - before;
        //if (Time.fixedDeltaTime > 0.0f)
        {
            CurrentVelocity = CurrentDelta / Time.fixedDeltaTime;
        }
    }
}
