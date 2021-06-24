using UnityEngine;

public class LazerColorModifier : LazerColliderModifier
{
    [SerializeField] private LineRenderer coloredLineRenderer = null;
    [SerializeField] [Min(0)] private float size = 1.5f;
    [SerializeField] private Material lineColor = null;

    private Lazer activeLazer = null;


    private void Awake()
    {
        coloredLineRenderer.enabled = false;
        coloredLineRenderer.startColor = lineColor.color;
        coloredLineRenderer.endColor = lineColor.color;
    }


    public override void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection)
    {
        coloredLineRenderer.enabled = true;
        // This is a heavy assumption that the lazer is coming from one of four directions and that the object is a uniform size
        Vector2 startPos = hitPoint + incidentDirection * size;
        // Create the new lazer (will update automatically from constructor)
        activeLazer = new Lazer(coloredLineRenderer, startPos, incidentDirection, lazer);

        coloredLineRenderer.SetPosition(coloredLineRenderer.positionCount - 1, hitPoint);
    }
    public override void LazerRemoved(Lazer lazer)
    {
        activeLazer.ClearLazer();
        coloredLineRenderer.enabled = false;
    }
}
