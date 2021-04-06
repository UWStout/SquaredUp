using UnityEngine;

/// <summary>Skill that changes the size of the player</summary>
public class ChangeSizeSkill : SkillBase<SizeData>
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;

    // Current state
    private int curAttemptedStateIndex;


    // Called when this is enabled
    // Subscribe to event
    private void OnEnable()
    {
        ChangeFormController.OnAvailableSpotFound += OnAvailableSpotFound;
    }

    // Called when this is disabled
    // Unsubscribe from events
    private void OnDisable()
    {
        ChangeFormController.OnAvailableSpotFound -= OnAvailableSpotFound;
    }


    /// <summary>Changes the player to become the size corresponding to the given index</summary>
    public override void Use(int stateIndex)
    {
        // Update current attempted state
        curAttemptedStateIndex = stateIndex;

        // Set the data in the form changer
        SizeData data = SkillData.GetData(stateIndex);
        changeFormCont.CurSizeData = data;
    }

    /// <summary>Called when an avaible spot is found in change form controller.
    /// Updates the state and plays the sound</summary>
    private void OnAvailableSpotFound()
    {
        // If it is not the current state
        if (!IsCurrentState(curAttemptedStateIndex))
        {
            // Update the state
            UpdateCurrentState(curAttemptedStateIndex);
        }
    }
}
