using UnityEngine;

public class UnlockSkillStateInteractable : NPCTalkInteractable
{
    [System.Serializable]
    private struct StateOfSkill {
        [SerializeField] private int skillIndex;
        public int SkillIndex { get { return skillIndex; } }
        [SerializeField] private int stateIndex;
        public int StateIndex { get { return stateIndex; } }
    }

    /// <summary>Skills to unlock. Unlocks the skill and all its default states</summary>
    [SerializeField] private int[] skillsToUnlock = new int[0];
    /// <summary>Locked states of an unlocked skill to unlock</summary>
    [SerializeField] private StateOfSkill[] statesToUnlock = new StateOfSkill[0];


    /// <summary>Unlocks the specified skills/states and displays dialogue for each skill/state unlocked</summary>
    public override void Interact()
    {
        foreach (int skillIndex in skillsToUnlock)
        {
            SkillController.Instance.UnlockSkill(skillIndex);
        }
        foreach (StateOfSkill state in statesToUnlock)
        {
            SkillController.Instance.UnlockSkillState(state.SkillIndex, state.StateIndex);
        }

        // Let the text display
        base.Interact();
    }
}
