using System;
using UnityEngine;

public static class GameplayEvents
{
    // 함수를 저장할 수 있는 변수.
    public static event Action<Vector3, int> OnCoinCollected;
    public static event Action<Vector3> OnPlayerHit;

    public static void RaiseCoinCollected(Vector3 worldPosition, int value)
    {
        if(OnCoinCollected != null)
        {
            OnCoinCollected.Invoke(worldPosition, value);
        }
    }

    public static void RaisePlayerHit(Vector3 worldPosition)
    {
        Action<Vector3> handler = OnPlayerHit;
        if (handler != null)
        {
            handler.Invoke(worldPosition);
        }
    }
}
