using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public string nextScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 패이드 아웃이 종료됐을 때 호출할 이벤트 함수 등록.
        fadeScreen.OnFinishedFadeOut += MoveToNextScene;
    }

    private void OnDisable()
    {
        // 등록한 이벤트 함수 해제.
        fadeScreen.OnFinishedFadeOut -= MoveToNextScene;
    }

    void MoveToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
