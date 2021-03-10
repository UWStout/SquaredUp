using System.Collections.Generic;
using UnityEngine;

/// <summary>Base class for skill controllers that have skills as components of children</summary>
public abstract class SkillControllerParent : SkillControllerBase
{
    // Called 0th
    // Set references
    protected virtual void Awake()
    {
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
}
