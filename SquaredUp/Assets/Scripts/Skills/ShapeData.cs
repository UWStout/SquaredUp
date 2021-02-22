using UnityEngine;

[CreateAssetMenu(fileName = "Shape Data", menuName = "ScriptablesObjects/ShapeData")]
public class ShapeData : ScriptableObject
{
    public enum ColliderType { BOX, CIRCLE}

    [SerializeField]
    private Mesh mesh = null;
    public Mesh Mesh { get { return mesh; } }

    [SerializeField]
    private ColliderType colliderShape = ColliderType.BOX;
    public ColliderType ColliderShape { get { return colliderShape; } }

    [SerializeField]
    private Vector3 scale = Vector3.one;
    public Vector3 Scale { get { return scale; } }
}
