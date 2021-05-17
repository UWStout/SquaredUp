using UnityEngine;

/// <summary>
/// Saves and loads data for the circle tilemap.
/// </summary>
public class CircleTilemapSavable : SavableMonoBehav<CircleTilemapSavable>
{
    // Reference to the circle tilemap to get and change the tile's alpha
    [SerializeField] private CircleTilemapSingleton circleTilemap = null;


    /// <summary>
    /// Load the circle tilemap's data from the serialized object and
    /// reapplies the tiles' alpha.
    /// </summary>
    /// <param name="serializedObj">object with the circle tilemap's data saved.</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        CircleTilemapSaveData data = serializedObj as CircleTilemapSaveData;

        // Apply the alpha to the circle tiles
        float targetAlpha = data.GetTargetAlpha();
        circleTilemap.TileFadeInstant(targetAlpha);
    }

    /// <summary>
    /// Creates and returns CircleTilemapSaveData holding the data for the circle tilemap.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Target alpha for the circle tilemap
        float targetAlpha = circleTilemap.GetTargetTileAlpha();

        // Return the new data
        return new CircleTilemapSaveData(targetAlpha);
    }
}
