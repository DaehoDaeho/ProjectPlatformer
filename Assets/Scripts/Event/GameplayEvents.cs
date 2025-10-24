using System;
using Unity.VisualScripting;
using UnityEngine;

public static class GameplayEvents
{
    // 함수를 저장할 수 있는 변수.
    public static event Action<Vector3, int> OnCoinCollected;
    public static event Action<Vector3> OnPlayerHit;

    //=======================================================================
    public static event Action<Vector3> OnEnemyDefeated;
    public static event Action<float> OnHardLanding;
    //=======================================================================

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

    public static void RaiseEnemyDefeated(Vector3 hitPos)
    {
        Action<Vector3> handler = OnEnemyDefeated;
        if(handler != null)
        {
            handler.Invoke(hitPos);
        }
    }

    public static void RaiseHardLanding(float impact)
    {
        Action<float> handler = OnHardLanding;
        if (handler != null)
        {
            handler.Invoke(impact);
        }
    }
}
