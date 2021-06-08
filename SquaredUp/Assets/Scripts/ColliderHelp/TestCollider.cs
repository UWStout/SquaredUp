using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    // Constants
    // Amount to increment for testing position
    private const float INCR_VAL = 0.125f;
    // Amount of times to iterate for testing position
    private const int ITERATIONS = 6;


    // Reference to the ChangeColorSkill
    [SerializeField] private ChangeColorSkill changeColorSkillRef = null;
    // Wall sorting layers names
    [SerializeField] private string wallLayerName = "Wall";
    [SerializeField] private string pushableLayerName = "Push";
    [SerializeField] private string[] coloredWallLayerNames = new string[0];
    // Values of the layer masks
    private int wallLayerValue = 0;
    private int pushLayerValue = 0;
    private int[] coloredWallLayerValues = new int[0];


    // Called 1st
    // Initialization
    private void Start()
    {
        // Convert the given names to layer
        wallLayerValue = 1 << LayerMask.NameToLayer(wallLayerName);
        pushLayerValue = 1 << LayerMask.NameToLayer(pushableLayerName);

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
    /// <param name="colliderType">Shape of collider to turn into</param>
    /// <param name="size">The actual size of the collider</param>
    /// <param name="rotation">Rotation of the shape.</param>
    public AvailableSpot CheckIfColliderWillHitWall(ShapeData.ShapeType colliderType, Vector3 size, float rotation)
    {
        // Colored wall layer mask
        LayerMask colorWallLayerMask = GetCurrentColoredWallLayerMask();

        bool foundHit = false;
        Vector2 roundedPos = RoundPositionToHalfInts(transform.position);

        // Physics casts don't play well with negatives sizes, so fix that
        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));

        // Test in 5 spots to try and see if the player can be pushed to those spots
        Vector2 curPos = roundedPos;
        int curIter = 0;
        for (int i = 0; i < 8 * ITERATIONS + 1; ++i)
        {
            // Turn on the test collider of the given type see if there is a collision with a wall
            switch (colliderType)
            {
                // BoxCollider2D
                case ShapeData.ShapeType.BOX:
                    // Testing
                    //foundHit = PhysicsDebugging.BoxCast(curPos, size, 0, transform.up, 0, colorWallLayerMask);
                    // End Testing
                    foundHit = Physics2D.BoxCast(curPos, size, rotation, transform.up, 0, colorWallLayerMask);
                    break;
                // CircleCollider2D
                case ShapeData.ShapeType.CIRCLE:
                    // Testing
                    //foundHit = PhysicsDebugging.CircleCast(curPos, size.x * 0.5f, transform.up, 0, colorWallLayerMask);
                    // End Testing
                    foundHit = Physics2D.CircleCast(curPos, size.x * 0.5f, transform.up, 0, colorWallLayerMask);
                    break;
                // Triangle needs to be a specific kind of polygon collider
                case ShapeData.ShapeType.TRIANGLE:
                    foundHit = false;
                    RaycastHit2D[] polyhits;
                    // Testing
                    //polyhits = PhysicsDebugging.PolygonCast(curPos, size, rotation, ShapeData.TRIANGLE_POINTS, colorWallLayerMask);
                    // End Testing
                    polyhits = PolygonCast(curPos, size, rotation, ShapeData.TRIANGLE_POINTS, colorWallLayerMask);
                    foreach (RaycastHit2D pHit in polyhits)
                    {
                        if (pHit)
                        {
                            foundHit = true;
                            break;
                        }
                    }
                    break;
                default:
                    Debug.LogError("Unhandled ColliderType of '" + colliderType + "' in PlayerColliderController.cs");
                    return new AvailableSpot(false, Vector2.zero);
            }
            //Debug.Break();
            // If there was no hit, we found a place the player can be
            if (!foundHit)
            {
                return new AvailableSpot(true, curPos);
            }

            float incAm = INCR_VAL * ((i / 8) + 1);
            curPos = ChangeTestPosition(roundedPos, curIter, incAm);
            // Change position based on iteration
            int stateTracking = i % ITERATIONS;
            if (stateTracking == 0)
            {
                ++curIter;
            }
        }
        return new AvailableSpot(false, Vector2.zero);
    }

    /// <summary>Checks if there is a wall or non-passable colored wall in a straight line from start to end.</summary>
    /// <param name="startPos">Start position of the line.</param>
    /// <param name="endPos">End position of the line.</param>
    /// <returns></returns>
    public bool CheckIfLineWillHitWall(Vector3 startPos, Vector3 endPos)
    {
        // Colored wall layer mask
        LayerMask colorWallLayerMask = GetCurrentColoredWallLayerMask();

        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, colorWallLayerMask);

        return hit;
    }

    /// <summary>Creates a LayerMask based off of what color the player is</summary>
    private LayerMask GetCurrentColoredWallLayerMask()
    {
        int layerMaskVal = wallLayerValue | pushLayerValue;
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

    /// <summary>Does a bunch of lines casts between the points of the polygon. Returns the list of hits.</summary>
    /// <param name="origin">Center of the polygon.</param>
    /// <param name="size">Scale of the polygon.</param>
    /// <param name="rotation">Rotation of the polygon.</param>
    /// <param name="points">Offsets for the points of the polygon.</param>
    /// <param name="layerMask">LayerMask to check on.</param>
    private RaycastHit2D[] PolygonCast(Vector2 origin, Vector2 size, float rotation, Vector2[] points, int layerMask)
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
            if (hit)
            {
                hits.Add(hit);
            }
        }

        return hits.ToArray();
    }
    
    /// <summary>Applies the origin (0,0) position, scale/size, and rotation to the given point</summary>
    private Vector2 ApplyTransformToPoint(Vector2 origin, Vector2 size, float rotation, Vector2 point)
    {
        Vector2 scaledPoint = Vector2.Scale(point, size);
        Vector2 rotatedPoint = Quaternion.Euler(0.0f, 0.0f, rotation) * scaledPoint;
        Vector2 translatedPoint = origin + rotatedPoint;
        return translatedPoint;
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

    /// <summary>Changes the given position to a different position based on the state</summary>
    /// <param name="originalPos">Origin position</param>
    /// <param name="stateTracking">State of the iteration</param>
    /// <param name="incrementAmount">Amount to increment the x/y by</param>
    /// <returns></returns>
    private Vector2 ChangeTestPosition(Vector2 originalPos, int stateTracking, float incrementAmount)
    {
        Vector2 curPos = originalPos;
        if (stateTracking == 0)
        {
            curPos.x += incrementAmount;
        }
        else if (stateTracking == 1)
        {
            curPos.x -= incrementAmount;
        }
        else if (stateTracking == 2)
        {
            curPos.y += incrementAmount;
        }
        else if (stateTracking == 3)
        {
            curPos.y -= incrementAmount;
        }
        else if (stateTracking == 4)
        {
            curPos.x += incrementAmount;
            curPos.y += incrementAmount;
        }
        else if (stateTracking == 5)
        {
            curPos.x += incrementAmount;
            curPos.y -= incrementAmount;
        }
        else if (stateTracking == 6)
        {
            curPos.x -= incrementAmount;
            curPos.y += incrementAmount;
        }
        else if (stateTracking == 7)
        {
            curPos.x -= incrementAmount;
            curPos.y -= incrementAmount;
        }
        return curPos;
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
