using UnityEngine;

/// <summary>Added to the player to initialize their position and unlocked states when loading in from a chosen puzzle.</summary>
public class PlayerInitializer : MonoBehaviour
{
    // Called 1st
    // Inititialization
    private void Start()
    {
        // Test to make sure there is a puzzle loader
        PuzzleLoader puzzleLoader = PuzzleLoader.Instance;
        if (puzzleLoader == null)
        {
            return;
        }
        // Test to see if we have a puzzle we are trying to load into
        PuzzleLoadData data = puzzleLoader.GetPuzzleData();
        if (data == null)
        {
            return;
        }

        // Move the player
        Vector3 pos = data.StartPosition;
        if (pos != Vector3.negativeInfinity)
        {
            PlayerMovement.Instance.GetComponent<GridMover>().SetPosition(pos);
        }

        // Unlock their shape states
        int[] shapeUnlockStates = data.ShapeUnlockStates;
        foreach (int state in shapeUnlockStates)
        {
            SkillController.Instance.UnlockSkillState(SkillController.SkillEnum.Shape, state);
        }

        // Unlock their color states
        int[] colorUnlockStates = data.ColorUnlockStates;
        foreach (int state in colorUnlockStates)
        {
            SkillController.Instance.UnlockSkillState(SkillController.SkillEnum.Color, state);
        }
    }
}
