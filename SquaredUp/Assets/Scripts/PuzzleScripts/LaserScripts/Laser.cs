using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls a line renderer to draw a laser.
/// </summary>
public class Laser
{
    // Constants
    // Max distance to display a laser when it doesn't hit anything
    private const float LASER_MAX_DISTANCE = 1000.0f;
    // Small offset to move from the hit point before raycasting again
    private const float LASER_SHOOT_OFFSET = 0.01f;
    // Tag of colliders that have laser collider modifiers
    private const string LASER_TAG = "Lazer";

    // Starting position of the laser
    private Vector2 startPoint = Vector3.zero;
    // Direction to shoot the laser in from the start position
    private Vector2 initialDirection = Vector3.forward;
    // Mask the laser should interact with
    private LayerMask layerMask = 1;
    // Points to renderer the lazer in the line renderer
    private Stack<Vector3> points = new Stack<Vector3>();
    // Current modifiers that the laser is hitting
    private List<LaserColliderModifier> curModifiers = new List<LaserColliderModifier>();
    
    // Reference to the line renderer to display the laser
    private LineRenderer lineRenderer = null;
    // Object that was hit last by the laser. Used to be sure that we
    // aren't hitting the same thing multiple times and causing an infinite loop
    private GameObject lastHitObj = null;
    private List<GameObject> hitObjs = new List<GameObject>();


    /// <summary>
    /// Constructs a laser. Calls UpdateLazer right away.
    /// </summary>
    /// <param name="lineRend">Line renderer to be used to draw the laser.</param>
    /// <param name="start">Position to cast the laser from. First point of the line renderer.</param>
    /// <param name="direction">Direction to raycast in from the start position.</param>
    /// <param name="laserLayer">Layer to raycast on to detect colliders that the laser interacts with.</param>
    public Laser(LineRenderer lineRend, Vector2 start, Vector2 direction, LayerMask laserLayer)
    {
        lineRenderer = lineRend;
        startPoint = start;
        initialDirection = direction;
        layerMask = laserLayer;

        UpdateLaser();
    }
    /// <summary>
    /// Constructs a laser. Calls UpdateLaser right away.
    /// </summary>
    /// <param name="lineRend">Line renderer to be used to draw the laser.</param>
    /// <param name="start">Position to cast the laser from. First point of the line renderer.</param>
    /// <param name="direction">Direction to raycast in from the start position.</param>
    /// <param name="other">Other laser to copy the layer mask from.</param>
    public Laser(LineRenderer lineRend, Vector2 start, Vector2 direction, Laser other) : this(lineRend, start, direction, other.layerMask)
    {
    }


    /// <summary>
    /// Reshoots the laser from its starting position and tries to collide with
    /// colliders that have a laser collider modifier on it.
    /// </summary>
    public void UpdateLaser()
    {
        // Reset any previous modifiers
        ClearLazer();
        // Reinitialize the contact points
        points.Clear();
        points.Push(startPoint);
        // Re-setup the infinite loop guard
        lastHitObj = null;
        hitObjs.Clear();
        // Start shooting the laser
        ShootLaser(initialDirection);
        // Refreshes the line renderer with the collected points from the ShootLaser
        RefreshLineRenderer();
    }
    /// <summary>
    /// Starts casting the laser from its last point in the specified direction.
    /// </summary>
    /// <param name="direction">Direction to shoot the laser in.</param>
    public void ShootLaser(Vector2 direction)
    {
        // Last point the laser hit
        Vector2 recentPoint = points.Peek();
        RaycastHit2D hit = Physics2D.Raycast(recentPoint + direction * LASER_SHOOT_OFFSET, direction, LASER_MAX_DISTANCE, layerMask);
        // If there was a hit and the hit was not due to a mirror
        if (hit)
        {
            // Check to make sure that the hit thing was not the last thing we hit to avoid an infinite loop
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj == lastHitObj)
            {
                Debug.LogError("Laser is rehitting the same object");
                return;
            }
            lastHitObj = hitObj;
            // Make sure we are not hitting an object we hit previously to avoid an infinite loop
            if (hitObjs.Contains(hitObj))
            {
                Debug.LogError("Laser is rehitting the a previous object");
                return;
            }
            else
            {
                hitObjs.Add(hitObj);
            }

            // Previous point to determine the incident direction from
            Vector2 prevPoint = points.Peek();
            // Put the hit point as a hit point on the laser
            points.Push(hit.point);

            // This tag should only exist on objets with LaserColliderModifiers
            if (hit.collider.CompareTag(LASER_TAG))
            {
                // Determine the incident direction of the laser
                Vector2 incidentDirection = (hit.point - prevPoint).normalized;

                // Pull all modifiers off the object
                LaserColliderModifier[] modifiers = hit.collider.GetComponents<LaserColliderModifier>();
                foreach (LaserColliderModifier mod in modifiers)
                {
                    // Add the mod to the current mods
                    curModifiers.Add(mod);
                    // Call the logic for what happens when the laser hits the modifier
                    mod.HandleLaser(this, hit.point, incidentDirection);
                }
            }
        }
        // If there was no hit, add a point on the lazer very far away
        else
        {
            points.Push(recentPoint + direction * LASER_MAX_DISTANCE);
        }
    }
    /// <summary>
    /// Gets the line renderer's current color.
    /// </summary>
    /// <returns>Lazer's color.</returns>
    public Color GetLineColor()
    {
        return lineRenderer.endColor;
    }
    /// <summary>
    /// Resets the lazer by removing it from all modifiers.
    /// </summary>
    public void ClearLazer()
    {
        foreach (LaserColliderModifier mod in curModifiers)
        {
            mod.LaserRemoved(this);
        }
        curModifiers.Clear();
    }
    public void RemoveLayerFromMask(int layer)
    {
        layerMask = layerMask & ~(1 << layer);
    }
    public void AddLayerToMask(int layer)
    {
        layerMask = layerMask | (1 << layer);
    }


    /// <summary>
    /// Sets the positions of the line renderer to reflect the collected points.
    /// </summary>
    private void RefreshLineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
