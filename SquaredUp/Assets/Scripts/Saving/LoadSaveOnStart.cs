using UnityEngine;

/// <summary>
/// Loads save data if there is any on awake.
/// </summary>
public class LoadSaveOnStart : MonoBehaviour
{
    public enum eLoadOption { SAVE, CHECKPOINT }
    public static eLoadOption s_currentLoadOption = eLoadOption.SAVE;


    // Called 1st
    // Initialization
    private void Start()
    {
        string temp_saveFile;
        switch (s_currentLoadOption)
        {
            case eLoadOption.SAVE:
                temp_saveFile = SaveManager.SAVE_DATA_FILE_NAME;
                break;
            case eLoadOption.CHECKPOINT:
                temp_saveFile = SaveManager.CHECKPOINT_SAVE_DATA_FILE_NAME;
                break;
            default:
                Debug.LogError($"Unhandled enum option {s_currentLoadOption} of type {typeof(eLoadOption).Name}");
                temp_saveFile = "";
                break;
        }

        // Load a save if save data exists
        if (SaveManager.CheckIfPreviousSaveExists(temp_saveFile))
        {
            SaveManager.LoadGame(temp_saveFile);
        }

        ResetLoadOption();
    }


    private static void ResetLoadOption()
    {
        s_currentLoadOption = eLoadOption.SAVE;
    }
}
