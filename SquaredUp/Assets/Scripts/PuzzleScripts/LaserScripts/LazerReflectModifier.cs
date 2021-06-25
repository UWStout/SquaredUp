using UnityEngine;

/// <summary>
/// LazerColliderModifier that reflects the lazer.
/// </summary>
[DisallowMultipleComponent]
public class LazerReflectModifier : LazerColliderModifier
{
    // An angle offset of 0 means that the normalize of the reflector is its up
    [SerializeField] [Range(0.0f, 360.0f)] private float angleOffset = 0.0f;


    /// <summary>
    /// Shoots the lazer in a reflected direction.
    /// 
    /// Called when the lazer is hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the lazer came from.</param>
    public override void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection)
    {
        Vector2 reflectDirection = DetermineReflectDirection(incidentDirection);
        lazer.ShootLazer(reflectDirection);
    }
    public override void LazerRemoved(Lazer lazer) { }


    /// <summary>
    /// Finds what angle to reflect the lazer at from the incident ray.
    /// </summary>
    /// <param name="incidentDirection">Direction of the incident vector to determine the reflect vector.</param>
    /// <returns>Reflected vector.</returns>
    private Vector2 DetermineReflectDirection(Vector2 incidentDirection)
    {
        Vector2 normal = DetermineSurfaceNormal();
        Vector3 reflection = Vector2.Reflect(incidentDirection, normal).normalized;
        return reflection;
    }
    /// <summary>
    /// Finds the normal to the reflective surface.
    /// </summary>
    /// <returns></returns>
    private Vector2 DetermineSurfaceNormal()
    {
        Vector3 normal = Quaternion.Euler(new Vector3(0, 0, angleOffset)) * transform.up;
        return normal;
    }
}
