using UnityEngine;

/// <summary>
/// Saves and loads data for the player's non scaling visuals.
/// </summary>
public class PlayerNonScaleVisualsSavable : SavableMonoBehav<PlayerNonScaleVisualsSavable>
{
    // Reference to the sprite renderer on the pointer
    [SerializeField] private SpriteRenderer pointerSprRend = null;

    /// <summary>
    /// Load the non scale visuals data from the serialized object and
    /// reapplies the loaded data to the visuals.
    /// </summary>
    /// <param name="serializedObj">object with the scale visuals's active state saved as data</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        PlayerNonScaleVisualsSaveData data = serializedObj as PlayerNonScaleVisualsSaveData;

        // Reapply the pointer's color
        pointerSprRend.color = data.GetPointerColor();
    }

    /// <summary>
    /// Creates and returns PlayerNonScaleVisualsSaveData holding the data for the 
    /// player's non scaling visuals.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Current color of the player's pointer
        Color pointerColor = pointerSprRend.color;

        // Return the new data
        return new PlayerNonScaleVisualsSaveData(pointerColor);
    }
}
