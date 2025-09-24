using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5.0f;

    private void LateUpdate()
    {
        Vector3 current = transform.position;
        Vector3 desired = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector3 smooth = Vector3.Lerp(current, desired, followSpeed * Time.deltaTime);
        transform.position = new Vector3(smooth.x, smooth.y, current.z);
    }
}
