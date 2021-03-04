/// <summary>Interface for a Skill. Used by the SkillBase class.
/// This is what you should use to get skill information off of a gameobject with GetComponent</summary>
public interface Skill
{
    /// <summary>Gets if the skill is unlocked</summary>
    bool IsSkillUnlocked();
    /// <summary>Sets the skill to be unlocked</summary>
    void UnlockSkill();
    /// <summary>Gets the amount of states this skill has</summary>
    int GetAmountStates();
    /// <summary>Gets the name of the state with the given index</summary>
    string GetStateName(int stateIndex);
    /// <summary>Gets if the state is unlocked</summary>
    bool IsStateUnlocked(int stateIndex);
    /// <summary>Sets the state with the given index to be unlocked</summary>
    void UnlockState(int stateIndex);
    /// <summary>Activation of the skill</summary>
    void Use(int stateIndex);
}