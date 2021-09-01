using UnityEngine;

[RequireComponent(typeof(GridMover))]
public class GridIcePushable : GridHittable
{
    private GridMover gridMover = null;

    private QuadDirection2D slideDir = QuadDirection2D.none;


    private void Awake()
    {
        gridMover = GetComponent<GridMover>();
    }
    private void OnEnable()
    {
        gridMover.OnGridCollision += OnGridCollision;
    }
    private void OnDisable()
    {
        gridMover.OnGridCollision -= OnGridCollision;
    }
    private void Update()
    {
        if (slideDir != QuadDirection2D.none)
        {
            gridMover.Move(slideDir);
        }
    }


    public override bool Hit(GridHit hit)
    {
        // Determine which side the hitter is closer on, vertical or horizontal
        Vector2 diff = hit.moverPosition - (Vector2)transform.position;
        bool isHori = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);

        slideDir = hit.direction.ToDirection2D(isHori).ToQuadDirection2D();
        return false;
    }


    private void OnGridCollision(Collider2D col)
    {
        slideDir = QuadDirection2D.none;
    }
}
