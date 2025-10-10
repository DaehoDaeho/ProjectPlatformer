using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float lifeTime = 0.8f;

    [SerializeField]
    private float riseSpeed = 1.0f;

    [SerializeField]
    private float fadeOutStart = 0.3f;

    private float alive = 0.0f;
    private Color initialColor;

    private void Awake()
    {
        if(text != null)
        {
            initialColor = text.color;
        }
    }

    public void SetText(string content)
    {
        if(text != null)
        {
            text.text = content;
        }
    }

    // Update is called once per frame
    void Update()
    {
        alive += Time.deltaTime;

        Vector3 p = transform.position;
        p.y = p.y + riseSpeed * Time.deltaTime;
        transform.position = p;

        if(text != null)
        {
            float t = Mathf.Clamp01((alive - fadeOutStart) / Mathf.Max(0.0001f, lifeTime - fadeOutStart));
            float alpha = 1.0f - t;
            Color c = initialColor;
            c.a = alpha;
            text.color = c;
        }

        if(alive >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
