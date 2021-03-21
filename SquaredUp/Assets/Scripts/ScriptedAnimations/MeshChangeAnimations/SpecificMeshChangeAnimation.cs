using UnityEngine;

/// <summary>Animation to transition between specified shapes.</summary>
public class SpecificMeshChangeAnimation : ShapeChangingAnimation
{
    // Reference to the shapes to change to
    [SerializeField] private ShapeData.ShapeType[] shapeTypes = null;

    /// <summary>Initialize the shapes to be the specified shape tpes</summary>
    protected override ShapeData.ShapeType[] Initialize()
    {
        return shapeTypes;
    }
}
