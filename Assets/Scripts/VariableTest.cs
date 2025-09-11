using UnityEngine;

public class VariableTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string a = "aaa";
        string b = "bbb";
        // 형 변환.
        Debug.Log("a = " + a);
        Debug.Log("b = " + b);

        string c = a + b;
        Debug.Log("c = " + c);
        int hp = 100;
        string message = "체력 : " + hp;
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
