using UnityEngine;

/// <summary>Skill that allows the player to change their shape</summary>
public class ChangeShapeSkill : SkillBase<ShapeData>
{
    // References
    // Transform that will be scaled for the player
    [SerializeField] private Transform playerScalableTrans = null;
    // Mesh Filters that will have their mesh changed to the shape being changed to
    [SerializeField] private MeshFilter[] playerMeshFilterRefs = null;

    /// <summary>Changes the player to become the shape corresponding to the given index.
    /// Index matches what is specified in the editor. If index is unknown, consider using Use(ShapeData) instead.</summary>
    public override void Use(int stateIndex)
    {
        ShapeData data = SkillData.GetData(stateIndex);
        // Change all the meshes
        foreach (MeshFilter filter in playerMeshFilterRefs)
        {
            filter.mesh = data.Mesh;
        }
        // Swap the colliders
        PlayerColliderController.Instance.ActivateCollider(data.ColliderShape);
        // Adjust the scale
        playerScalableTrans.localScale = data.Scale;
    }
}
