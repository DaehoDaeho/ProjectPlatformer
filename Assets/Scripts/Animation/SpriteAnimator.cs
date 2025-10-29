using UnityEngine;
using System;

/// <summary>
/// SpriteAnimator
/// 기능: 프레임 교체 방식으로 스프라이트 애니메이션을 재생하는 컴포넌트.
/// 사용법:
///  - 인스펙터에서 Clip 항목들을 등록한다(이름, 프레임 배열, FPS, 루프 여부).
///  - 다른 스크립트에서 Play("Idle") 처럼 이름으로 재생을 지시한다.
/// 주요 메서드:
///  - Play(string clipName): 해당 이름의 클립 재생(전환).
///  - Stop(): 재생 정지(첫 프레임 표시 유지).
/// 설계:
///  - Update()에서 deltaTime 누적 -> 프레임 경계마다 다음 스프라이트로 전환.
///  - 루프면 끝에서 처음으로, 아니면 마지막 프레임 고정.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [Serializable]
    public class Clip
    {
        [Tooltip("클립 이름. 다른 스크립트에서 Play(\"이름\")으로 호출합니다.")]
        public string name;

        [Tooltip("재생할 스프라이트 프레임들(순서 중요).")]
        public Sprite[] frames;

        [Tooltip("초당 프레임 수(FPS). 값이 0 이하이면 1로 보정됩니다.")]
        public float framesPerSecond = 8.0f;

        [Tooltip("마지막 프레임 뒤에 처음으로 돌아갈지 여부.")]
        public bool loop = true;
    }

    [Header("클립 등록")]
    [Tooltip("Idle, Move, Jump 등 필요한 클립들을 등록하세요.")]
    [SerializeField] private Clip[] clips;

    [Header("전환 옵션")]
    [Tooltip("같은 클립을 다시 Play할 때 프레임 인덱스를 0으로 리셋할지 여부.")]
    [SerializeField] private bool resetFrameOnSamePlay = false;

    // 재생에 사용할 SpriteRenderer 캐시.
    private SpriteRenderer spriteRenderer;

    // 현재 재생 중인 클립 참조.
    private Clip currentClip;

    // 현재 프레임 인덱스(0 기반)
    private int currentFrameIndex = 0;

    // 다음 프레임으로 넘어갈 때까지 남은 시간(초)
    private float frameTimer = 0.0f;

    private void Awake()
    {
        // 캐시할 스프라이트 렌더러.
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError("ClassicSpriteAnimator: SpriteRenderer가 필요합니다.");
            return;
        }

        spriteRenderer = sr;

        // 시작 시 아무 클립도 재생하지 않으며,
        // 외부에서 Play를 호출하면 그때부터 재생.
    }

    private void Update()
    {
        // 함수 설명:
        //  - 현재 클립이 있으면 deltaTime을 누적하고 프레임 전환 타이밍에 맞춰 스프라이트를 바꾼다.
        if (currentClip == null)
        {
            return;
        }

        // 현재 클립 FPS(0 이하면 1로 보정).        
        float fps = currentClip.framesPerSecond;

        if (fps <= 0.0f)
        {
            fps = 1.0f;
        }

        // 프레임 하나가 유지될 시간(초).
        float frameDuration = 1.0f / fps;

        // 이번 프레임 경과 시간.
        float dt = Time.deltaTime;

        frameTimer = frameTimer + dt;

        // 프레임 전환이 필요한지 검사.
        if (frameTimer >= frameDuration)
        {
            // 남은 시간을 차감(긴 dt에서도 누락 없이 정확히 전환)
            frameTimer = frameTimer - frameDuration;

            // 다음 프레임으로.
            currentFrameIndex = currentFrameIndex + 1;

            // 배열 경계 처리.
            if (currentFrameIndex >= GetFrameCount(currentClip))
            {
                if (currentClip.loop == true)
                {
                    currentFrameIndex = 0;
                }
                else
                {
                    currentFrameIndex = GetFrameCount(currentClip) - 1;
                }
            }

            // 실제 스프라이트 교체.
            ApplyFrame();
        }
    }

    /// <summary>
    /// 지정한 이름의 클립을 재생.
    /// 이미 같은 클립을 재생 중일 때의 처리:
    ///  - resetFrameOnSamePlay == true 이면 프레임 0으로 리셋.
    ///  - 아니면 현재 프레임을 유지하고 그대로 진행.
    /// </summary>
    public void Play(string clipName)
    {
        // 찾은 클립 참조.
        Clip target = FindClip(clipName);

        if (target == null)
        {
            Debug.LogWarning("ClassicSpriteAnimator: 클립을 찾을 수 없음: " + clipName);
            return;
        }

        if (currentClip == target)
        {
            if (resetFrameOnSamePlay == true)
            {
                currentFrameIndex = 0;
                frameTimer = 0.0f;
                ApplyFrame();
            }

            return;
        }

        currentClip = target;
        currentFrameIndex = 0;
        frameTimer = 0.0f;
        ApplyFrame();
    }

    /// <summary>
    /// 현재 재생을 정지하고 첫 프레임을 표시한 채로 멈춘다.
    /// </summary>
    public void Stop()
    {
        if (currentClip == null)
        {
            return;
        }

        currentFrameIndex = 0;
        frameTimer = 0.0f;
        ApplyFrame();
        currentClip = null;
    }

    /// <summary>
    /// 현재 프레임의 스프라이트를 SpriteRenderer에 반영.
    /// </summary>
    private void ApplyFrame()
    {
        if (currentClip == null)
        {
            return;
        }

        // 현재 클립의 프레임 배열.        
        Sprite[] arr = currentClip.frames;

        // count: 프레임 개수.
        int count = GetFrameCount(currentClip);

        if (count <= 0)
        {
            return;
        }

        if (currentFrameIndex < 0)
        {
            currentFrameIndex = 0;
        }

        if (currentFrameIndex >= count)
        {
            currentFrameIndex = count - 1;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = arr[currentFrameIndex];
        }
    }

    /// <summary>
    /// 이름으로 클립을 찾아 반환.
    /// </summary>
    private Clip FindClip(string clipName)
    {
        if (clips == null)
        {
            return null;
        }

        for (int i = 0; i < clips.Length; i = i + 1)
        {
            Clip c = clips[i];

            if (c != null)
            {
                if (string.Equals(c.name, clipName, StringComparison.Ordinal) == true)
                {
                    return c;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 클립의 프레임 개수를 안전하게 계산.
    /// </summary>
    private int GetFrameCount(Clip c)
    {
        if (c == null)
        {
            return 0;
        }

        if (c.frames == null)
        {
            return 0;
        }

        return c.frames.Length;
    }
}
