using UnityEngine;

// 파일: PauseInputListener.cs
// 설치: GameManager 또는 아무 빈 오브젝트에 부착.
// 역할: 키 입력(Escape)을 감지하여 일시정지 토글을 요청한다.
public class PauseInputListener : MonoBehaviour
{
    [Header("입력 키")]
    [SerializeField] private KeyCode toggleKey = KeyCode.Escape;

    private void Update()
    {
        // GetKeyDown 은 프레임에 한 번만 true 가 된다.
        if (Input.GetKeyDown(toggleKey) == true)
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.TogglePauseRequested();
            }
        }
    }
}
