using System.Collections.Generic;
using UnityEngine;

// Class that helps debug some physics stuff
public static class PhysicsDebugging
{
    /// <summary>Returns the result of a Physics2D BoxCast and draws lines in the editor for it.
    /// To view these lines, call Debug.Break() after calling this function as lines are discarded the frame
    /// after they are drawn.</summary>
    /// <param name="origen">The point in 2D space where the box originates.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box (in degrees).</param>
    /// <param name="direction">A vector representing the direction of the box.</param>
    /// <param name="distance">The maximum distance over which to cast the box.</param>
    /// <param name="mask">Filter to detect Colliders only on certain layers.</param>
    /// <returns>RaycastHit2D The cast results returned.</returns>
    public static RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance, int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }

    /// <summary>Returns the result of a Physics2D CircleCast and draws lines in the editor for it.
    /// To view these lines, call Debug.Break() after calling this function as lines are discarded the frame
    /// after they are drawn.</summary>
    /// <param name="origin">The point in 2D space where the circle originates.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="direction">A vector representing the direction of the circle.</param>
    /// <param name="distance">The maximum distance over which to cast the circle.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>RaycastHit2D The cast results returned.</returns>
    public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers,
        float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity)
    {
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth, maxDepth);

        // Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        DrawCircle(origin, radius, castColor);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }

    /// <summary>Draws a circle using Debug.DrawLine() calls.</summary>
    /// <param name="origin">The point in 2D space where the circle originates.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">Color of the lines</param>
    /// <param name="detailLevel">Optional value that can increase the curvature of the circle</param>
    public static void DrawCircle(Vector2 origin, float radius, Color color, int detailLevel = 16)
    {
        float curRad = 0;
        float incrAm = 2 * Mathf.PI / detailLevel;
        for (int i = 0; i < detailLevel; ++i)
        {
            float xP1 = radius * Mathf.Cos(curRad);
            float yP1 = radius * Mathf.Sin(curRad);
            Vector2 p1 = origin + new Vector2(xP1, yP1);

            curRad += incrAm;

            float xP2 = radius * Mathf.Cos(curRad);
            float yP2 = radius * Mathf.Sin(curRad);
            Vector2 p2 = origin + new Vector2(xP2, yP2);

            Debug.DrawLine(p1, p2, color);
        }
    }

    /// <summary>Does a bunch of lines casts between the points of the polygon. Returns the list of hits.</summary>
    /// <param name="origin">Center of the polygon.</param>
    /// <param name="size">Scale of the polygon.</param>
    /// <param name="rotation">Rotation of the polygon.</param>
    /// <param name="points">Offsets for the points of the polygon.</param>
    /// <param name="layerMask">LayerMask to check on.</param>
    public static RaycastHit2D[] PolygonCast(Vector2 origin, Vector2 size, float rotation, Vector2[] points, int layerMask)
    {
        // Return list
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        // Iterate over the points
        for (int i = 0; i < points.Length; ++i)
        {
            // Create a line from the current point to the next point
            // If the current point is the last, loop back to the first
            Vector2 start = ApplyTransformToPoint(origin, size, rotation, points[i]);
            Vector2 end;
            if (i == points.Length - 1)
            {
                end = ApplyTransformToPoint(origin, size, rotation, points[0]);
            }
            else
            {
                end = ApplyTransformToPoint(origin, size, rotation, points[i + 1]);
            }

            // Do a line cast with the start and end points
            // Add it to the list if there is a hit
            RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawLine(start, end);
            if (hit)
            {
                Debug.Log("Hit " + hit.transform.name + " at " + hit.point);
                hits.Add(hit);
            }
        }

        return hits.ToArray();
    }



    /// <summary>Applies the origin (0,0) position, scale/size, and rotation to the given point</summary>
    private static Vector2 ApplyTransformToPoint(Vector2 origin, Vector2 size, float rotation, Vector2 point)
    {
        Vector2 scaledPoint = Vector2.Scale(point, size);
        Vector2 rotatedPoint = Quaternion.Euler(0.0f, 0.0f, rotation) * scaledPoint;
        Vector2 translatedPoint = origin + rotatedPoint;
        return translatedPoint;
    }
}
