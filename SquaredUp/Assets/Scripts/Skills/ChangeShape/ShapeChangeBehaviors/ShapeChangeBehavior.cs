using UnityEngine;

/// <summary>
/// Base behavior definition for when the shape is changed.
/// </summary>
public abstract class ShapeChangeBehavior : ScriptableObject
{
    // If we are restricting changing shape.
    private bool restrictChange = false;
    public bool RestrictChange { get { return restrictChange; } set { restrictChange = value; } }

    /// <summary>Called when the shape is change to.</summary>
    public virtual void ChangeTo() { }
    /// <summary>Called when the shape is changed from.</summary>
    public virtual void ChangeFrom() { }
}