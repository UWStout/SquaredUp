using UnityEngine;

/// <summary>
/// Save data for the player.
/// </summary>
[System.Serializable]
public class PlayerSaveData
{
    // Player's position vector
    private float x = 0.0f;
    private float y = 0.0f;
    private float z = 0.0f;


    /// <summary>
    /// Create the save data for the player.
    /// </summary>
    /// <param name="position">Position of the player.</param>
    public PlayerSaveData(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }


    /// <summary>
    /// Gets the player's position saved in the data.
    /// </summary>
    /// <returns>Player's saved position.</returns>
    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
}
