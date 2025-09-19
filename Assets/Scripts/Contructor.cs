using UnityEngine;

public class Contructor
{
    private int data;
    public int Data
    {
        get { return data; }
    }

    private float data2;
    public float Data2
    {
        set { data2 = value; }
        get { return data2; }
    }

    private string data3;
    public string Data3
    {
        set { data3 = value; }
        get { return data3; }
    }

    // 생성자.(Constructor)
    public Contructor()
    {
        data = 0;
        data2 = 0.0f;
        data3 = "Hello";
        Debug.Log("Contructor() 호출!!");
    }

    public Contructor(int value, float value2, string value3)
    {
        data = value;
        data2 = value2;
        data3 = value3;
        Debug.Log("Contructor(int value, float value2, string value3) 호출!!");
    }
}
