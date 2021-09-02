using UnityEngine;

public static class Direction2DExtensions
{
    public static QuadDirection2D Add(this Direction2D curDir, Direction2D otherDir)
    {
        // If current direction is none
        if (curDir == Direction2D.none)
        {
            return otherDir.ToQuadDirection2D();
        }
        // If other direction is none
        if (otherDir == Direction2D.none)
        {
            return curDir.ToQuadDirection2D();
        }

        // Neither direction is none
        // If they are the same direction
        if (curDir == otherDir)
        {
            return curDir.ToQuadDirection2D();
        }
        // If they are opposite directions
        if (curDir.IsOppositeDirection(otherDir))
        {
            return QuadDirection2D.none;
        }
        // They are orthogonal
        // Left
        if (curDir == Direction2D.left)
        {
            if (otherDir == Direction2D.up)
            {
                return QuadDirection2D.upLeft;
            }
            if (otherDir == Direction2D.down)
            {
                return QuadDirection2D.downLeft;
            }
        }
        // Right
        if (curDir == Direction2D.right)
        {
            if (otherDir == Direction2D.up)
            {
                return QuadDirection2D.upRight;
            }
            if (otherDir == Direction2D.down)
            {
                return QuadDirection2D.downRight;
            }
        }
        // Up
        if (curDir == Direction2D.up)
        {
            if (otherDir == Direction2D.left)
            {
                return QuadDirection2D.upLeft;
            }
            if (otherDir == Direction2D.right)
            {
                return QuadDirection2D.upRight;
            }
        }
        // Down
        if (curDir == Direction2D.down)
        {
            if (otherDir == Direction2D.left)
            {
                return QuadDirection2D.downLeft;
            }
            if (otherDir == Direction2D.right)
            {
                return QuadDirection2D.downRight;
            }
        }

        Debug.LogError("Unhandled conditions in Direction2D");
        return QuadDirection2D.none;
    }
    public static QuadDirection2D ToQuadDirection2D(this Direction2D curDir)
    {
        if (curDir == Direction2D.left)
        {
            return QuadDirection2D.left;
        }
        else if (curDir == Direction2D.right)
        {
            return QuadDirection2D.right;
        }
        else if (curDir == Direction2D.up)
        {
            return QuadDirection2D.up;
        }
        else if (curDir == Direction2D.down)
        {
            return QuadDirection2D.down;
        }
        else // if (curDir == Direction2D.none)
        {
            return QuadDirection2D.none;
        }
    }
}
