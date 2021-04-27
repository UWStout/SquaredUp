using UnityEngine;

/// <summary>Non singleton puzzle loader to use in the main menu to maintain references.</summary>
public class MenuPuzzleLoader : MonoBehaviour
{
    /// <summary>Reset the variables to be passed to the player.</summary>
    public void ResetPuzzleLoader()
    {
        PuzzleLoader.Instance.ResetPuzzleLoader();
    }
    /// <summary>Sets the puzzle data to the given data.</summary>
    /// <param name="data">Data for the puzzle to load.</param>
    public void SetPuzzleData(PuzzleLoadData data)
    {
        PuzzleLoader.Instance.SetPuzzleData(data);
    }
}
