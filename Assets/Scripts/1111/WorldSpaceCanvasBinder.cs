using UnityEngine;
using UnityEngine.UI;

// 파일: WorldSpaceCanvasBinder.cs
// 설치: 팝업 텍스트 프리팹의 루트(월드 스페이스 Canvas)에 부착.
[RequireComponent(typeof(Canvas))]
public class WorldSpaceCanvasBinder : MonoBehaviour
{
    public void BindCamera(Camera cam)
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                canvas.worldCamera = cam;
            }
        }
    }
}
