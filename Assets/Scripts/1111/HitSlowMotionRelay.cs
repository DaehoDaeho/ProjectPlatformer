using UnityEngine;

// 파일: HitSlowMotionRelay.cs
// 설치: GameManager 또는 Camera 에 부착.
// 역할: 플레이어 피격 이벤트를 수신하여 짧은 슬로모션을 실행한다.
// 주의: 일시정지 중에는 슬로모션을 실행하지 않는다.
public class HitSlowMotionRelay : MonoBehaviour
{
    [Header("슬로모션 설정")]
    [SerializeField] private float hitTimeScale = 0.2f;
    [SerializeField] private float durationSeconds = 0.15f;

    private void OnEnable()
    {
        GameplayEvents.OnPlayerHit += HandlePlayerHit;
    }

    private void OnDisable()
    {
        GameplayEvents.OnPlayerHit -= HandlePlayerHit;
    }

    private void HandlePlayerHit(Vector3 hitPos)
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        // 현재 상태가 Paused 이면 슬로모션을 실행하지 않는다.
        if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Paused)
        {
            return;
        }

        GameStateManager.Instance.StartSlowMotion(hitTimeScale, durationSeconds);
    }
}
