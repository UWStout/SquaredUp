using UnityEngine;
using UnityEditor;

/// <summary>Data to describe the shape the player will change into in the change shape skill</summary>
[CreateAssetMenu(fileName = "Shape Data", menuName = "ScriptableObjects/SkillData/ShapeData")]
public class ShapeData : SkillStateData
{
    // Types of 2D colliders
    public enum ShapeType { BOX, CIRCLE, TRIANGLE}
    // Constants
    // Points for the types of colliders that are polygonal
    public static readonly Vector2[] TRIANGLE_POINTS = { new Vector2(0, 0.5f), new Vector2(-0.5f, -0.36603f), new Vector2(0.5f, -0.36603f) };

    // Type of 2D collider that this shape uses
    [SerializeField] private ShapeType typeOfShape = ShapeType.BOX;
    public ShapeType TypeOfShape { get { return typeOfShape; } }

    // Scale of this shape
    [SerializeField] private Vector3 scale = Vector3.one;
    public Vector3 Scale { get { return scale; } }

    // If direction affects the scale
    [SerializeField] private bool directionAffectsScale = false;
    public bool DirectionAffectsScale { get { return directionAffectsScale; } }

    // If this data has a shape change behavior
    [SerializeField] private bool hasShapeChangeBehavior = false;
    public bool HasShapeChangeBehavior { get { return hasShapeChangeBehavior; } }
    // Shape change behavior
    [SerializeField] [HideInInspector] private ShapeChangeBehavior shapeChangeBehave = null;
    public ShapeChangeBehavior ShapeChangeBehave { get { return shapeChangeBehave; } set { shapeChangeBehave = value; } }
}

// Editor to hide shape change behavior if we don't have it for this data
#if UNITY_EDITOR
[CustomEditor(typeof(ShapeData))]
public class ShapeData_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ShapeData script = (ShapeData)target;

        // If we have shape change behavior, show the behavior in inspector
        if (script.HasShapeChangeBehavior)
        {
            script.ShapeChangeBehave = EditorGUILayout.ObjectField("Shape Change Behavior", script.ShapeChangeBehave,
                typeof(ShapeChangeBehavior), true) as ShapeChangeBehavior;
        }
    }
}
#endif