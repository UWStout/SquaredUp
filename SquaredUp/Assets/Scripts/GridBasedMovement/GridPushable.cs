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
        // Determine which side the hitter is closer on, vertical or horizontal
        Transform moverTrans = hit.moverObj.transform;
        Vector2 moverPos = moverTrans.position;
        Vector2 moveSize = moverTrans.lossyScale;
        Vector2 diff = moverPos - (Vector2)transform.position;
        bool isHori = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);

        gridMover.speed = hit.speed;
        QuadDirection2D dir = hit.direction.ToDirection2D(isHori).ToQuadDirection2D();
        return gridMover.Move(dir);
    }
}
