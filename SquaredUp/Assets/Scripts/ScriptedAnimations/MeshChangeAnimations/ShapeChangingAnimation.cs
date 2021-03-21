using UnityEngine;

/// <summary>Base class to animate a mesh using a MeshTransitioner.</summary>
[RequireComponent(typeof(BlendTransitioner))]
public abstract class ShapeChangingAnimation : StateChangeAnimation<ShapeData.ShapeType>
{
    // References
    // Reference to the blend transitioner
    private BlendTransitioner blendTransitioner;

    // Called 0th
    // Set references
    private void Awake()
    {
        blendTransitioner = GetComponent<BlendTransitioner>();
    }


    /// <summary>Start changing to the given shape.</summary>
    /// <param name="shapeToChangeTo">Shape to change to.</param>
    protected override void ChangeStateCall(ShapeData.ShapeType shapeToChangeTo)
    {
        blendTransitioner.StartChangeShape(shapeToChangeTo, ChangeAfterDelay);
    }
}
