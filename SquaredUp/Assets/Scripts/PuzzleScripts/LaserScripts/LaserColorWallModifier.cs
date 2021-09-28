using UnityEngine;

[DisallowMultipleComponent]
public class LaserColorWallModifier : LaserColliderModifier
{
    // Color that we need the laser to be to turn off the obstruction
    [SerializeField] private Material requiredColor = null;


    /// <summary>
    /// Changes the color of the laser.
    /// 
    /// Called when the laser is hitting this collider.
    /// </summary>
    /// <param name="laser">Laser that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the laser came from.</param>
    public override void HandleLaser(Laser laser, Vector2 hitPoint, Vector2 incidentDirection)
    {
        if (laser.GetLineColor() == requiredColor.color)
        {
            laser.ShootLaser(incidentDirection);
        }
    }
    public override void LaserRemoved(Laser laser) { }
}
