using UnityEngine;
using UnityEngine.UI;

// ����: WorldSpaceCanvasBinder.cs
// ��ġ: �˾� �ؽ�Ʈ �������� ��Ʈ(���� �����̽� Canvas)�� ����.
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
