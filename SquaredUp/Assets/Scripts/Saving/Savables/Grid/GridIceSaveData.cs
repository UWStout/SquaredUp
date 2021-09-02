using System;
using UnityEngine;

/// <summary>
/// Save data for an ice block.
/// </summary>
[Serializable]
public class GridIceSaveData
{
    public QuadDirection2D GetSlideDirection() => new Vector2(slideDir[0], slideDir[1]).ToQuadDirection2D();
    private float[] slideDir = new float[2];


    public GridIceSaveData(GridIcePushable ice)
    {
        slideDir = new float[2]
        { 
            ice.slideDirection.Vector.x,
            ice.slideDirection.Vector.y
        };
    }
}
