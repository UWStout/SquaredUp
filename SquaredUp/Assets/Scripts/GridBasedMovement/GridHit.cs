using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridHit
{
    public Vector2 moverPosition { get; private set; }
    public QuadDirection2D direction { get; private set; }
    public float speed { get; private set; }


    public GridHit(Vector2 moverPos, QuadDirection2D fromDir, float hitSpeed)
    {
        moverPosition = moverPos;
        direction = fromDir;
        speed = hitSpeed;
    }
}
