
/// <summary>
/// Saves and loads data for this gameobject's active state.
/// Does not work if the desired game object to save starts as disabled.
/// </summary>
public class ActiveGameObjectSavable : SavableMonoBehav<ActiveGameObjectSavable>
{
    /// <summary>
    /// Load the skill data from the serialized object and
    /// reapply the loaded data to the associated gameobject.
    /// </summary>
    /// <param name="serializedObj">object with the gameobject's active state saved as data</param>
    public override void Load(object serializedObj)
    {
        ActiveGameObjectSaveData data = serializedObj as ActiveGameObjectSaveData;

        // Set the game object's activity
        gameObject.SetActive(data.GetWasActive());
    }

    /// <summary>
    /// Creates and returns ActiveGameObjectSaveData holding the current active state of this gameobject.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        return new ActiveGameObjectSaveData(gameObject.activeSelf);
    }
}
