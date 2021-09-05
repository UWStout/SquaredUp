using UnityEngine;

[RequireComponent(typeof(GridMover))]
public class GridIcePushable : GridHittable
{
    private GridMover gridMover = null;

    public QuadDirection2D slideDirection { get; set; } = QuadDirection2D.none;


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
        if (slideDirection != QuadDirection2D.none)
        {
            gridMover.Move(slideDirection);
        }
    }


    public override bool Hit(GridHit hit)
    {
        // Determine which side the hitter is closer on, vertical or horizontal
        Vector2 diff = hit.hitPosition - (Vector2)transform.position;
        bool isHori = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);

        slideDirection = hit.direction.ToDirection2D(isHori).ToQuadDirection2D();
        return false;
    }


    private void OnGridCollision(Collider2D col)
    {
        slideDirection = QuadDirection2D.none;
    }
}
