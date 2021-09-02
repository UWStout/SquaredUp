using UnityEngine;

/// <summary>
/// Save data for the player's visuals that don't care about the player's scale.
/// </summary>
[System.Serializable]
public class PlayerNonScaleVisualsSaveData
{
    // Color of the pointer stored as an array of rgb
    private float[] pointerColor = new float[3];

    
    /// <summary>
    /// Creates save data for the player's visuals that are unaffected by their scale.
    /// </summary>
    /// <param name="pointCol">Color of the player's pointer.</param>
    public PlayerNonScaleVisualsSaveData(Color pointCol)
    {
        pointerColor[0] = pointCol.r;
        pointerColor[1] = pointCol.g;
        pointerColor[2] = pointCol.b;
    }

    /// <summary>
    /// Gets the saved pointer color.
    /// </summary>
    /// <returns>Color of the pointer when it was saved.</returns>
    public Color GetPointerColor()
    {
        return new Color(pointerColor[0], pointerColor[1], pointerColor[2]);
    }
}
