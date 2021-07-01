using UnityEngine;

/// <summary>
/// LaserColliderModifier that changes the color of the laser.
/// </summary>
[DisallowMultipleComponent]
public class LaserColorModifier : LaserColliderModifier
{
    // Line renderer to use to render the different colored line
    [SerializeField] private LineRenderer coloredLineRenderer = null;
    // Size of the laser color changer
    [SerializeField] [Min(0)] private float size = 1.5f;
    // Material holding the color to change to
    [SerializeField] private Material lineColor = null;

    // Active laser that is being shot
    private Laser activeLaser = null;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        // Have the line renderer off by default
        coloredLineRenderer.enabled = false;
        // Set color of the line renderer
        coloredLineRenderer.startColor = lineColor.color;
        coloredLineRenderer.endColor = lineColor.color;
    }


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
        coloredLineRenderer.enabled = true;
        // This is a heavy assumption that the laser is coming from one of four directions and that the object is a uniform size
        Vector2 startPos = hitPoint + incidentDirection * size;
        // Create the new laser (will update automatically from constructor)
        activeLaser = new Laser(coloredLineRenderer, startPos, incidentDirection, laser);

        // After the new laser has been shot, update the line renderer's last position (beginning position)
        // to be where the other laser ended
        coloredLineRenderer.SetPosition(coloredLineRenderer.positionCount - 1, hitPoint);
    }
    /// <summary>
    /// Clears the other colored laser and disables the line renderer.
    /// 
    /// Called when the laser stops hitting this collider.
    /// </summary>
    /// <param name="laser">Laser that stopped hitting the collider.</param>
    public override void LaserRemoved(Laser laser)
    {
        activeLaser.ClearLazer();
        coloredLineRenderer.enabled = false;
    }
}
