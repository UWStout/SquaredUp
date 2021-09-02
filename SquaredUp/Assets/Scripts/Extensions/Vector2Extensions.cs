using UnityEngine;

/// <summary>
/// Extension functions for the unity Vector2 class.
/// </summary>
public static class Vector2Extensions
{
    /// <summary>
    /// Casts the current vector to left, right, down, up, or none and returns it.
    /// Does not change the current vector.
    /// </summary>
    /// <param name="rawVector">Reference to self for extension.</param>
    public static Vector2 CastToCardinal(this Vector2 rawVector)
    {
        Direction2D dir = ToDirection2D(rawVector);
        return dir.Vector;
    }
    /// <summary>
    /// Gets the closest Direction2D (none, left, right, down, up) this vector is pointed in.
    /// </summary>
    /// <param name="vector">Reference to self for extension.</param>
    /// <returns>Closest Direction2D this vector is pointed in.</returns>
    public static Direction2D ToDirection2D(this Vector2 vector)
    {
        // Vector is zero = not pointed in any direction
        if (vector.x == 0.0f && vector.y == 0.0f)
        {
            return Direction2D.none;
        }
        // Pointed more horizontally than vertically
        else if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            if (vector.x > 0)
            {
                return Direction2D.right;
            }
            else
            {
                return Direction2D.left;
            }
        }
        // Pointed more vertically than horizontally
        else
        {
            if (vector.y > 0)
            {
                return Direction2D.up;
            }
            else
            {
                return Direction2D.down;
            }
        }
    }
    public static QuadDirection2D ToQuadDirection2D(this Vector2 vector)
    {
        Direction2D dirX = new Vector2(vector.x, 0).ToDirection2D();
        Direction2D dirY = new Vector2(0, vector.y).ToDirection2D();

        return dirX.Add(dirY);
    }
    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
