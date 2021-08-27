using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoundedBoxColliderController : MonoBehaviour
{
    private BoxCollider2D boxCollider = null;


    private void Start()
    {
        // Must be done in start because colliders are possibly generated in collider controller
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Vector3 globalScale = boxCollider.transform.lossyScale;
        float radius2 = 2 * boxCollider.edgeRadius;

        float xSize = 1 - radius2 / globalScale.x;
        float ySize = 1 - radius2 / globalScale.y;

        boxCollider.size = new Vector2(xSize, ySize);
    }
}
