using UnityEngine;

/// <summary>
/// Saves and loads the player's data.
/// </summary>
public class PlayerSavable : SavableMonoBehav<PlayerSavable>
{
    // Player's movement transform
    [SerializeField] private Transform playerMovement = null;

    
    /// <summary>
    /// Load the player data from the serialized object and
    /// reapply the loaded data to the active player.
    /// </summary>
    /// <param name="serializedObj">object with the player's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast to the correct data type
        PlayerSaveData data = serializedObj as PlayerSaveData;

        // Put the player back to their previous position
        playerMovement.position = data.GetPosition();
    }

    /// <summary>
    /// Creates and returns player save data.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        PlayerSaveData data = new PlayerSaveData(playerMovement.position);
        return data;
    }
}
