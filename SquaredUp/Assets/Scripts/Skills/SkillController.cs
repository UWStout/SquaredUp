using System.Collections.Generic;
using UnityEngine;

/// <summary>Manages the player's skills.
/// Skills must each be set up to their own gameobjects and made children of the gameobject
/// this script is attached to.</summary>
public class SkillController : MonoBehaviour
{
    // Unkown if this will be needed yet, stayed tuned.
    // This enum MUST exactly match the hierarchical order of the skills.
    //public enum SkillEnum { SHAPE, COLOR, ZOOM };

    // Singleton
    private static SkillController instance = null;
    public static SkillController Instance { get { return instance; } }

    // The skills that were found in the children
    private List<Skill> skills = new List<Skill>();

    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null) { instance = this; }
        else
        {
            Debug.LogError("Cannot have multiple SkillControllers in a scene");
            Destroy(this);
        }

        // Get references to all the skills
        GetSkillsFromChildren();
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
    public void UseSkills(int[] values)
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
            if (s.IsSkillUnlocked())
            {
                s.Use(values[count]);
            }
            ++count;
        }
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
