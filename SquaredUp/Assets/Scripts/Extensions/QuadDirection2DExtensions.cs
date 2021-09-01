using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuadDirection2DExtensions 
{
    public static Direction2D ToDirection2D(this QuadDirection2D curDir, bool prioritizeHorizontal = true)
    {
        if (curDir == QuadDirection2D.left)
        {
            return Direction2D.left;
        }
        else if (curDir == QuadDirection2D.right)
        {
            return Direction2D.right;
        }
        else if (curDir == QuadDirection2D.up)
        {
            return Direction2D.up;
        }
        else if (curDir == QuadDirection2D.down)
        {
            return Direction2D.down;
        }
        else if (curDir == QuadDirection2D.downLeft)
        {
            if (prioritizeHorizontal)
            {
                return Direction2D.left;
            }
            return Direction2D.down;
        }
        else if (curDir == QuadDirection2D.downRight)
        {
            if (prioritizeHorizontal)
            {
                return Direction2D.right;
            }
            return Direction2D.down;
        }
        else if (curDir == QuadDirection2D.upLeft)
        {
            if (prioritizeHorizontal)
            {
                return Direction2D.left;
            }
            return Direction2D.up;
        }
        else if (curDir == QuadDirection2D.upRight)
        {
            if (prioritizeHorizontal)
            {
                return Direction2D.right;
            }
            return Direction2D.up;
        }


        return Direction2D.none;
    }
}
