using UnityEngine;

/// <summary>
/// Saves and loads data for the specified gameobject's active state.
/// </summary>
public class ActiveOtherGameObjectSavable : SavableMonoBehav<ActiveOtherGameObjectSavable>
{
    // GameObject to set active or inactive
    [SerializeField] private GameObject otherGameObject = null;

    /// <summary>
    /// Load the skill data from the serialized object and
    /// reapply the loaded data to the associated gameobject.
    /// </summary>
    /// <param name="serializedObj">object with the gameobject's active state saved as data</param>
    public override void Load(object serializedObj)
    {
        ActiveGameObjectSaveData data = serializedObj as ActiveGameObjectSaveData;

        // Set the game object's activity
        otherGameObject.SetActive(data.GetWasActive());
    }

    /// <summary>
    /// Creates and returns ActiveGameObjectSaveData holding the current active state of this gameobject.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        return new ActiveGameObjectSaveData(otherGameObject.activeSelf);
    }
}
