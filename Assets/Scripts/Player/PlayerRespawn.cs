using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnDelay = 0.0f;
    public Rigidbody2D rigidBody;

    private Vector3 currentSpawnPosition;
    private Vector3 defaultSpawnPosition;
    private bool hasSpawnPosition = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpawnPosition = transform.position;
        defaultSpawnPosition = transform.position;
        hasSpawnPosition = false;
    }

    public void SetSpawnPosition(Vector3 worldPosition)
    {
        currentSpawnPosition = worldPosition;
        hasSpawnPosition = true;
        Debug.Log("위치가 저장 되었습니다!!!!");
        Debug.Log("position x = " + currentSpawnPosition.x + ", y = " + currentSpawnPosition.y + ", z = " + currentSpawnPosition.z);
    }

    public void Respawn()
    {
        if(hasSpawnPosition == true)
        {
            rigidBody.linearVelocity = Vector2.zero;
            transform.position = currentSpawnPosition;
        }
        else
        {
            rigidBody.linearVelocity = Vector2.zero;
            transform.position = defaultSpawnPosition;
        }
    }
}
