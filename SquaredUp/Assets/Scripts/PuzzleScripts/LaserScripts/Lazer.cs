using System.Collections.Generic;
using UnityEngine;

public class Lazer
{
    private const float LAZER_MAX_DISTANCE = 1000.0f;
    private const float LAZER_SHOOT_OFFSET = 0.01f;
    private const string LAZER_TAG = "Lazer";

    private Vector2 startPoint = Vector3.zero;
    private Vector2 initialDirection = Vector3.forward;
    private LayerMask layerMask = 1;
    private List<LazerColliderModifier> removeModifiers = new List<LazerColliderModifier>();
    private List<LazerColliderModifier> curModifiers = new List<LazerColliderModifier>();
    private Stack<Vector3> points = new Stack<Vector3>();

    private LineRenderer lineRenderer = null;

    private GameObject lastHitObj = null;


    public Lazer(LineRenderer lineRend, Vector2 start, Vector2 direction, LayerMask lazerLayer)
    {
        lineRenderer = lineRend;
        startPoint = start;
        initialDirection = direction;
        layerMask = lazerLayer;

        UpdateLazer();
    }
    public Lazer(LineRenderer lineRend, Vector2 start, Vector2 direction, Lazer other)
    {
        lineRenderer = lineRend;
        startPoint = start;
        initialDirection = direction;
        layerMask = other.layerMask;

        UpdateLazer();
    }


    public void UpdateLazer()
    {
        ResetModifiers();
        points.Clear();
        points.Push(startPoint);
        lastHitObj = null;
        ShootLazer(initialDirection);
        RefreshLineRenderer();
    }
    public void ShootLazer(Vector2 direction)
    {
        Vector2 recentPoint = points.Peek();
        RaycastHit2D hit = Physics2D.Raycast(recentPoint + direction * LAZER_SHOOT_OFFSET, direction, LAZER_MAX_DISTANCE, layerMask);
        if (hit)
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj == lastHitObj)
            {
                Debug.LogError("Laser is rehitting the same object");
                return;
            }
            lastHitObj = hitObj;

            Vector2 prevPoint = points.Peek();
            points.Push(hit.point);

            if (hit.collider.CompareTag(LAZER_TAG))
            {
                // Determine the incident direction of the lazer
                Vector2 incidentDirection = (hit.point - prevPoint).normalized;

                LazerColliderModifier[] modifiers = hit.collider.GetComponents<LazerColliderModifier>();
                foreach (LazerColliderModifier mod in modifiers)
                {
                    curModifiers.Add(mod);
                    removeModifiers.Remove(mod);
                    mod.HandleLazer(this, hit.point, incidentDirection);
                }
            }
        }
        else
        {
            points.Push(recentPoint + direction * LAZER_MAX_DISTANCE);
        }
    }
    public Color GetLineColor()
    {
        return lineRenderer.endColor;
    }
    public void ClearLazer()
    {
        foreach (LazerColliderModifier mod in curModifiers)
        {
            mod.LazerRemoved(this);
        }
    }


    private void ResetModifiers()
    {
        foreach (LazerColliderModifier mod in removeModifiers)
        {
            mod.LazerRemoved(this);
        }
        removeModifiers = new List<LazerColliderModifier>(curModifiers);
        curModifiers.Clear();
    }
    private void RefreshLineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
