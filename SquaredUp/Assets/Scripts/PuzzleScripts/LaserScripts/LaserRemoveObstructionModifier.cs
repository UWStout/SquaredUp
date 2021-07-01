using UnityEngine;

/// <summary>
/// LaserColliderModifier that turns off an obstruction if hit with the correctly colored laser.
/// </summary>
public class LaserRemoveObstructionModifier : LaserColliderModifier
{
    // Reference to the obstruction to turn off
    [SerializeField] private GameObject obstacle = null;
    // If we need a specificly colored laser to turn off the obstruction
    [SerializeField] private bool colorSpecific = false;
    // Color that we need the laser to be to turn off the obstruction
    [SerializeField] private Material requiredColor = null;


    /// <summary>
    /// Turns off the obstruction if the laser is the correct color.
    /// 
    /// Called when the laser is hitting this collider.
    /// </summary>
    /// <param name="laser">Laser that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the laser came from.</param>
    public override void HandleLaser(Laser laser, Vector2 hitPoint, Vector2 incidentDirection)
    {
        if (colorSpecific)
        {
            if (laser.GetLineColor() == requiredColor.color)
            {
                obstacle.SetActive(false);
            }
        }
        else
        {
            obstacle.SetActive(false);
        }
    }
    /// <summary>
    /// Reanables the obstruction.
    /// 
    /// Called when the laser stops hitting this collider.
    /// </summary>
    /// <param name="laser">Laser that stopped hitting the collider.</param>
    public override void LaserRemoved(Laser laser)
    {
        obstacle.SetActive(true);
    }
}
