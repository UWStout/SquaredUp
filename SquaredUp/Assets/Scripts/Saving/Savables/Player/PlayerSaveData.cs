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
    // States of the shape skill to unlock
    private int[] shapeUnlockStates = new int[0];
    // States of the color skill to unlock
    private int[] colorUnlockStates = new int[0];


    /// <summary>
    /// Create the save data for the player.
    /// </summary>
    /// <param name="position">Position of the player.</param>
    public PlayerSaveData(Vector3 position, int[] shapeStates, int[] colorStates)
    {
        x = position.x;
        y = position.y;
        z = position.z;
        shapeUnlockStates = shapeStates.Clone() as int[];
        colorUnlockStates = colorStates.Clone() as int[];
    }


    /// <summary>
    /// Gets the player's position saved in the data.
    /// </summary>
    /// <returns>Player's saved position.</returns>
    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
    /// <summary>
    /// Gets the indices of the shape states that were saved as unlocked.
    /// </summary>
    /// <returns>Array of indices of the states in the shape skill that were saved as unlocked.</returns>
    public int[] GetShapeUnlockStates()
    {
        return shapeUnlockStates.Clone() as int[];
    }
    /// <summary>
    /// Gets the indices of the color states that were saved as unlocked.
    /// </summary>
    /// <returns>Array of indices of the states in the color skill that were saved as unlocked.</returns>
    public int[] GetColorUnlockStates()
    {
        return colorUnlockStates.Clone() as int[];
    }
}
