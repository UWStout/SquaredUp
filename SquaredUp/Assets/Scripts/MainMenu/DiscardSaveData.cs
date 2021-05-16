using UnityEngine;

/// <summary>
/// Used by the new game button to get rid of any previous game data.
/// </summary>
public class DiscardSaveData : MonoBehaviour
{
    /// <summary>
    /// Gets rid of the previous save data.
    /// </summary>
    public void DeletePreviousSave()
    {
        SaveManager.ResetSaveFolder();
    }
}
