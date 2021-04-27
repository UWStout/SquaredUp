using UnityEngine;

/// <summary>Puzzle load data to pass to the player when selecting to start from a specific puzzle.</summary>
[CreateAssetMenu(fileName = "New PuzzleLoadData", menuName = "ScriptableObjects/PuzzleLoadData")]
public class PuzzleLoadData : ScriptableObject
{
    // Start position of the player
    [SerializeField] private Vector3 startPos = Vector3.negativeInfinity;
    // States of the shape skill to unlock
    [SerializeField] private int[] shapeUnlockStates = new int[0];
    // States of the color skill to unlock
    [SerializeField] private int[] colorUnlockStates = new int[0];

    /// <summary>
    /// Start position of the player.
    /// </summary>
    public Vector3 StartPosition { get { return startPos; } }
    /// <summary>
    /// Copy of the states of the shape skill to unlock.
    /// </summary>
    public int[] ShapeUnlockStates { get { return shapeUnlockStates.Clone() as int[]; } }
    /// <summary>
    /// Copy of the states of the color skill to unlock.
    /// </summary>
    public int[] ColorUnlockStates { get { return colorUnlockStates.Clone() as int[]; } }
}
