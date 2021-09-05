using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridHit
{
    public Vector2 hitPosition { get; private set; }
    public QuadDirection2D direction { get; private set; }
    public float speed { get; private set; }
    public GameObject moverObj { get; private set; }


    public GridHit(Vector2 hitPos, QuadDirection2D fromDir, float hitSpeed, GameObject moverObject)
    {
        hitPosition = hitPos;
        direction = fromDir;
        speed = hitSpeed;
        moverObj = moverObject;
    }
}
