using UnityEngine;

public class EnemySight2D : MonoBehaviour
{
    public Transform player;
    public Transform sensorPoint;
    public LayerMask groundMask;
    public float detectRadius = 6.0f;
    public float fovAngle = 90.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GameObject objectPlayer = GameObject.FindGameObjectWithTag("Player");
        //if(objectPlayer != null)
        //{
        //    player = objectPlayer.transform;
        //}

        //GameObject objectPlayer = GameObject.Find("Player");
        //if(objectPlayer != null)
        //{
        //    player = objectPlayer.transform;
        //}

        PlayerMove playerMove = GameObject.FindAnyObjectByType<PlayerMove>();
        if(playerMove != null)
        {
            player = playerMove.transform;
        }
    }

    public bool CanSeePlayer()
    {
        if(player == null || sensorPoint == null)
        {
            return false;
        }

        if(player != null)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if(ph != null)
            {
                if(ph.IsPlayerAlive() == false)
                {
                    return false;
                }
            }
        }

        Vector2 origin = sensorPoint.position;
        Vector2 toPlayer = (Vector2)(player.position - sensorPoint.position);
        float distance = toPlayer.magnitude;    // º¤ÅÍÀÇ Å©±â.

        if(distance > detectRadius)
        {
            return false;
        }

        Vector2 forward = sensorPoint.right;
        float angle = Vector2.Angle(forward, toPlayer);
        if(angle > (fovAngle * 0.5f))
        {
            return false;
        }

        RaycastHit2D block = Physics2D.Raycast(origin, toPlayer.normalized, distance, groundMask);
        bool blocked = (block.collider != null);
        if(blocked == true)
        {
            return false;
        }

        return true;
    }

    public Vector2 GetPlayerPosition()
    {
        if(player != null)
        {
            return player.position;
        }

        return transform.position;
    }
}
