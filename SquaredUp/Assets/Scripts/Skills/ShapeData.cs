using UnityEngine;

// Data to describe the shape the player will change into in the change shape skill
[CreateAssetMenu(fileName = "Shape Data", menuName = "ScriptablesObjects/ShapeData")]
public class ShapeData : ScriptableObject
{
    // Types of 2D colliders
    public enum ColliderType { BOX, CIRCLE}

    // Mesh that the shape will be changed to
    [SerializeField]
    private Mesh mesh = null;
    public Mesh Mesh { get { return mesh; } }

    // Type of 2D collider that this shape uses
    [SerializeField]
    private ColliderType colliderShape = ColliderType.BOX;
    public ColliderType ColliderShape { get { return colliderShape; } }

    // Scale of this shape
    [SerializeField]
    private Vector3 scale = Vector3.one;
    public Vector3 Scale { get { return scale; } }
}
