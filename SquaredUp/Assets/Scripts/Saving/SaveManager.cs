using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager class that acts as an instance where we can call save and load.
/// </summary>
public static class SaveManager
{
    // Constants
    // Holder name we will save the data in.
    private const string SAVE_FOLDER_NAME = "saves";
    // File we are saving the save data to.
    private const string SAVE_DATA_FILE_NAME = "saveFile.savedata";


    /// <summary>
    /// Saves the current state of the game. 
    /// </summary>
    public static void SaveGame()
    {
        // Get the active savables
        IEnumerable<ISavable> savables = SaveSystem.Savables;
        // Create save data from them
        SaveData saveData = new SaveData(savables);

        // Reset the saves folder and save the newly created SaveData
        ResetSaveFolder();
        SaveSystem.SaveData(saveData, SAVE_FOLDER_NAME + "/" + SAVE_DATA_FILE_NAME);
    }

    /// <summary>
    /// If the folder exists, we destroy it and create a new folder.
    /// Otherwise, we just create a new folder.
    /// </summary>
    public static void ResetSaveFolder()
    {
        SaveSystem.RecreateMainSaveFolder(SAVE_FOLDER_NAME);
    }

    /// <summary>
    /// Loads previously saved game data into the active savables.
    /// </summary>
    public static void LoadGame()
    {
        // Get the active savables
        IEnumerable<ISavable> savables = SaveSystem.Savables;

        // Load the previously saved data
        SaveData savedData = SaveSystem.LoadData<SaveData>(SAVE_FOLDER_NAME + "/" + SAVE_DATA_FILE_NAME);
        // If we successfully loaded the data
        if (savedData != null)
        {
            // Get the enumerator of the savable's data from the loaded SaveData
            Dictionary<string, object>.Enumerator savedObjEnumerator = savedData.SavedStates;
            do
            {
                KeyValuePair<string, object> curPair = savedObjEnumerator.Current;
                // Find the savable with the correct ID for the current pair
                foreach (ISavable singleSavable in savables)
                {
                    if (singleSavable.GetID() == curPair.Key)
                    {
                        // Load the data for that savable and break out of the inner loop
                        singleSavable.Load(curPair.Value);
                        break;
                    }
                }
                // Loop over the enumerator while it has next
            } while (savedObjEnumerator.MoveNext());
        }
        // Failed to load the previously saved data
        else
        {
            Debug.Log("No previously saved data found");
        }
    }

    /// <summary>
    /// Checks if there is an existing save file.
    /// </summary>
    /// <returns>True if there is an existing save file. False otherwise.</returns>
    public static bool CheckIfPreviousSaveExists()
    {
        return SaveSystem.CheckIfDataExists(SAVE_FOLDER_NAME + "/" + SAVE_DATA_FILE_NAME);
    }
}
