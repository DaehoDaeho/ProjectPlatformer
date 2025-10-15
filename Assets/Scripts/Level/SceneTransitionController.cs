using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// 파일: SceneTransitionController.cs
// 설치: GameManager 오브젝트에 부착.
// 역할: 페이드 아웃 → 씬 로드 → 페이드 인 순서의 전환을 한 곳에서 관리합니다.
public class SceneTransitionController : MonoBehaviour
{
    [SerializeField] private FadeCanvasController fadeCanvas;

    private void Awake()
    {
        if (fadeCanvas == null)
        {
            Debug.LogWarning("SceneTransitionController: fadeCanvas가 연결되지 않았습니다.");
        }
    }

    // 현재 Build Index의 다음 씬을 로드합니다.
    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        int next = index + 1;
        LoadSceneByIndex(next);
    }

    // 씬 이름으로 로드합니다.
    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName) == true)
        {
            Debug.LogWarning("LoadSceneByName: sceneName이 비어 있습니다.");
            return;
        }
        StartCoroutine(FadeAndLoadByName(sceneName));
    }

    // 씬 인덱스로 로드합니다.
    public void LoadSceneByIndex(int buildIndex)
    {
        if (buildIndex < 0)
        {
            Debug.LogWarning("LoadSceneByIndex: 잘못된 인덱스입니다.");
            return;
        }
        StartCoroutine(FadeAndLoadByIndex(buildIndex));
    }

    private IEnumerator FadeAndLoadByName(string sceneName)
    {
        // 1) 페이드 아웃
        if (fadeCanvas != null)
        {
            float d = fadeCanvas.GetDefaultDuration();
            fadeCanvas.StartFade(1.0f, d);
            yield return new WaitForSecondsRealtime(d);
        }

        // 2) 타임스케일 복구(클리어로 0인 경우 대비)
        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }

        // 3) 씬 로드
        SceneManager.LoadScene(sceneName);

        // 4) 새 씬에서 페이드 인
        if (fadeCanvas != null)
        {
            yield return null; // 한 프레임 대기(씬 교체 안정화)
            float d = fadeCanvas.GetDefaultDuration();
            fadeCanvas.StartFade(0.0f, d);
        }
    }

    private IEnumerator FadeAndLoadByIndex(int buildIndex)
    {
        if (fadeCanvas != null)
        {
            float d = fadeCanvas.GetDefaultDuration();
            fadeCanvas.StartFade(1.0f, d);
            yield return new WaitForSecondsRealtime(d);
        }

        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }

        SceneManager.LoadScene(buildIndex);

        if (fadeCanvas != null)
        {
            yield return null;
            float d = fadeCanvas.GetDefaultDuration();
            fadeCanvas.StartFade(0.0f, d);
        }
    }
}
