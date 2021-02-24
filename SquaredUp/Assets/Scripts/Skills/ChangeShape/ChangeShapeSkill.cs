using UnityEngine;

// Skill that allows the player to change their shape
public class ChangeShapeSkill : Skill
{
    // Enum of the supported shapes.
    public enum Shape { SQUARE, RECTANGLE, CIRCLE, TRIANGLE };

    // References
    // Transform that will be scaled for the player
    [SerializeField] private Transform playerScalableTrans;
    // Mesh Filters that will have their mesh changed to the shape being changed to
    [SerializeField] private MeshFilter[] playerMeshFilterRefs;
    // Manages all the player's colliders that need to be changed
    private PlayerColliderController playerCollContRef;

    // ShapeData for the shapes to change into
    [SerializeField] private ShapeData squareData = null;
    [SerializeField] private ShapeData rectangleData = null;
    [SerializeField] private ShapeData circleData = null;
    [SerializeField] private ShapeData triangleData = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        playerCollContRef = FindObjectOfType<PlayerColliderController>();
        if (playerCollContRef == null)
        {
            Debug.LogError("ChangeShapeSkill could not find PlayerColliderController");
        }
    }

    /// <summary>Changes the player to become the currently specified shape</summary>
    /// <param name="enumVal">Should be a value of the enum Shape</param>
    public override void Use(int enumVal)
    {
        ShapeData data = GetShapeData((Shape)enumVal);
        // Change all the meshes
        foreach (MeshFilter filter in playerMeshFilterRefs)
        {
            filter.mesh = data.Mesh;
        }
        // Swap the colliders
        playerCollContRef.ActivateCollider(data.ColliderShape);
        // Adjust the scale
        playerScalableTrans.localScale = data.Scale;
    }

    /// <summary>Gets the data for the current shape.</summary>
    private ShapeData GetShapeData(Shape changeToShape)
    {
        switch (changeToShape)
        {
            case Shape.SQUARE:
                return squareData;
            case Shape.RECTANGLE:
                return rectangleData;
            case Shape.CIRCLE:
                return circleData;
            case Shape.TRIANGLE:
                return triangleData;
            default:
                Debug.LogError("Unhandled Shape " + changeToShape + " in ChangeShapeSkill");
                return squareData;
        }
    }
}
