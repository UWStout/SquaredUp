using UnityEngine;

[RequireComponent(typeof(GridMover))]
public class TestGridMover : MonoBehaviour
{
    private GridMover gridMover = null;


    private void Awake()
    {
        gridMover = GetComponent<GridMover>();
    }
    private void Update()
    {
        Direction2D horiDir = Direction2D.none;
        Direction2D vertDir = Direction2D.none;

        float horiInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horiInput) == 1.0f)
        {
            horiDir = new Vector2(horiInput, 0).ToDirection2D();
        }
        if (Mathf.Abs(vertInput) == 1.0f)
        {
            vertDir = new Vector2(0, vertInput).ToDirection2D();
        }

        QuadDirection2D moveDir = horiDir.Add(vertDir);
        if (moveDir != QuadDirection2D.none)
        {
            gridMover.Move(moveDir);
        }
    }
}
