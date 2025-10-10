using System;
using UnityEngine;

// ����: GameplayEvents.cs
// ��ġ: ������Ʈ�� 1���� ����. � ������Ʈ���� ������ ����.
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
