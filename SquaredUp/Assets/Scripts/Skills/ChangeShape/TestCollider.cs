using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    // Reference to the ChangeColorSkill
    [SerializeField] private ChangeColorSkill changeColorSkillRef = null;
    // Wall sorting layers names
    [SerializeField] private string wallLayerName = "Wall";
    [SerializeField] private string[] coloredWallLayerNames = new string[0];
    // Values of the layer masks
    private int wallLayerValue = 0;
    private int[] coloredWallLayerValues = new int[0];


    // Called 1st
    // Initialization
    private void Start()
    {
        // Convert the given names to layer
        wallLayerValue = 1 << LayerMask.NameToLayer(wallLayerName);

        coloredWallLayerValues = new int[coloredWallLayerNames.Length];
        int count = 0;
        foreach (string colWallName in coloredWallLayerNames)
        {
            coloredWallLayerValues[count] = 1 << LayerMask.NameToLayer(colWallName);
            ++count;
        }
    }

    /// <summary>Checks if the type of type of collider given will hit any unpassable walls if changed to.
    /// Returns true if a hit is found. False if no hits are found</summary>
    /// <param name="data">Shape of collider to turn into</param>
    /// <param name="size">The actual size of the collider</param>
    public bool CheckIfColliderWillHitWall(ShapeData data, Vector3 size)
    {
        // Colored wall layer mask
        LayerMask colorWallLayerMask = GetCurrentColoredWallLayerMask();

        RaycastHit2D hit;
        // Turn on the test collider of the given type see if there is a collision with a wall
        switch (data.ColliderShape)
        {
            // BoxCollider2D
            case ShapeData.ColliderType.BOX:
                hit = Physics2D.BoxCast(transform.position, size, transform.eulerAngles.y, transform.up, 0, colorWallLayerMask);
                return hit;
            // CircleCollider2D
            case ShapeData.ColliderType.CIRCLE:
                hit = Physics2D.CircleCast(transform.position, size.x * 0.5f, transform.up, 0, colorWallLayerMask);
                return hit;
            // Triangle needs to be a specific kind of polygon collider
            case ShapeData.ColliderType.TRIANGLE:
                RaycastHit2D[] hits = PolygonCast(transform.position, size, ShapeData.TRIANGLE_POINTS, colorWallLayerMask);
                if (hits.Length > 0)
                {
                    return hits[0];
                }
                break;
            default:
                Debug.LogError("Unhandled ColliderType of '" + data.ColliderShape + "' in PlayerColliderController.cs");
                return false;
        }
        return false;
    }

    /// <summary>Creates a LayerMask based off of what color the player is</summary>
    private LayerMask GetCurrentColoredWallLayerMask()
    {
        int layerMaskVal = wallLayerValue;
        int passableIndex = changeColorSkillRef.GetPassableWallColorIndex();

        for (int i = 0; i < coloredWallLayerValues.Length; ++i)
        {
            if (i != passableIndex)
            {
                layerMaskVal = layerMaskVal | coloredWallLayerValues[i];
            }
        }

        return layerMaskVal;
    }

    /// <summary>Does a bunch of lines casts between the points of the polygon. Returns the list of hits</summary>
    /// <param name="origin">Center of the polygon</param>
    /// <param name="size">Scale of the polygon</param>
    /// <param name="points">Offsets for the points of the polygon</param>
    /// <param name="layerMask">LayerMask to check on</param>
    private RaycastHit2D[] PolygonCast(Vector2 origin, Vector2 size, Vector2[] points, int layerMask)
    {
        // Return list
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        // Iterate over the points
        for (int i = 0; i < points.Length; ++i)
        {
            // Create a line from the current point to the next point
            // If the current point is the last, loop back to the first
            Vector2 start = ApplyTransformToPoint(origin, size, points[i]);
            Vector2 end;
            if (i == points.Length - 1)
            {
                end = ApplyTransformToPoint(origin, size, points[0]);
            }
            else
            {
                end = ApplyTransformToPoint(origin, size, points[1]);
            }

            // Do a line cast with the start and end points
            // Add it to the list if there is a hit
            RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
            if (hit)
            {
                hits.Add(hit);
            }
        }

        return hits.ToArray();
    }
    
    /// <summary>Applies the origin (0,0) position and scale/size to the given point</summary>
    private Vector2 ApplyTransformToPoint(Vector2 origin, Vector2 size, Vector2 point)
    {
        return new Vector2(origin.x + point.x * size.x, origin.y + point.y * size.y);
    }

    /// <summary>Prints the hierarchy to get to the given child</summary>
    private void PrintHierarchy(Transform child)
    {
        string str = child.name;
        while (child.parent != null)
        {
            str += "<-" + child.parent.name;
            child = child.parent;
        }
        Debug.Log(str);
    }
}
