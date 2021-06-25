using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls a line renderer to draw a lazer.
/// </summary>
public class Lazer
{
    // Constants
    // Max distance to display a lazer when it doesn't hit anything
    private const float LAZER_MAX_DISTANCE = 1000.0f;
    // Small offset to move from the hit point before raycasting again
    private const float LAZER_SHOOT_OFFSET = 0.01f;
    // Tag of colliders that have lazer collider modifiers
    private const string LAZER_TAG = "Lazer";

    // Starting position of the lazer
    private Vector2 startPoint = Vector3.zero;
    // Direction to shoot the lazer in from the start position
    private Vector2 initialDirection = Vector3.forward;
    // Mask the lazer should interact with
    private LayerMask layerMask = 1;
    // Points to renderer the lazer in the line renderer
    private Stack<Vector3> points = new Stack<Vector3>();
    // Current modifiers that the lazer is hitting
    private List<LazerColliderModifier> curModifiers = new List<LazerColliderModifier>();
    // Modifiers that the lazer was hitting, but is not hitting any longer
    private List<LazerColliderModifier> removeModifiers = new List<LazerColliderModifier>();
    
    // Reference to the line renderer to display the lazer
    private LineRenderer lineRenderer = null;
    // Object that was hit last by the lazer. Used to be sure that we
    // aren't hitting the same thing multiple times and causing an infinite loop
    private GameObject lastHitObj = null;


    /// <summary>
    /// Constructs a lazer. Calls UpdateLazer right away.
    /// </summary>
    /// <param name="lineRend">Line renderer to be used to draw the lazer.</param>
    /// <param name="start">Position to cast the lazer from. First point of the line renderer.</param>
    /// <param name="direction">Direction to raycast in from the start position.</param>
    /// <param name="lazerLayer">Layer to raycast on to detect colliders that the lazer interacts with.</param>
    public Lazer(LineRenderer lineRend, Vector2 start, Vector2 direction, LayerMask lazerLayer)
    {
        lineRenderer = lineRend;
        startPoint = start;
        initialDirection = direction;
        layerMask = lazerLayer;

        UpdateLazer();
    }
    /// <summary>
    /// Constructs a lazer. Calls UpdateLazer right away.
    /// </summary>
    /// <param name="lineRend">Line renderer to be used to draw the lazer.</param>
    /// <param name="start">Position to cast the lazer from. First point of the line renderer.</param>
    /// <param name="direction">Direction to raycast in from the start position.</param>
    /// <param name="other">Other lazer to copy the layer mask from.</param>
    public Lazer(LineRenderer lineRend, Vector2 start, Vector2 direction, Lazer other)
    {
        lineRenderer = lineRend;
        startPoint = start;
        initialDirection = direction;
        layerMask = other.layerMask;

        UpdateLazer();
    }


    /// <summary>
    /// Reshoots the lazer from its starting position and tries to collide with
    /// colliders that have a lazer collider modifier on it.
    /// </summary>
    public void UpdateLazer()
    {
        // Reset any previous modifiers
        ResetModifiers();
        // Reinitialize the contact points
        points.Clear();
        points.Push(startPoint);
        // Re-setup the infinite loop guard
        lastHitObj = null;
        // Start shooting the lazer
        ShootLazer(initialDirection);
        // Refreshes the line renderer with the collected points from the ShootLazer
        RefreshLineRenderer();
    }
    /// <summary>
    /// Starts casting the lazer from its last point in the specified direction.
    /// </summary>
    /// <param name="direction">Direction to shoot the lazer in.</param>
    public void ShootLazer(Vector2 direction)
    {
        // Last point the lazer hit
        Vector2 recentPoint = points.Peek();
        RaycastHit2D hit = Physics2D.Raycast(recentPoint + direction * LAZER_SHOOT_OFFSET, direction, LAZER_MAX_DISTANCE, layerMask);
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

            // Previous point to determine the incident direction from
            Vector2 prevPoint = points.Peek();
            // Put the hit point as a hit point on the lazer
            points.Push(hit.point);

            // This tag should only exist on objets with LazerColliderModifiers
            if (hit.collider.CompareTag(LAZER_TAG))
            {
                // Determine the incident direction of the lazer
                Vector2 incidentDirection = (hit.point - prevPoint).normalized;

                // Pull all modifiers off the object
                LazerColliderModifier[] modifiers = hit.collider.GetComponents<LazerColliderModifier>();
                foreach (LazerColliderModifier mod in modifiers)
                {
                    // Add the mod to the current mods and potentially remove it from the mods to remove so it
                    // doesn't get turned off
                    curModifiers.Add(mod);
                    removeModifiers.Remove(mod);
                    // Call the logic for what happens when the lazer hits the modifier
                    mod.HandleLazer(this, hit.point, incidentDirection);
                }
            }
        }
        // If there was no hit, add a point on the lazer very far away
        else
        {
            points.Push(recentPoint + direction * LAZER_MAX_DISTANCE);
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
        foreach (LazerColliderModifier mod in curModifiers)
        {
            mod.LazerRemoved(this);
        }
    }


    /// <summary>
    /// Removes old modifiers that are no longer selected.
    /// </summary>
    private void ResetModifiers()
    {
        foreach (LazerColliderModifier mod in removeModifiers)
        {
            mod.LazerRemoved(this);
        }
        removeModifiers = new List<LazerColliderModifier>(curModifiers);
        curModifiers.Clear();
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
