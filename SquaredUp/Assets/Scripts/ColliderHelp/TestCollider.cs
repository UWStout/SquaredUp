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
    /// Returns an available spot which holds if a spot was found (True) or not (False) and
    /// the location the available spot was found</summary>
    /// <param name="data">Shape of collider to turn into</param>
    /// <param name="size">The actual size of the collider</param>
    public AvailableSpot CheckIfColliderWillHitWall(ShapeData data, Vector3 size)
    {
        // Colored wall layer mask
        LayerMask colorWallLayerMask = GetCurrentColoredWallLayerMask();

        RaycastHit2D hit = new RaycastHit2D();
        Vector2 roundedPos = RoundPositionToHalfInts(transform.position);

        // Physics casts don't play well with negatives sizes, so fix that
        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));

        // Test in 5 spots to try and see if the player can be pushed to those spots
        Vector2 curPos = roundedPos;
        for (int i = 0; i < 5; ++i)
        {
            // Turn on the test collider of the given type see if there is a collision with a wall
            switch (data.ColliderShape)
            {
                // BoxCollider2D
                case ShapeData.ColliderType.BOX:
                    // Testing
                    hit = PhysicsDebugging.BoxCast(curPos, size, 0, transform.up, 0, colorWallLayerMask);
                    // End Testing

                    //hit = Physics2D.BoxCast(roundedPos, size, 0, transform.up, 0, colorWallLayerMask);
                    break;
                // CircleCollider2D
                case ShapeData.ColliderType.CIRCLE:
                    hit = Physics2D.CircleCast(curPos, size.x * 0.5f, transform.up, 0, colorWallLayerMask);
                    break;
                // Triangle needs to be a specific kind of polygon collider
                case ShapeData.ColliderType.TRIANGLE:
                    RaycastHit2D[] polyhits = PolygonCast(curPos, size, ShapeData.TRIANGLE_POINTS, colorWallLayerMask);
                    if (polyhits.Length > 0)
                    {
                        hit = polyhits[0];
                    }
                    break;
                default:
                    Debug.LogError("Unhandled ColliderType of '" + data.ColliderShape + "' in PlayerColliderController.cs");
                    return new AvailableSpot(false, Vector2.zero);
            }
            // If there was no hit, we found a place the player can be
            if (!hit)
            {
               // Debug.Break();
                return new AvailableSpot(true, curPos);
            }

            // Change position based on iteration
            curPos = roundedPos;
            switch (i)
            {
                case 0:
                    curPos.x += 0.5f;
                    break;
                case 1:
                    curPos.x -= 0.5f;
                    break;
                case 2:
                    curPos.y += 0.5f;
                    break;
                case 3:
                    curPos.y -= 0.5f;
                    break;
               default:
                    break;
            }
        }
        //Debug.Break();
        return new AvailableSpot(false, Vector2.zero);
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

    /// <summary>Rounds the given position's x and y values to the closest half int</summary>
    private Vector2 RoundPositionToHalfInts(Vector3 position)
    {
        float x = RoundToHalfInt(position.x);
        float y = RoundToHalfInt(position.y);
        return new Vector2(x, y);
    }

    /// <summary>Rounds the given number to the closest half int</summary>
    private float RoundToHalfInt(float value)
    {
        int valInt = Mathf.FloorToInt(value);
        float upper = (valInt + 1) - value;
        float lower = value - valInt;
        float middle = Mathf.Abs(value - (valInt + 0.5f));
        // Closet to upper int (round up/ceil)
        if (upper < lower && upper < middle)
        {
            return valInt + 1;
        }
        // Closest to lower int (round down/floor)
        else if (lower < middle)
        {
            return valInt;
        }
        // Closest to half int (round to int(value) + 0.5
        else
        {
            return valInt + 0.5f;
        }
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
