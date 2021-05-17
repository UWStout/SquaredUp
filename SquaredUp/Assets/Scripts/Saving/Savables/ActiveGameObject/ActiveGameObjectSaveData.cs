
/// <summary>
/// Save data for a gameobject's active state.
/// </summary>
[System.Serializable]
public class ActiveGameObjectSaveData
{
    // If the gameobject is active
    private bool isActive = false;

    
    /// <summary>
    /// Creates save data for a gameobject's active state.
    /// </summary>
    /// <param name="active">If the game object is currently active.</param>
    public ActiveGameObjectSaveData(bool active)
    {
        isActive = active;
    }
    
    /// <summary>
    /// Gets if the gameobject was saved as active (true) or inactive (false).
    /// </summary>
    /// <returns>Saved state of the gameobject.</returns>
    public bool GetWasActive()
    {
        return isActive;
    }
}
