using UnityEngine;

/// <summary>Base type for a skill. All skills should extend this</summary>
public abstract class SkillBase<T> : MonoBehaviour, Skill where T : SkillStateData
{
    // Data for the skill and its states
    [SerializeField] private SkillData<T> skillData = null;
    public SkillData<T> SkillData { get { return skillData; } }

    /// <summary>Returns the index of the given state data. If it was not found, returns -1</summary>
    public int FindStateIndex(T state) { return skillData.FindStateIndex(state); }

    /// <summary>Gets if the skill is unlocked</summary>
    public bool IsSkillUnlocked() { return SkillData.Unlocked; }

    /// <summary>Sets the skill to be unlocked</summary>
    public void UnlockSkill() { SkillData.UnlockSkill(); }

    /// <summary>Gets the amount of states this skill has</summary>
    public int GetAmountStates() { return SkillData.GetAmountStates(); }

    /// <summary>Gets the name of the state with the given index</summary>
    public string GetStateName(int stateIndex) { return SkillData.GetData(stateIndex).name; }
    
    /// <summary>Gets if the state is unlocked</summary>
    public bool IsStateUnlocked(int stateIndex) { return SkillData.GetState(stateIndex).Unlocked; }

    /// <summary>Sets the state with the given index to be unlocked</summary>
    public void UnlockState(int stateIndex) { SkillData.GetState(stateIndex).UnlockState(); }


    /// <summary>Uses the skill and switches to the given state</summary>
    public abstract void Use(int stateIndex);

    /// <summary>Uses the skill and tries to swap to the state corresponding to the given data</summary>
    public void Use(T stateData)
    {
        int index = SkillData.FindStateIndex(stateData);
        if (index != -1) { Use(index); }
        else { Debug.LogError(stateData.name + " is not specified as a state of " + ToString()); }
    }
}
