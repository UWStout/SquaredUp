using System.Collections.Generic;
using UnityEngine;

/// <summary>Holds data for a skill: which states are unlocked and which aren't. Also if the skill if unlocked</summary>
/// <typeparam name="T">Kind of skill data being used</typeparam>
[System.Serializable]
public class SkillData<T> where T : SkillStateData
{
    /// <summary>Holds information for a state. If its unlocked and its data</summary>
    [System.Serializable]
    public class LockableState
    {
        // If the this state is unlocked yet
        [SerializeField] private bool isUnlocked;
        public bool Unlocked { get { return isUnlocked; } }
        // Data for the state
        [SerializeField] private T data;
        public T Data { get { return data; } }

        /// <summary>Unlocks this state</summary>
        public void UnlockState() { isUnlocked = true; }
    }

    // The states this skill has
    [SerializeField] private List<LockableState> states;

    // If the skill is unlocked
    [SerializeField] private bool isUnlocked = false;
    public bool Unlocked { get { return isUnlocked; } }

    /// <summary>Gets the state(data and isUnlock) with the given index</summary>
    public LockableState GetState(int index) { return states[index]; }
    /// <summary>Gets the data with the given index</summary>
    public T GetData(int index) { return states[index].Data; }
    /// <summary>Gets the amount of states of this skill</summary>
    public int GetAmountStates() { return states.Count; }
    /// <summary>Unlocks the skills</summary>
    public void UnlockSkill() { isUnlocked = true; }

    /// <summary>Returns the index of the given state data. If it was not found, returns -1</summary>
    public int FindStateIndex(T stateData) { return states.FindIndex(lockState => lockState.Data == stateData); }
}
