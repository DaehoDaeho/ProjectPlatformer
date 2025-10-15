using UnityEngine;

// 파일: LevelLoadNextButton.cs
// 설치: 클리어 패널의 "다음 레벨" 버튼 오브젝트에 부착.
// 역할: 버튼 클릭 시 SceneTransitionController에게 다음 씬 로드를 요청합니다.
public class LevelLoadNextButton : MonoBehaviour
{
    [SerializeField] private SceneTransitionController transition;

    public void OnClickedLoadNext()
    {
        if (transition != null)
        {
            transition.LoadNextScene();
        }
        else
        {
            Debug.LogWarning("LevelLoadNextButton: transition이 연결되지 않았습니다.");
        }
    }
}
