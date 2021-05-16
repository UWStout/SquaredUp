
/// <summary>
/// Save data for the player's skills.
/// </summary>
[System.Serializable]
public class SkillSaveData
{
    // States of the shape skill to unlock
    private int[] shapeUnlockStates = new int[0];
    // States of the color skill to unlock
    private int[] colorUnlockStates = new int[0];
    // Currently active shape skill state
    private int activeShapeState = 0;
    // Currently active color skill state
    private int activeColorState = 0;


    /// <summary>
    /// Creates save data for the skills.
    /// </summary>
    /// <param name="shapeStates">Array of indices of the unlocked shapes.</param>
    /// <param name="colorStates">Array of indices of the unlocked colors.</param>
    /// <param name="activeShape">Index of the active shape.</param>
    /// <param name="activeColor">Index of the active color.</param>
    public SkillSaveData(int[] shapeStates, int[] colorStates, int activeShape, int activeColor)
    {
        // Unlocked skill states
        shapeUnlockStates = shapeStates.Clone() as int[];
        colorUnlockStates = colorStates.Clone() as int[];
        // Active skill states
        activeShapeState = activeShape;
        activeColorState = activeColor;
    }

    /// <summary>
    /// Gets the indices of the shape states that were saved as unlocked.
    /// </summary>
    /// <returns>Array of indices of the states in the shape skill that were saved as unlocked.</returns>
    public int[] GetShapeUnlockStates()
    {
        return shapeUnlockStates.Clone() as int[];
    }
    /// <summary>
    /// Gets the indices of the color states that were saved as unlocked.
    /// </summary>
    /// <returns>Array of indices of the states in the color skill that were saved as unlocked.</returns>
    public int[] GetColorUnlockStates()
    {
        return colorUnlockStates.Clone() as int[];
    }
    /// <summary>
    /// Gets the index of the shape state that was saved as active.
    /// </summary>
    /// <returns>Index of the state in the shape skill that was saved as active.</returns>
    public int GetActiveShapeState()
    {
        return activeShapeState;
    }
    /// <summary>
    /// Gets the index of the color state that was saved as active.
    /// </summary>
    /// <returns>Index of the state in the color skill that was saved as active.</returns>
    public int GetActiveColorState()
    {
        return activeColorState;
    }
}
