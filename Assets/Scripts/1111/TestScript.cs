using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Finish()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[] score = new int[1000];    // 1차원 배열.

        int[] score2;
        score2 = new int[10000];

        int[] score3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // 다차원 배열.
        int[,] data = new int[4, 4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
