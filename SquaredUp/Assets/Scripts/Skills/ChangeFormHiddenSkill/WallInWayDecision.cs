using UnityEngine;

/// <summary>ChangeFormController helper class.
/// Detects a wall and finds a midpoint to avoid the wall from the given position to the target position.</summary>
public class WallInWayDecision
{
    // If a wall was found in the way.
    private bool wasWallInWay = false;
    public bool WasWallInWay { get { return wasWallInWay; } }
    // The midpoint generated to avoid the wall.
    private Vector3 midpoint = Vector3.zero;
    public Vector3 Midpoint { get { return midpoint; } }

    /// <summary>Constructs a WallInWayDecision.
    /// Checks right away if there is a wall in the way and if there is, generates a midpoint
    /// such that traversing start -> mid -> target is clear.</summary>
    /// <param name="testColRef">Reference to the TestCollider script.</param>
    /// <param name="startPos">Start position to move from.</param>
    /// <param name="targetPos">Target position to move to.</param>
    public WallInWayDecision(TestCollider testColRef, Vector3 startPos, Vector3 targetPos)
    {
        // Do a physics cast to see if we can go directly to the target point
        wasWallInWay = testColRef.CheckIfLineWillHitWall(startPos, targetPos);
        if (wasWallInWay)
        {
            // If a wall was in the way, get the midpoin that would make movement valid
            midpoint = CreateMidpoint(testColRef, startPos, targetPos);
        }
    }

    ///<summary>Creates a valid midpoint that avoid walls.</summary>
    /// <param name="testColRef">Reference to the TestCollider script.</param>
    /// <param name="startPos">Start position to move from.</param>
    /// <param name="targetPos">Target position to move to.</param>
    private Vector3 CreateMidpoint(TestCollider testColRef, Vector3 startPos, Vector3 targetPos)
    {
        Vector3 xMid = new Vector3(startPos.x, targetPos.y);
        Vector3 yMid = new Vector3(targetPos.x, startPos.y);

        // If the x direction is clear
        if (CheckMidpointClear(testColRef, startPos, xMid, targetPos))
        {
            return xMid;
        }
        // If the y direction is clear
        if (CheckMidpointClear(testColRef, startPos, yMid, targetPos))
        {
            return yMid;
        }
        // If we can go neither, something is amis
        else
        {
            Debug.LogError("Error: Can change form in neither direction. This boolean logic or AvailableSpot detection may be flawed.");
            return targetPos;
        }
    }

    /// <summary>Checks if the target midpoint is a valid midpoint.</summary>
    /// <param name="testColRef">Reference to the TestCollider script.</param>
    /// <param name="startPos">Start position to move from.</param>
    /// <param name="targetMidPoint">Midpoint to check for validity.</param>
    /// <param name="targetPos">Target position to move to.</param>
    /// <returns></returns>
    private bool CheckMidpointClear(TestCollider testColRef, Vector3 startPos, Vector3 targetMidPoint, Vector3 targetPos)
    {
        bool isClear = !testColRef.CheckIfLineWillHitWall(startPos, targetMidPoint);
        if (isClear)
        {
            isClear = !testColRef.CheckIfLineWillHitWall(targetMidPoint, targetPos);
        }
        return isClear;
    }
}
