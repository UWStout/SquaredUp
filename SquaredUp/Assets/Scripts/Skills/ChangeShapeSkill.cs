using UnityEngine;

public class ChangeShapeSkill : Skill
{
    [SerializeField] private Transform playerScalableTrans;
    [SerializeField] private MeshFilter[] playerMeshFilterRefs;
    [SerializeField] private PlayerColliderController playerCollContRef;

    private ShapeData changeToShape;

    public void SpecifyChangeType(ShapeData data)
    {
        changeToShape = data;
    }

    public override void Use()
    {
        foreach (MeshFilter filter in playerMeshFilterRefs)
        {
            filter.mesh = changeToShape.Mesh;
        }
        playerCollContRef.ActivateCollider(changeToShape.ColliderShape);
        playerScalableTrans.localScale = changeToShape.Scale;
        //playerScalableTrans.localPosition = (changeToShape.Scale - Vector3.one) / 2f;
    }
}
