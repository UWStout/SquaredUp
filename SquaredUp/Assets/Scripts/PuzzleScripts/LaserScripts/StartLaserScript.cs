using UnityEngine;

/// <summary>
/// Shoots the laser from the position of this script.
/// </summary>
public class StartLaserScript : MonoBehaviour
{
    // Line renderer to render the laser
    [SerializeField] private LineRenderer lineRenderer = null;

    // Laser to control the line renderer and cast rays with
    private Laser laser = null;


    // Called 0th
    // Foreign Initialization
    private void Start()
    {
        laser = new Laser(lineRenderer, transform.position, transform.up);
    }
    // Called once every frame
    private void Update()
    {
        laser.UpdateLaser();
    }
}
