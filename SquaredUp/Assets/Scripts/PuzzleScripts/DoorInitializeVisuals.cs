using UnityEngine;

/// <summary>Initializes the visuals of the door spawned by InstantiateLevel2Puzzle2.</summary>
public class DoorInitializeVisuals : MonoBehaviour
{
    // Perfab of the visual component to spawn.
    [SerializeField] private GameObject singleVisualPrefab = null;
    // Parent of all the visual components.
    [SerializeField] private Transform visualParent = null;
    // Transform of the physics parent of the door.
    [SerializeField] private Transform physicsTransform = null;


    // Start is called before the first frame update
    void Start()
    {
        // Get scale
        Vector2Int scale = new Vector2Int(Mathf.RoundToInt(transform.localScale.x), Mathf.RoundToInt(transform.localScale.y));
        // Move scale to be on the physics only
        physicsTransform.localScale = transform.localScale;
        transform.localScale = Vector3.one;

        float xWidth = singleVisualPrefab.transform.localScale.x;
        float yWidth = singleVisualPrefab.transform.localScale.y;
        float initialX = -(scale.x - 1) * xWidth / 2f;
        float currentX = initialX;
        float currentY = -(scale.y - 1) * yWidth / 2f;
        // Spawn visual components
        for (int i = 0; i < scale.y; ++i)
        {
            for (int k = 0; k < scale.x; ++k)
            {
                // Spawn and position the visual component
                GameObject doorVisual = Instantiate(singleVisualPrefab, visualParent);
                doorVisual.transform.localPosition = new Vector3(currentX, currentY);

                // Increment x
                currentX += xWidth;
            }
            // Increment y, reset x
            currentY += yWidth;
            currentX = initialX;
        }
    }
}
