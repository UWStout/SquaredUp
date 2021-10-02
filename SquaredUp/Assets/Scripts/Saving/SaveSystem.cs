using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>Static class to be used for saving data to binary files.</summary>
public static class SaveSystem
{
    // The objects that need to be saved. ISavables will subscribe themselves to this list
    private static List<ISavable> objectsToSave = new List<ISavable>();
    // Returns the numerable of the objects that need to be saved
    public static IEnumerable<ISavable> Savables { get { return objectsToSave; } }


    /// <summary>
    /// Adds the given savable to the list of savables.
    /// </summary>
    /// <param name="savable">Savable to be saved or loaded.</param>
    public static void SubscribeToSaveLoadSystem(ISavable savable)
    {
        objectsToSave.Add(savable);
    }
    /// <summary>
    /// Removes the given savable from the list of savables.
    /// </summary>
    /// <param name="savable">Savable to no longer be saved or loaded.</param>
    public static void UnsubscribeFromSaveLoadSystem(ISavable savable)
    {
        objectsToSave.Remove(savable);
    }

    /// <summary>
    /// For setting the folder we will be saving in.
    /// If the folder exists, we do nothing.
    /// We create a new folder with the given name.
    /// </summary>
    /// <param name="folderName">String name of the folder we will be saving in. Does not expect / before it.</param>
    public static void CreateMainSaveFolder(string folderName)
    {
        // Get the fullPath
        string fullPath = Application.persistentDataPath + "/" + folderName;
        // Delete any already existing directory
        if (Directory.Exists(fullPath))
        {
            // No need to reset the directory if it exists already.
            return;
        }
        // Create a new folder with the path
        Directory.CreateDirectory(fullPath);
    }

    /// <summary>
    /// Saves the passed in data in a binary file at the specified path.
    /// </summary>
    /// <param name="data">Data to save.</param>
    /// <param name="additionalPath">What to save the file as. Does not expect a '/'.</param>
    public static void SaveData(object data, string additionalPath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        // Location of the file
        string path = Application.persistentDataPath + '/' + additionalPath;
        // Open a connection to the file
        FileStream stream = new FileStream(path, FileMode.Create);

        // Write to the file
        formatter.Serialize(stream, data);
        // Close the connection to the file
        stream.Close();
    }
    /// <summary>
    /// Loads data from a binary file with the specified path.
    /// </summary>
    /// <param name="additionalPath">Name of the file to load. Must start with '/'.</param>
    /// <returns>Data as of the specified type.</returns>
    public static T LoadData<T>(string additionalPath)
    {
        // The attempted path
        string path = Application.persistentDataPath + '/' + additionalPath;
        // If there is a file there
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // Open a connection to the file
            FileStream stream = new FileStream(path, FileMode.Open);

            // Create data from the file
            T data = (T)formatter.Deserialize(stream);
            // Close the connection to the file
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return default;
        }
    }

    /// <summary>
    /// Checks if the given path exists.
    /// </summary>
    /// <param name="additionalPath">Additional path from the base folder.</param>
    /// <returns>True if path exists, false otherwise.</returns>
    public static bool CheckIfDataExists(string additionalPath)
    {
        // The attempted path
        string path = Application.persistentDataPath + '/' + additionalPath;
        return File.Exists(path);
    }

    /// <summary>
    /// Deletes the file at the end of the given path.
    /// </summary>
    /// <param name="additionalPath">Additional path from the base folder.</param>
    public static void DeleteData(string additionalPath)
    {
        string path = Application.persistentDataPath + '/' + additionalPath;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
