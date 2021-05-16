using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Saves and loads the player's data.
/// </summary>
public class PlayerSavable : SavableMonoBehav<PlayerSavable>
{
    // Player's movement transform
    [SerializeField] private Transform playerMovement = null;
    // Skill controller to unlock the saved skill states
    [SerializeField] private SkillController skillController = null;

    
    /// <summary>
    /// Load the player data from the serialized object and
    /// reapply the loaded data to the active player.
    /// </summary>
    /// <param name="serializedObj">object with the player's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast to the correct data type
        PlayerSaveData data = serializedObj as PlayerSaveData;

        // Put the player back to their previous position
        playerMovement.position = data.GetPosition();
        // Give the player their unlocked skills and unlocked skill states back
        // Shapes
        int[] shapeStates = data.GetShapeUnlockStates();
        UnlockSkillStatesForPlayer(SkillController.SkillEnum.Shape, shapeStates);
        // Colors
        int[] colorStates = data.GetColorUnlockStates();
        UnlockSkillStatesForPlayer(SkillController.SkillEnum.Color, colorStates);
    }

    /// <summary>
    /// Creates and returns player save data.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Determine the skill states that need to be saved
        int[] shapeUnlockStates = GetUnlockedStatesArray(SkillController.SkillEnum.Shape);
        int[] colorUnlockStates = GetUnlockedStatesArray(SkillController.SkillEnum.Color);

        // Create the data
        PlayerSaveData data = new PlayerSaveData(playerMovement.position, shapeUnlockStates, colorUnlockStates);
        return data;
    }


    /// <summary>
    /// Unlocks the given states for the specified skill. Also unlocks the skill if
    /// any states exist.
    /// </summary>
    /// <param name="skillType">Type of skill to unlock states for.</param>
    /// <param name="stateIndicies">States of the specified skill to unlock.</param>
    private void UnlockSkillStatesForPlayer(SkillController.SkillEnum skillType, int[] stateIndicies)
    {
        if (stateIndicies.Length > 0)
        {
            skillController.UnlockSkill(skillType);
        }
        for (int i = 0; i < stateIndicies.Length; ++i)
        {
            skillController.UnlockSkillState(skillType, i);
        }
    }
    
    /// <summary>
    /// Creates an array of indices of the unlocked states of the given skill.
    /// </summary>
    /// <param name="skillType">Which skill to check the states of.</param>
    /// <returns>Array of indices of the unlocked states of the given skill.</returns>
    private int[] GetUnlockedStatesArray(SkillController.SkillEnum skillType)
    {
        // Create a temporary list to hold the states.
        List<int> unlockedStates = new List<int>();
        Skill skill = skillController.GetSkill(skillType);
        // Iterate over the states and check if each one is unlocked.
        for (int i = 0; i < skill.GetAmountStates(); ++i)
        {
            if (skill.IsStateUnlocked(i))
            {
                unlockedStates.Add(i);
            }
        }
        return unlockedStates.ToArray();
    } 
}
