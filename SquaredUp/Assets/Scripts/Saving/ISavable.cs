
/// <summary>
/// Interface to attach to something we want to save and load some data for.
/// </summary>
public interface ISavable
{
    /// <summary>
    /// Returns a unique ID that persists between sessions.
    /// </summary>
    /// <returns>Unique persistent ID.</returns>
    string GetID();
    /// <summary>
    /// Saves the data by returning a Serializable object holding that data.
    /// </summary>
    /// <returns>Serializable object with save data.</returns>
    object Save();
    /// <summary>
    /// Loads previously saved data from the given object where
    /// the given object is data saved previously by this ISavable.
    /// </summary>
    /// <param name="serializedObj">Data previously saved by this ISavable.</param>
    void Load(object serializedObj);
}
