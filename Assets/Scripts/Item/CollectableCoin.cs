using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            GameScore gameScore = GameObject.FindAnyObjectByType<GameScore>();
            if(gameScore != null)
            {
                gameScore.AddScore(value);
            }

            Destroy(gameObject);
        }
    }
}
