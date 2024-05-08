using UnityEngine;
public class TopDownCamera : MonoBehaviour
{
    public Transform Target;
    public string Owner;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 desiredPosition = Target.position + Offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, SmoothTime);
        }
    }
}
