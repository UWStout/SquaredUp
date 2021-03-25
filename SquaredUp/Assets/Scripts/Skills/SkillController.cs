using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages the player's skills.
/// Skills must each be set up to their own gameobjects and made children of the gameobject
/// this script is attached to.</summary>
public class SkillController : MonoBehaviour
{
    // Enum for the skills
    public enum SkillEnum { Shape, Color, Size, Form };

    // Singleton
    private static SkillController instance = null;
    public static SkillController Instance { get { return instance; } }

    // The skills that were found in the children
    private List<Skill> skills = new List<Skill>();


    // Called 0th
    // Set references
    private void Awake()
    {
        // Get references to all the skills
        GetSkillsFromChildren();

        // Set up singleton
        if (instance == null) { instance = this; }
        else
        {
            Debug.LogError("Cannot have multiple SkillControllers in a scene");
            Destroy(this);
        }
    }


    /// <summary>Helper Method. Pulls all the skills off the children</summary>
    private void GetSkillsFromChildren()
    {
        skills = new List<Skill>(transform.childCount);
        foreach (Transform child in transform)
        {
            Skill s = child.GetComponent<Skill>();
            skills.Add(s);
        }
    }

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
    /// <param name="skillEnum">Enum of skill to use</param>
    /// <param name="stateToSwapTo">Index of state of the skill to swap to</param>
    public void UseSkill(SkillEnum skillEnum, int stateToSwapTo)
    {
        UseSkill(skills[(int)skillEnum], stateToSwapTo);
    }

    /// <summary>Swaps the given skill to the given state</summary>
    /// <param name="skillToUse">Skill to use</param>
    /// <param name="stateToSwapTo">Index of state of the skill to swap to</param>
    private void UseSkill(Skill skillToUse, int stateToSwapTo)
    {
        skillToUse.Use(stateToSwapTo);
    }

    /// <summary>Unlocks the skill with the given index</summary>
    public void UnlockSkill(SkillEnum skillEnum)
    {
        int skillIndex = (int)skillEnum;
        if (skillIndex < GetSkillAmount())
        {
            Skill skillToUnlock = GetSkill(skillEnum);
            if (skillToUnlock != null && !skillToUnlock.IsSkillUnlocked())
            {
                skillToUnlock.UnlockSkill();
            }
        }
    }
    /// <summary>Unlocks the skill with the given index</summary>
    public void UnlockSkill(int skillIndex)
    {
        UnlockSkill(SkillEnum.Shape + skillIndex);
    }

    /// <summary>Unlocks the state with the given index for the skill with the given index</summary>
    public void UnlockSkillState(SkillEnum skillEnum, int stateIndex)
    {
        int skillIndex = (int)skillEnum;
        if (skillIndex < GetSkillAmount())
        {
            Skill skillInContention = GetSkill(skillEnum);
            if (skillInContention != null)
            {
                if (stateIndex < skillInContention.GetAmountStates() && !skillInContention.IsStateUnlocked(stateIndex))
                {
                    skillInContention.UnlockState(stateIndex);
                }
            }
        }
    }
    /// <summary>Unlocks the state with the given index for the skill with the given index</summary>
    public void UnlockSkillState(int skillIndex, int stateIndex)
    {
        UnlockSkillState(SkillEnum.Shape + skillIndex, stateIndex);
    }

    /// <summary>Gets the skill with the given index</summary>
    public Skill GetSkill(int skillIndex) { return skills[skillIndex]; }
    /// <summary>Gets the skill with the given index</summary>
    public Skill GetSkill(SkillEnum skillEnum) { return GetSkill((int)skillEnum); }
    /// <summary>Gets the amount of skills</summary>
    public int GetSkillAmount() { return skills.Count; }
}
