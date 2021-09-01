using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGrid : SingletonMonoBehav<ActiveGrid>
{
    public float GetTileSize() => tileSize;
    [SerializeField] [Min(0.00001f)] private float tileSize = 1.0f;

    public LayerMask GetWallLayerMask() => gridWallLayerMask;
    [SerializeField] private LayerMask gridWallLayerMask = 1;
}
