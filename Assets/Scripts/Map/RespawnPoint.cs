using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public SpriteRenderer sr;
    public float flashLifetime = 0.5f;
    public Color flashColor = Color.green;

    private bool flash = false;
    private float timer = 0.0f;
    private Color originColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            PlayerRespawn pr = collision.GetComponent<PlayerRespawn>();
            if(pr != null)
            {
                pr.SetSpawnPosition(transform.position);
                originColor = sr.color;
                sr.color = flashColor;
                flash = true;
                timer = 0.0f;
            }
        }
    }

    private void Update()
    {
        if(flash == true)
        {
            timer += Time.deltaTime;
            if (timer >= flashLifetime)
            {
                sr.color = originColor;
                flash = false;
            }
        }
    }
}
