using UnityEngine;

/// <summary>
/// Shoots the lazer from the position of this script.
/// </summary>
public class StartLaserScript : MonoBehaviour
{
    // Layer mask to use to detect lazer collisions
    [SerializeField] private LayerMask layerMask = 1;
    // Line renderer to render the lazer
    [SerializeField] private LineRenderer lineRenderer = null;

    // Lazer to control the line renderer and cast rays with
    private Lazer lazer = null;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        lazer = new Lazer(lineRenderer, transform.position, transform.up, layerMask);
    }
    // Called once every frame
    private void Update()
    {
        lazer.UpdateLazer();
    }
}
