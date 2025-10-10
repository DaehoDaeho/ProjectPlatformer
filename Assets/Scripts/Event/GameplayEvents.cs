using System;
using UnityEngine;

public static class GameplayEvents
{
    // 함수를 저장할 수 있는 변수.
    public static event Action<Vector3, int> OnCoinCollected;

    public static void RaiseCoinCollected(Vector3 worldPosition, int value)
    {
        if(OnCoinCollected != null)
        {
            OnCoinCollected.Invoke(worldPosition, value);
        }
    }
}
