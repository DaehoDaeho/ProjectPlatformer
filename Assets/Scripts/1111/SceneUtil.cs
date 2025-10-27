using UnityEngine;
using UnityEngine.SceneManagement;

// 파일: SceneUtil.cs
// 설치: 아무 곳에도 부착하지 않는 정적 클래스.
// 역할: 씬 전환 편의 함수 제공. 기존 SceneTransitionController 가 있을 경우 우선 사용해도 된다.
public static class SceneUtil
{
    public static void ReloadCurrentScene()
    {
        Scene s = SceneManager.GetActiveScene();
        if (s.IsValid() == true)
        {
            // 일시정지는 해제하고 로드하는 편이 안전하다.
            if (GameStateManager.Instance != null)
            {
                if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Paused)
                {
                    GameStateManager.Instance.ResumeGame();
                }
            }

            SceneManager.LoadScene(s.name);
        }
    }

    public static void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName) == true)
        {
            return;
        }

        // 동일하게 일시정지 상태를 해제하고 이동.
        if (GameStateManager.Instance != null)
        {
            if (GameStateManager.Instance.GetState() == GameStateManager.GameState.Paused)
            {
                GameStateManager.Instance.ResumeGame();
            }
        }

        SceneManager.LoadScene(sceneName);
    }
}
