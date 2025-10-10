using System;
using UnityEngine;

public static class GameplayEvents
{
    // �Լ��� ������ �� �ִ� ����.
    public static event Action<Vector3, int> OnCoinCollected;

    public static void RaiseCoinCollected(Vector3 worldPosition, int value)
    {
        if(OnCoinCollected != null)
        {
            OnCoinCollected.Invoke(worldPosition, value);
        }
    }
}
