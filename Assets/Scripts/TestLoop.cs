using UnityEngine;

public class TestLoop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ++ : ���� 1�� ���������ִ� ���� ������.
        // -- : ���� 1�� ���ҽ����ִ� ���� ������.
        // for
        //  �ʱ��;���ǽ�;������.
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("i = " + i);
            if(i == 5)
            {
                break;
            }
        }

        bool isAlive = true;
        int hp = 100;
        while(hp > 0)
        {
            Debug.Log("���� ����ִ�!!!!");
            hp = hp - 10;
            
            if(hp <= 50)
            {
                break;
            }
        }

        Debug.Log("���� �׾���~~~~ " + hp);

        // 1 -> 00000001
        // 2 -> 00000010
        // 3 -> 00000011
        // 4 -> 00000100
        // 5 -> 00000101
        // 6 -> 00000110
        // 7 -> 00000111
        // 8 -> 00001000
        // ��Ʈ����.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
