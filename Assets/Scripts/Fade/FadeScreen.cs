using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;

    public event Action OnFinishedFadeOut;

    public void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;  // 패이드 이미지의 색상 정보를 가져온다.
        color.a = 0.0f;
        fadeImage.color = color;

        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoFadeOut()
    {
        // 함수 내의 코드를 시간 단위로 분할해서 처리할 때 사용하는 함수.
        // 비동기 처리를 하기 위해 사용하는 함수.
        while (true)
        {
            Color color = fadeImage.color;  // 패이드 이미지의 색상 정보를 가져온다.
            color.a += Time.deltaTime;

            if(color.a > 1.0f)
            {
                color.a = 1.0f;
            }

            fadeImage.color = color;

            if(fadeImage.color.a == 1.0f)
            {
                break;
            }

            yield return null;
        }

        if(OnFinishedFadeOut != null)
        {
            OnFinishedFadeOut.Invoke();
        }
    }
}
