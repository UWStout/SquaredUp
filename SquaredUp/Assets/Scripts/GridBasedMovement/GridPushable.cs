using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMover))]
public class GridPushable : GridHittable
{
    private GridMover gridMover = null;


    private void Awake()
    {
        gridMover = GetComponent<GridMover>();
    }


    public override bool Hit(GridHit hit)
    {
        Vector2 diff = hit.hitPosition - (Vector2)transform.position;
        // Scale the difference by the mover's size. The bigger magnitude of the result (x or y) is closer
        Vector2 distComparisonV = diff / transform.lossyScale;
        bool isHori = Mathf.Abs(distComparisonV.x) > Mathf.Abs(distComparisonV.y);

        gridMover.speed = hit.speed;
        QuadDirection2D dir = hit.direction.ToDirection2D(isHori).ToQuadDirection2D();
        return gridMover.Move(dir);
    }
}
