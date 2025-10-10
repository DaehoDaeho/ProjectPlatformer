using UnityEngine;

public class WorldSpaceCanvasBinder : MonoBehaviour
{
    public void BindCamera(Camera cam)
    {
        Canvas canvas = GetComponent<Canvas>();
        if(canvas != null)
        {
            if(canvas.renderMode == RenderMode.WorldSpace)
            {
                canvas.worldCamera = cam;
            }
        }
    }
}
