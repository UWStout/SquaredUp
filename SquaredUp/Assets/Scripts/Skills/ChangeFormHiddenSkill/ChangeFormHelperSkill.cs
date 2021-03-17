using UnityEngine;

/// <summary>Helper skill that activates the form change for the player. Does not need any data.</summary>
public class ChangeFormHelperSkill : SkillBase<SizeData>
{
    // Reference to the change form controller
    [SerializeField] ChangeFormController changeFormContRef = null;

    /// <summary>Calls activate on the ChangeFormController</summary>
    public override void Use(int stateIndex)
    {
        changeFormContRef.ActivateFormChange();
    }
}
