using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Finish()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int a = 0;
        for(int i=0;i<50;i++)
        {
            if(i == 30)
            {
                continue;
            }
            a++;
        }

        int j = 0;
        while(j<50)
        {
            j++;
        }

        int k = 0;
        do
        {
            k++;
        } while (k < 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
