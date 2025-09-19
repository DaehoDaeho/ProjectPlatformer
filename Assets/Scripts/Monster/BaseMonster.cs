using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform trans1;
    public Transform trans2;

    private float dir = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > trans1.position.x)
        {
            transform.position = trans1.position;
            dir = -1.0f;
        }
        else if (transform.position.x < trans2.position.x)
        {
            transform.position = trans2.position;
            dir = 1.0f;
        }    

        transform.position += new Vector3(dir * speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
