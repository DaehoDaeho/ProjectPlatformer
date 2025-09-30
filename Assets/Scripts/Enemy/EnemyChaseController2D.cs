using UnityEngine;

public class EnemyChaseController2D : MonoBehaviour
{
    public EnemyPatrol2D patrol;
    public EnemySight2D sight;

    public float chaseSpeed = 5.0f;
    public float maxChaseDistance = 12.0f;
    public float loseSightTime = 1.0f;

    public Rigidbody2D body;

    private float notSeenTimer = 0.0f;
    private bool isChasing = false;

    private void FixedUpdate()
    {
        bool canSee = (sight != null) ? sight.CanSeePlayer() : false;

        if(isChasing == false)
        {
            if (canSee == true)
            {
                EnterChase();
            }
            return;
        }
        
        if(canSee == true)
        {
            notSeenTimer = 0.0f;
        }
        else
        {
            notSeenTimer += Time.fixedDeltaTime;
        }

        Vector2 enemyPos = transform.position;
        Vector2 playerPos = sight.GetPlayerPosition();
        float dx = playerPos.x - enemyPos.x;
        int dir = (dx >= 0.0f) ? 1 : -1;    // 1 = ¿À¸¥ÂÊ, -1 = ¿ÞÂÊ

        if(patrol != null)
        {
            patrol.Face(dir);
        }

        Vector2 v = body.linearVelocity;
        v.x = dir * chaseSpeed;
        body.linearVelocity = v;

        float dist = Mathf.Abs(dx);
        bool tooFar = (dist > maxChaseDistance);
        bool lostTooLong = (notSeenTimer >= loseSightTime);

        if(tooFar == true || lostTooLong == true)
        {
            EnterPatrol();
        }
    }

    void EnterChase()
    {
        isChasing = true;
        notSeenTimer = 0.0f;

        if(patrol != null)
        {
            patrol.enabled = false;
        }
    }

    void EnterPatrol()
    {
        isChasing = false;

        if (patrol != null)
        {
            patrol.enabled = true;
        }
    }
}
