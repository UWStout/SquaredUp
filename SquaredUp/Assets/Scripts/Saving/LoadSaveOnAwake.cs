using UnityEngine;

/// <summary>
/// Loads save data if there is any on awake.
/// </summary>
public class LoadSaveOnAwake : MonoBehaviour
{
    // Called 0th
    // Set references
    private void Awake()
    {
        // Load a save if save data exists
        if (SaveManager.CheckIfPreviousSaveExists())
        {
            SaveManager.LoadGame();
        }
    }
}
