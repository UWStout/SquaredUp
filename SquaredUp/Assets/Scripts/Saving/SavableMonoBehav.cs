using UnityEngine;

/// <summary>
/// Base class for MonoBehaviors to extend if they want to be saved.
/// </summary>
/// <typeparam name="T">Must be the type of the class that is extending this class.</typeparam>
public abstract class SavableMonoBehav<T> : MonoBehaviour, ISavable where T : SavableMonoBehav<T>
{
    // Unique ID generated on Awake to be the ISavable ID
    private string savableID = "";


    // Called 0th
    // Set references
    protected virtual void Awake()
    {
        // Generate the unique savable ID for this savable
        CreateSavableID();
        // Subscribe this savable to the save load system
        SaveSystem.SubscribeToSaveLoadSystem(this);
    }
    // Called when this component is destroyed
    protected virtual void OnDestroy()
    {
        // Unsubscribe from the save load system
        SaveSystem.UnsubscribeFromSaveLoadSystem(this);
    }


    // ISavable implementation
    public string GetID()
    {
        return savableID;
    }
    public abstract void Load(object serializedObj);
    public abstract object Save();


    /// <summary>
    /// Creates a unique savable ID for this MonoBehavior using its position, type, and name.
    /// </summary>
    private void CreateSavableID()
    {
        savableID = transform.position.ToString() + (this as T).GetType().ToString() + name;
    }
}
