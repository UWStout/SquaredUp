using UnityEngine;

/// <summary>
/// LazerColliderModifier that turns off an obstruction if hit with the correctly colored lazer.
/// </summary>
public class LazerRemoveObstructionModifier : LazerColliderModifier
{
    // Reference to the obstruction to turn off
    [SerializeField] private GameObject obstacle = null;
    // If we need a specificly colored lazer to turn off the obstruction
    [SerializeField] private bool colorSpecific = false;
    // Color that we need the lazer to be to turn off the obstruction
    [SerializeField] private Material requiredColor = null;


    /// <summary>
    /// Turns off the obstruction if the lazer is the correct color.
    /// 
    /// Called when the lazer is hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the lazer came from.</param>
    public override void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection)
    {
        if (colorSpecific)
        {
            if (lazer.GetLineColor() == requiredColor.color)
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
    /// Called when the lazer stops hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that stopped hitting the collider.</param>
    public override void LazerRemoved(Lazer lazer)
    {
        obstacle.SetActive(true);
    }
}
