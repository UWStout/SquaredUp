using UnityEngine;

// Skill that allows the player to change their shape
public class ChangeShapeSkill : Skill
{
    // References
    // Transform that will be scaled for the player
    [SerializeField] private Transform playerScalableTrans;
    // Mesh Filters that will have their mesh changed to the shape being changed to
    [SerializeField] private MeshFilter[] playerMeshFilterRefs;
    // Manages all the player's colliders that need to be changed
    [SerializeField] private PlayerColliderController playerCollContRef;

    // Data for the shape to change to
    private ShapeData changeToShape;

    /// <summary> Sets what kind of shape the player will change into </summary>
    /// <param name="data">Shape information</param>
    public void SpecifyChangeType(ShapeData data)
    {
        changeToShape = data;
    }

    /// <summary>Changes the player to become the currently specified shape</summary>
    public override void Use()
    {
        // Change all the meshes
        foreach (MeshFilter filter in playerMeshFilterRefs)
        {
            filter.mesh = changeToShape.Mesh;
        }
        // Swap the colliders
        playerCollContRef.ActivateCollider(changeToShape.ColliderShape);
        // Adjust the scale
        playerScalableTrans.localScale = changeToShape.Scale;
    }
}
