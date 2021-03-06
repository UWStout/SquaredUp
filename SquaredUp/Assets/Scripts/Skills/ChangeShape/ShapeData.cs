using UnityEngine;

/// <summary>Data to describe the shape the player will change into in the change shape skill</summary>
[CreateAssetMenu(fileName = "Shape Data", menuName = "ScriptablesObjects/SkillData/ShapeData")]
public class ShapeData : SkillStateData
{
    // Types of 2D colliders
    public enum ColliderType { BOX, CIRCLE, TRIANGLE}
    // Constants
    // Points for the types of colliders that are polygonal
    public static readonly Vector2[] TRIANGLE_POINTS = { new Vector2(-0.5f, 0.5f), new Vector2(-0.5f, -0.5f), new Vector2(0.5f, 0) };

    // Mesh that the shape will be changed to
    [SerializeField] private Mesh mesh = null;
    public Mesh Mesh { get { return mesh; } }

    // Type of 2D collider that this shape uses
    [SerializeField] private ColliderType colliderShape = ColliderType.BOX;
    public ColliderType ColliderShape { get { return colliderShape; } }

    // Scale of this shape
    [SerializeField] private Vector3 scale = Vector3.one;
    public Vector3 Scale { get { return scale; } }

    // If direction affects the scale
    [SerializeField] private bool directionAffectsScale = false;
    public bool DirectionAffectsScale { get { return directionAffectsScale; } }
}
