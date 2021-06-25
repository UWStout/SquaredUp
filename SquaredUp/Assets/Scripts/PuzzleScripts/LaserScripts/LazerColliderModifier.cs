using UnityEngine;

/// <summary>
/// Base class to add behaviour to a lazer when the lazer hits this collider.
/// </summary>
public abstract class LazerColliderModifier : MonoBehaviour
{
    /// <summary>
    /// Called when the lazer is hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the lazer came from.</param>
    public abstract void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection);
    /// <summary>
    /// Called when the lazer stops hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that stopped hitting the collider.</param>
    public abstract void LazerRemoved(Lazer lazer);
}
