using UnityEngine;

public class TestLoop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    // �Լ�(Method) -> ���α׷��� �����ϸ� ó���� ����Ǵ� �Լ�
    {
        PrintDebugLog();
        PrintDebugLog("Hello");
        string message = GetString();
        Debug.Log(message);
        string message2 = GetString("hi");
        Debug.Log(message2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintDebugLog()
    {
        Debug.Log("PrintDebug>og()");
    }

    void PrintDebugLog(string message)
    {
        Debug.Log(message);
    }

    string GetString()
    {
        string message = "Hello";
        return message;
    }

    string GetString(string message)
    {
        string result = "aaa" + message;
        return result;
    }
}
