using System;
using UnityEngine;

// 파일: GameplayEvents.cs
// 설치: 프로젝트에 1개만 존재. 어떤 오브젝트에도 붙이지 않음.
public static class GameplayEvents
{
    public static event Action<Vector3, int> OnCoinCollected;
    public static event Action<Vector3> OnPlayerHit;

    public static void RaiseCoinCollected(Vector3 worldPosition, int value)
    {
        Action<Vector3, int> handler = OnCoinCollected;
        if (handler != null)
        {
            handler.Invoke(worldPosition, value);
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
