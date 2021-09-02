using UnityEngine;

/// <summary>
/// Save data for the player's rotating visuals (color/shape).
/// </summary>
[System.Serializable]
public class PlayerScaleVisualsSaveData
{
    // Enum for which shape the renderer is
    private int shapeType = 0;
    // RGB values for the color the renderer is
    private float[] color = new float[3];


    /// <summary>
    /// Creates save data for the rotating visuals.
    /// </summary>
    /// <param name="type">Type of shape the player is.</param>
    /// <param name="col">Color the player is.</param>
    public PlayerScaleVisualsSaveData(ShapeData.ShapeType type, Color col)
    {
        // Cast the enum to an int
        shapeType = (int)type;
        // Save the color in an array of floats
        color[0] = col.r;
        color[1] = col.g;
        color[2] = col.b;
    }

    /// <summary>
    /// Gets the saved type of shape the player was.
    /// </summary>
    /// <returns>Saved shape the player was.</returns>
    public ShapeData.ShapeType GetShapeType()
    {
        return shapeType + ShapeData.ShapeType.BOX;
    }
    /// <summary>
    /// Gets the saved color the player was.
    /// </summary>
    /// <returns>Saved color the player was.</returns>
    public Color GetColor()
    {
        return new Color(color[0], color[1], color[2]);
    }
}
