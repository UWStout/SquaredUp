using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Refernece to the target to follow.
    [SerializeField]
    private Transform followTarget = null;

    // LateUpdate is called once per end of frame
    private void LateUpdate()
    {
        Vector3 pos = followTarget.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
