
/// <summary>
/// Save data for the circle tilemap.
/// </summary>
[System.Serializable]
public class CircleTilemapSaveData 
{
    // Alpha value of the tiles on the circle tilemap
    private float targetAlpha = 1f;


    /// <summary>
    /// Creates save data for the circle tilemap.
    /// </summary>
    /// <param name="targetTransparency">Current alpha value of the tiles on the circle tilemap.</param>
    public CircleTilemapSaveData(float targetTransparency)
    {
        targetAlpha = targetTransparency;
    }

    /// <summary>
    /// Gets the saved alpha for the tiles on the tilemap.
    /// </summary>
    /// <returns>Saved alpha value for the tiles on the tilemap.</returns>
    public float GetTargetAlpha()
    {
        return targetAlpha;
    }
}
