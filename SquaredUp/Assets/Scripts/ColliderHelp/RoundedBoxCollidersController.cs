using UnityEngine;

public class RoundedBoxCollidersController : MonoBehaviour
{
    private BoxCollider2D[] boxColliders = new BoxCollider2D[0];


    private void Start()
    {
        // Must be done in start because colliders are possibly generated in collider controller
        boxColliders = GetComponentsInChildren<BoxCollider2D>();
    }
    private void Update()
    {
        foreach (BoxCollider2D col in boxColliders)
        {
            Vector3 globalScale = col.transform.lossyScale;
            float radius2 = 2 * col.edgeRadius;

            float xSize = 1 - radius2 / globalScale.x;
            float ySize = 1 - radius2 / globalScale.y;

            col.size = new Vector2(xSize, ySize);
        }
    }
}
