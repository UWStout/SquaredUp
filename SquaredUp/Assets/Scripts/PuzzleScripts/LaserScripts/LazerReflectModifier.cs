using UnityEngine;

[DisallowMultipleComponent]
public class LazerReflectModifier : LazerColliderModifier
{
    // An angle offset of 0 means that the normalize of the reflector is its up
    [SerializeField] [Range(0.0f, 360.0f)] private float angleOffset = 0.0f;


    public override void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection)
    {
        Vector2 reflectDirection = DetermineReflectDirection(incidentDirection);
        lazer.ShootLazer(reflectDirection);
    }
    public override void LazerRemoved(Lazer lazer) { }


    private Vector2 DetermineReflectDirection(Vector2 incidentDirection)
    {
        Vector2 normal = DetermineSurfaceNormal();
        Vector3 reflection = Vector2.Reflect(incidentDirection, normal).normalized;
        return reflection;
    }
    private Vector2 DetermineSurfaceNormal()
    {
        Vector3 normal = Quaternion.Euler(new Vector3(0, 0, angleOffset)) * transform.up;
        return normal;
    }
}
