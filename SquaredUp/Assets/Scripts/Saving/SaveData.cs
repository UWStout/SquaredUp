using System.Collections.Generic;

/// <summary>
/// Data to be saved to the file.
/// </summary>
[System.Serializable]
public class SaveData 
{
    // State data for each of the ISavables stored using the ISavable's ID as the key
    private Dictionary<string, object> saveStates = new Dictionary<string, object>();
    /// <summary>
    /// Iteratable ValueCollection of the save states. Key is the ISavable's ID and value is the
    /// save data from the ISavable.
    /// </summary>
    public Dictionary<string, object>.Enumerator SavedStates { get { return saveStates.GetEnumerator(); } }

    /// <summary>
    /// Constructs SaveData from the Iteratable of the savables.
    /// Stores the data from each ISavable in the dictionary with the ISavable's ID as the key. 
    /// </summary>
    /// <param name="savables">Iteratable of desired ISavables to save.</param>
    public SaveData(IEnumerable<ISavable> savables)
    {
        foreach (ISavable singleSavable in savables)
        {
            string id = singleSavable.GetID();
            object data = singleSavable.Save();
            saveStates.Add(id, data);
        }
    }
}
