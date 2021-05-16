using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Saves and loads data for the player's skills.
/// </summary>
public class SkillSavable : SavableMonoBehav<SkillSavable>
{
    // Skill controller to unlock the saved skill states
    [SerializeField] private SkillController skillController = null;
    // Scale controller to set the sizes based on the shape and size skills
    [SerializeField] private ScaleController scaleController = null;


    /// <summary>
    /// Load the skill data from the serialized object and
    /// reapply the loaded data to the active player.
    /// </summary>
    /// <param name="serializedObj">object with the player's saved data</param>
    public override void Load(object serializedObj)
    {
        SkillSaveData data = serializedObj as SkillSaveData;

        // Give the player their unlocked skills and unlocked skill states back
        // Shapes
        int[] shapeStates = data.GetShapeUnlockStates();
        UnlockSkillStatesForPlayer(SkillController.SkillEnum.Shape, shapeStates);
        // Colors
        int[] colorStates = data.GetColorUnlockStates();
        UnlockSkillStatesForPlayer(SkillController.SkillEnum.Color, colorStates);
        // Set the active states without using the skills
        SkillBase<ShapeData> shapeSkill = skillController.GetSkill(SkillController.SkillEnum.Shape) as SkillBase<ShapeData>;
        SkillBase<ColorData> colorSkill = skillController.GetSkill(SkillController.SkillEnum.Color) as SkillBase<ColorData>;
        shapeSkill.FakeUse(data.GetActiveShapeState());
        colorSkill.FakeUse(data.GetActiveColorState());
        // Set the shape size
        scaleController.ShapeScale = shapeSkill.GetCurrentState().Scale;
    }

    /// <summary>
    /// Creates and returns SkillSaveData holding the current skill controller's information.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Determine the skill states that need to be saved
        int[] shapeUnlockStates = GetUnlockedStatesArray(SkillController.SkillEnum.Shape);
        int[] colorUnlockStates = GetUnlockedStatesArray(SkillController.SkillEnum.Color);
        // Active skill states
        int shapeActive = skillController.GetSkill(SkillController.SkillEnum.Shape).GetCurrentStateIndex();
        int colorActive = skillController.GetSkill(SkillController.SkillEnum.Color).GetCurrentStateIndex();
        // Return the created data
        return new SkillSaveData(shapeUnlockStates, colorUnlockStates, shapeActive, colorActive);
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
