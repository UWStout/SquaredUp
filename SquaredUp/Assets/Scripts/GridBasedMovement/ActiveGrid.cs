using UnityEngine;

public class ActiveGrid : SingletonMonoBehav<ActiveGrid>
{
    public float GetTileSize() => tileSize;
    [SerializeField] [Min(0.00001f)] private float tileSize = 1.0f;

    public LayerMask GetWallLayerMask() => gridWallLayerMask;
    [SerializeField] private LayerMask gridWallLayerMask = 1;


    /// <summary>
    /// Returns a position on the grid closest to the given position
    /// for an object with the given size (x, y) and rotation (z).
    /// Currently only works if the tile size is 1.
    /// </summary>
    /// <param name="position">Original position.</param>
    /// <param name="size">Size of the objec to place on the grid.</param>
    /// <param name="rotationAngle">Angle of the object to place on the grid.</param>
    /// <returns>Position on the grid.</returns>
    public Vector2 CastToGridPosition(Vector2 position, Vector2 size, float rotationAngle)
    {
        Vector2 rotSize = size.Rotate(rotationAngle);
        rotSize = new Vector2(Mathf.Abs(rotSize.x), Mathf.Abs(rotSize.y));
        
        Vector2Int rotSizeInt = new Vector2Int(Mathf.RoundToInt(rotSize.x), Mathf.RoundToInt(rotSize.y));
        Vector2 offset = Vector2.zero;
        if (rotSizeInt.x % 2 != 0)
        {
            offset.x = 0.5f;
        }
        if (rotSizeInt.y % 2 != 0)
        {
            offset.y = 0.5f;
        }

        Vector2 roundedPos = new Vector2Int(Mathf.RoundToInt(position.x - 0.01f), Mathf.RoundToInt(position.y - 0.01f));
        return roundedPos + offset;
    }
}
