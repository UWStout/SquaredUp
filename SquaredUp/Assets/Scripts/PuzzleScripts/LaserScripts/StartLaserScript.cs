using UnityEngine;

/// <summary>
/// Shoots the laser from the position of this script.
/// </summary>
public class StartLaserScript : MonoBehaviour
{
    // Layer mask to use to detect laser collisions
    [SerializeField] private LayerMask layerMask = 1;
    // Line renderer to render the laser
    [SerializeField] private LineRenderer lineRenderer = null;

    // Laser to control the line renderer and cast rays with
    private Laser laser = null;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        laser = new Laser(lineRenderer, transform.position, transform.up, layerMask);
    }
    // Called once every frame
    private void Update()
    {
        laser.UpdateLaser();
    }
}
