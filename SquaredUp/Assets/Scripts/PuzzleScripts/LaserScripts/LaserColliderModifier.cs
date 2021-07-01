using UnityEngine;

/// <summary>
/// Base class to add behaviour to a laser when the laser hits this collider.
/// </summary>
public abstract class LaserColliderModifier : MonoBehaviour
{
    /// <summary>
    /// Called when the laser is hitting this collider.
    /// </summary>
    /// <param name="laser">Laser that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the laser came from.</param>
    public abstract void HandleLaser(Laser laser, Vector2 hitPoint, Vector2 incidentDirection);
    /// <summary>
    /// Called when the laser stops hitting this collider.
    /// </summary>
    /// <param name="laser">Laser that stopped hitting the collider.</param>
    public abstract void LaserRemoved(Laser laser);
}
