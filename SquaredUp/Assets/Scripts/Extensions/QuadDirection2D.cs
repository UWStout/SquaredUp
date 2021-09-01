using UnityEngine;

// Private enum for direction. Assigned values for quick checking if directions are opposite
public enum eQuadDirection2D { None = -8, Left = -1, Right = 1, Down = -2, Up = 2,
    DownLeft = -3, UpRight = 3, DownRight = -4, UpLeft = 4 }

public struct QuadDirection2D
{
    // Constants
    public static readonly QuadDirection2D none = new QuadDirection2D(eQuadDirection2D.None);
    public static readonly QuadDirection2D left = new QuadDirection2D(eQuadDirection2D.Left);
    public static readonly QuadDirection2D right = new QuadDirection2D(eQuadDirection2D.Right);
    public static readonly QuadDirection2D down = new QuadDirection2D(eQuadDirection2D.Down);
    public static readonly QuadDirection2D up = new QuadDirection2D(eQuadDirection2D.Up);
    public static readonly QuadDirection2D downLeft = new QuadDirection2D(eQuadDirection2D.DownLeft);
    public static readonly QuadDirection2D upRight = new QuadDirection2D(eQuadDirection2D.UpRight);
    public static readonly QuadDirection2D downRight = new QuadDirection2D(eQuadDirection2D.DownRight);
    public static readonly QuadDirection2D upLeft = new QuadDirection2D(eQuadDirection2D.UpLeft);


    // Direction enum value of the QuadDirection2D
    private eQuadDirection2D direction;


    /// <summary>
    /// Private constructor to assign the direction enum of the QuadDirection2D.
    /// </summary>
    /// <param name="dir">Enum value to assign to the QuadDirection2D.</param>
    private QuadDirection2D(eQuadDirection2D dir)
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
    public bool IsOppositeDirection(QuadDirection2D otherDirection)
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
                case eQuadDirection2D.None:
                    return Vector2.zero;
                case eQuadDirection2D.Left:
                    return Vector2.left;
                case eQuadDirection2D.Right:
                    return Vector2.right;
                case eQuadDirection2D.Down:
                    return Vector2.down;
                case eQuadDirection2D.Up:
                    return Vector2.up;
                case eQuadDirection2D.DownLeft:
                    return new Vector2(-1, -1);
                case eQuadDirection2D.UpRight:
                    return new Vector2(1, 1);
                case eQuadDirection2D.DownRight:
                    return new Vector2(1, -1);
                case eQuadDirection2D.UpLeft:
                    return new Vector2(-1, 1);
                default:
                    Debug.LogError("Unhandled direction " + direction);
                    return Vector2.zero;
            }
        }
    }


    public static bool operator ==(QuadDirection2D a, QuadDirection2D b)
    {
        return a.direction == b.direction;
    }
    public static bool operator !=(QuadDirection2D a, QuadDirection2D b)
    {
        return !(a == b);
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return direction.ToString();
    }
}
