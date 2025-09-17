using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public class TestLoop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()    // 함수(Method) -> 프로그램을 실행하면 처음에 실행되는 함수
    {
        List<int> item = new List<int>();
        item.Add(1);    // 데이터 추가.
        item.Add(2);
        item.Add(3);
        item.Add(4);
        item.Add(5);

        int index = item.IndexOf(10);
        bool ok = item.Contains(5);
        Debug.Log(ok);

        item.Insert(1, 6);

        for (int i = 0; i < item.Count; ++i)
        {
            Debug.Log(item[i]);
        }

        item.Remove(3);
        item.RemoveAt(0);

        for (int i = 0; i < item.Count; ++i)
        {
            Debug.Log(item[i]);
        }

        item.Clear();
        Debug.Log("count = " + item.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddNubmers(int a, int b)
    {

    }
}
