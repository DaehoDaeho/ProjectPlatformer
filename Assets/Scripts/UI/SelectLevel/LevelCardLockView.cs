using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 파일: LevelCardLockView.cs
// 설치: 카드 프리팹 루트 또는 자식 "LockOverlay" 오브젝트에 부착.
// 역할: 잠금 여부에 따라 자물쇠 오버레이, 문구, 투명도, 버튼 활성화를 제어.
public class LevelCardLockView : MonoBehaviour
{
    [Header("UI 참조")]
    [SerializeField] private CanvasGroup lockOverlay;
    [SerializeField] private TMP_Text lockText;
    [SerializeField] private Button playButton;

    [Header("회색 처리(잠금 시)")]
    [SerializeField] private Graphic[] dimTargets;
    [SerializeField] private float dimAlpha = 0.5f;

    private float[] originalAlpha;

    private void Awake()
    {
        if (dimTargets != null)
        {
            originalAlpha = new float[dimTargets.Length];

            for (int i = 0; i < dimTargets.Length; i = i + 1)
            {
                if (dimTargets[i] != null)
                {
                    originalAlpha[i] = dimTargets[i].color.a;
                }
            }
        }
    }

    public void Apply(bool locked, int requiredStars, int currentTotalStars)
    {
        // 오버레이 표시.
        if (lockOverlay != null)
        {
            lockOverlay.alpha = locked == true ? 1.0f : 0.0f;
            lockOverlay.blocksRaycasts = locked == true ? true : false;
            lockOverlay.interactable = false;
        }

        // 문구 업데이트.
        if (lockText != null)
        {
            if (locked == true)
            {
                lockText.text = "Locked: Required Stars " + requiredStars.ToString() + "\nCurrent Stars " + currentTotalStars.ToString();
            }
            else
            {
                lockText.text = "";
            }
        }

        // 플레이 버튼 활성화.
        if (playButton != null)
        {
            playButton.interactable = (locked == false);
        }

        // 회색 처리.
        if (dimTargets != null)
        {
            for (int i = 0; i < dimTargets.Length; i = i + 1)
            {
                Graphic g = dimTargets[i];

                if (g == null)
                {
                    continue;
                }

                Color c = g.color;

                if (locked == true)
                {
                    c.a = dimAlpha;
                }
                else
                {
                    c.a = originalAlpha != null ? originalAlpha[i] : 1.0f;
                }

                g.color = c;
            }
        }
    }
}
