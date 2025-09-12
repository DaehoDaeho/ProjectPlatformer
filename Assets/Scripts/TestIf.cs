using UnityEngine;

public class TestIf : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int a = 10;
        int b = 50;

        if((a > 0 && a < 20) || b > 100)
        {
            Debug.Log("조건 성립!!!!!!");
        }
        else
        {
            Debug.Log("조건 성립 XXXXXXX");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
