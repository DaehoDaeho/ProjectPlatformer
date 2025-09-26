using UnityEngine;

public class PlayerPlatformRide : MonoBehaviour
{
    public Rigidbody2D rb;
    private MovingPlatform movingPlatform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingPlatform = collision.collider.GetComponent<MovingPlatform>();
        if(movingPlatform != null)
        {
            transform.SetParent(movingPlatform.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MovingPlatform platform = collision.collider.GetComponent<MovingPlatform>();
        if(platform != null)
        {
            if(platform == movingPlatform)
            {
                movingPlatform = null;
                transform.SetParent(null);
            }
        }
    }

    private void FixedUpdate()
    {
        if(movingPlatform != null)
        {
            Vector2 v = rb.linearVelocity;
            v = v + movingPlatform.CurrentVelocity;
            rb.linearVelocity = v;
        }
    }
}
