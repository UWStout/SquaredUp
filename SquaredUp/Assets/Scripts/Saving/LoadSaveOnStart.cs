using UnityEngine;

/// <summary>
/// Loads save data if there is any on awake.
/// </summary>
public class LoadSaveOnStart : MonoBehaviour
{
    // Called 1st
    // Initialization
    private void Start()
    {
        // Load a save if save data exists
        if (SaveManager.CheckIfPreviousSaveExists())
        {
            SaveManager.LoadGame();
        }
    }
}
