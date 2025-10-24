using UnityEngine;

// 파일: ShakeRelayOnEvents.cs
// 설치: GameManager 또는 Camera에 부착.
// 역할: 게임 이벤트를 수신하여 CameraShake2D에 셰이크를 요청.
public class ShakeRelayOnEvents : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private CameraShake2D cameraShake;

    [Header("강도 프리셋")]
    [SerializeField] private float hitIntensity = 0.25f;
    [SerializeField] private float hitDuration = 0.18f;
    [SerializeField] private float hitDamping = 9.0f;

    [SerializeField] private float killIntensity = 0.35f;
    [SerializeField] private float killDuration = 0.22f;
    [SerializeField] private float killDamping = 10.0f;

    [SerializeField] private float landIntensity = 0.18f;
    [SerializeField] private float landDuration = 0.12f;
    [SerializeField] private float landDamping = 8.0f;

    private void Awake()
    {
        if (cameraShake == null)
        {
            cameraShake = FindAnyObjectByType<CameraShake2D>();
        }
    }

    private void OnEnable()
    {
        GameplayEvents.OnPlayerHit += HandlePlayerHit;
        GameplayEvents.OnEnemyDefeated += HandleEnemyDefeated;
        GameplayEvents.OnHardLanding += HandleHardLanding;
    }

    private void OnDisable()
    {
        GameplayEvents.OnPlayerHit -= HandlePlayerHit;
        GameplayEvents.OnEnemyDefeated -= HandleEnemyDefeated;
        GameplayEvents.OnHardLanding -= HandleHardLanding;
    }

    private void HandlePlayerHit(Vector3 hitPos)
    {
        if (cameraShake != null)
        {
            cameraShake.ShakeOnce(hitIntensity, hitDuration, hitDamping);
        }
    }

    private void HandleEnemyDefeated(Vector3 enemyPos)
    {
        if (cameraShake != null)
        {
            cameraShake.ShakeOnce(killIntensity, killDuration, killDamping);
        }
    }

    private void HandleHardLanding(float impact)
    {
        if (cameraShake != null)
        {
            cameraShake.ShakeOnce(landIntensity, landDuration, landDamping);
        }
    }
}
