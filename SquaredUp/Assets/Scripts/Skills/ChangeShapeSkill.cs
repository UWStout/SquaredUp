using UnityEngine;

public class ChangeShapeSkill : Skill
{
    private Transform playerScalableTrans;
    private MeshFilter playerMeshFilterRef;
    private PlayerColliderController playerCollContRef;

    private Mesh changeToMesh;
    private ShapeData.ColliderType changeToCollider;
    private Vector3 changeToScale;

    public void SpecifyChangeType(ShapeData data)
    {
        changeToMesh = data.Mesh;
        changeToCollider = data.ColliderShape;
        changeToScale = data.Scale;
    }

    public override void Use()
    {
        playerMeshFilterRef.mesh = changeToMesh;
        playerCollContRef.ActivateCollider(changeToCollider);
        playerScalableTrans.localScale = changeToScale;
    }
}
