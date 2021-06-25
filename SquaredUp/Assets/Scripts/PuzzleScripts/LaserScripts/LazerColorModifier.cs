using UnityEngine;

/// <summary>
/// LazerColliderModifier that changes the color of the lazer.
/// </summary>
[DisallowMultipleComponent]
public class LazerColorModifier : LazerColliderModifier
{
    // Line renderer to use to render the different colored line
    [SerializeField] private LineRenderer coloredLineRenderer = null;
    // Size of the lazer color changer
    [SerializeField] [Min(0)] private float size = 1.5f;
    // Material holding the color to change to
    [SerializeField] private Material lineColor = null;

    // Active lazer that is being shot
    private Lazer activeLazer = null;


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
    /// Changes the color of the lazer.
    /// 
    /// Called when the lazer is hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that is hititng this collider.</param>
    /// <param name="hitPoint">Point on the collider that the collider hit.</param>
    /// <param name="incidentDirection">Direction the lazer came from.</param>
    public override void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection)
    {
        coloredLineRenderer.enabled = true;
        // This is a heavy assumption that the lazer is coming from one of four directions and that the object is a uniform size
        Vector2 startPos = hitPoint + incidentDirection * size;
        // Create the new lazer (will update automatically from constructor)
        activeLazer = new Lazer(coloredLineRenderer, startPos, incidentDirection, lazer);

        // After the new lazer has been shot, update the line renderer's last position (beginning position)
        // to be where the other lazer ended
        coloredLineRenderer.SetPosition(coloredLineRenderer.positionCount - 1, hitPoint);
    }
    /// <summary>
    /// Clears the other colored lazer and disables the line renderer.
    /// 
    /// Called when the lazer stops hitting this collider.
    /// </summary>
    /// <param name="lazer">Lazer that stopped hitting the collider.</param>
    public override void LazerRemoved(Lazer lazer)
    {
        activeLazer.ClearLazer();
        coloredLineRenderer.enabled = false;
    }
}
