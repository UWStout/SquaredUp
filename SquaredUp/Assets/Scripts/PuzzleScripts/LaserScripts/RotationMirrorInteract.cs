using UnityEngine;

/// <summary>
/// Rotates the specified object by 90 degrees when interacted with.
/// </summary>
public class RotationMirrorInteract : Interactable
{
    // Object to rotate
    [SerializeField] private GameObject rotateObject = null;


    /// <summary>
    /// Rotates the object 90 degrees.
    /// 
    /// Called when the player interacts with this interactable.
    /// </summary>
    public override void InteractAbstract()
    {
        rotateObject.transform.Rotate(0f, 0f, 90f);
    }
}
