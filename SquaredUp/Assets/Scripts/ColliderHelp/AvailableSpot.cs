using UnityEngine;

/// <summary>Holds the result of a can fit test from TestCollider script</summary>
public struct AvailableSpot
{
    /// <summary>Creates an AvailableSpot</summary>
    /// <param name="avail">If a spot was found</param>
    /// <param name="pos">Position the spot was found</param>
    public AvailableSpot(bool avail, Vector2 pos)
    {
        isAvailable = avail;
        position = pos;
    }

    private bool isAvailable;
    /// <summary>If a spot was found</summary>
    public bool Available { get { return isAvailable; } }

    private Vector2 position;
    /// <summary>Position the spot was found</summary>
    public Vector2 Position { get { return position; } }
}
