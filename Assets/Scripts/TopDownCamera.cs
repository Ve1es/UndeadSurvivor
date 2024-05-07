using UnityEngine;
public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public string owner;
    public Vector3 offset;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }
    private void SetTarget(GameObject playerObject)
    {
        target = playerObject.transform;
    }
}
