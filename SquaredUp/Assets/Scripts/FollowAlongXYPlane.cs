using UnityEngine;

// Put this on the camera and give it a follow target. It will follow that target on the x and y positions.
public class FollowAlongXYPlane : MonoBehaviour
{
    // Refernece to the target to follow.
    [SerializeField]
    private Transform followTarget = null;

    // LateUpdate is called once per end of frame
    private void LateUpdate()
    {
        Vector3 pos = followTarget.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
