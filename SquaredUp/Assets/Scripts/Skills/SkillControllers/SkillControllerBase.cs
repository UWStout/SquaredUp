using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Base class for controlling skills. Has a list of skills and various functions to use and unlock them</summary>
public class SkillControllerBase : MonoBehaviour
{
    // The skills that were found in the children
    protected List<Skill> skills = new List<Skill>();

    /// <summary>Uses all the unlocked skills</summary>
    /// <param name="values">Array that holds values for the state of each skill that is being used.</param>
    public void UseAllSkills(int[] values)
    {
        // Validation
        if (values.Length != skills.Count)
        {
            Debug.LogError("Amount of values and amount of skills do not match up");
        }

        int count = 0;
        // Use all unlocked skills
        foreach (Skill s in skills)
        {
            UseSkill(s, values[count]);
            ++count;
        }
    }

    /// <summary>Swaps the skill with the given index to the given state</summary>
    /// <param name="skillIndex">Index of skill to use</param>
    /// <param name="stateToSwapTo">Index of state of the skill to swap to</param>
    public void UseSkill(int skillIndex, int stateToSwapTo)
    {
        UseSkill(skills[skillIndex], stateToSwapTo);
    }
    /// <summary>Swaps the given skill to the given state</summary>
    /// <param name="skillToUse">Skill to use</param>
    /// <param name="stateToSwapTo">Index of state of the skill to swap to</param>
    private void UseSkill(Skill skillToUse, int stateToSwapTo)
    {
        skillToUse.Use(stateToSwapTo);
    }

    /// <summary>Unlocks the skill with the given index</summary>
    public void UnlockSkill(int skillIndex)
    {
        if (skillIndex < GetSkillAmount())
        {
            Skill skillToUnlock = GetSkill(skillIndex);
            if (skillToUnlock != null && !skillToUnlock.IsSkillUnlocked())
            {
                skillToUnlock.UnlockSkill();
            }
        }
    }

    /// <summary>Unlocks the state with the given index for the skill with the given index</summary>
    public void UnlockSkillState(int skillIndex, int stateIndex)
    {
        if (skillIndex < GetSkillAmount())
        {
            Skill skillInContention = GetSkill(skillIndex);
            if (skillInContention != null)
            {
                if (stateIndex < skillInContention.GetAmountStates() && !skillInContention.IsStateUnlocked(stateIndex))
                {
                    skillInContention.UnlockState(stateIndex);
                }
            }
        }
    }

    /// <summary>Gets the skill with the given index</summary>
    public Skill GetSkill(int skillIndex) { return skills[skillIndex]; }
    /// <summary>Gets the amount of skills</summary>
    public int GetSkillAmount() { return skills.Count; }
}
