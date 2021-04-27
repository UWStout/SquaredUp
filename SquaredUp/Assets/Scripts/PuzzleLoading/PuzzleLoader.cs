using UnityEngine;

/// <summary>
/// Singleton persistent class that holds data for where the player should start and what skill states to give them.
/// </summary>
public class PuzzleLoader : MonoBehaviour
{
    // Singleton
    private static PuzzleLoader instance = null;
    public static PuzzleLoader Instance { get { return instance; } }

    // Variables to set for the player when we load the level
    private PuzzleLoadData puzzleData = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    /// <summary>Reset the variables to be passed to the player.</summary>
    public void ResetPuzzleLoader()
    {
        puzzleData = null;
    }
    /// <summary>Sets the puzzle data to the given data.</summary>
    /// <param name="data">Data for the puzzle to load.</param>
    public void SetPuzzleData(PuzzleLoadData data)
    {
        puzzleData = data;
    }
    /// <summary>Gets the data for the puzzle to be loaded.</summary>
    public PuzzleLoadData GetPuzzleData()
    {
        return puzzleData;
    }
}
