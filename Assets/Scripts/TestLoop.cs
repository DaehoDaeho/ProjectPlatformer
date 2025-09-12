using UnityEngine;

public class TestLoop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ++ : 값을 1씩 증가시켜주는 증감 연산자.
        // -- : 값을 1씩 감소시켜주는 증감 연산자.
        // for
        //  초기식;조건식;증감식.
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
            Debug.Log("나는 살아있다!!!!");
            hp = hp - 10;
            
            if(hp <= 50)
            {
                break;
            }
        }

        Debug.Log("나는 죽었다~~~~ " + hp);

        // 1 -> 00000001
        // 2 -> 00000010
        // 3 -> 00000011
        // 4 -> 00000100
        // 5 -> 00000101
        // 6 -> 00000110
        // 7 -> 00000111
        // 8 -> 00001000
        // 비트연산.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
