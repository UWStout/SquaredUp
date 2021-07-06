using UnityEngine;

/// <summary>
/// Describes a direction (left, right, down, or up).
/// </summary>
public struct Direction2D
{
    // Private enum for direction. Assigned values for quick checking if directions are opposite
    private enum eDirection { None = -4, Left = -1, Right = 1, Down = -2, Up = 2 }


    // Constants
    public static readonly Direction2D none = new Direction2D(eDirection.None);
    public static readonly Direction2D left = new Direction2D(eDirection.Left);
    public static readonly Direction2D right = new Direction2D(eDirection.Right);
    public static readonly Direction2D down = new Direction2D(eDirection.Down);
    public static readonly Direction2D up = new Direction2D(eDirection.Up);


    // Direction enum value of the Direction2D
    private eDirection direction;


    /// <summary>
    /// Private constructor to assign the direction enum of the Direction2D.
    /// </summary>
    /// <param name="dir">Enum value to assign to the Direction2D.</param>
    private Direction2D(eDirection dir)
    {
        direction = dir;
    }


    /// <summary>
    /// Returns true if the given direction is opposite to this direction.
    /// Left and right are opposites. Down and up are opposites.
    /// No direction is opposite to none.
    /// </summary>
    /// <param name="otherDirection">Direction to check if it is opposite to the current direction.</param>
    /// <returns>If the given direction is opposite to the current direction.</returns>
    public bool IsOppositeDirection(Direction2D otherDirection)
    {
        return (int)(direction) + (int)(otherDirection.direction) == 0;
    }
    /// <summary>
    /// Gets the vector associated with the current direction.
    /// </summary>
    public Vector2 Vector
    {
        get
        {
            switch (direction)
            {
                case eDirection.None:
                    return Vector2.zero;
                case eDirection.Left:
                    return Vector2.left;
                case eDirection.Right:
                    return Vector2.right;
                case eDirection.Down:
                    return Vector2.down;
                case eDirection.Up:
                    return Vector2.up;
                default:
                    Debug.LogError("Unhandled direction " + direction);
                    return Vector2.zero;
            }
        }
    }
}