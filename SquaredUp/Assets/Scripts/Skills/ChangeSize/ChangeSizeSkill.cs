using UnityEngine;

/// <summary>Skill that changes the size of the player</summary>
public class ChangeSizeSkill : SkillBase<SizeData>
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // SFX for size transformation
    [SerializeField] private AudioSource transformSizeSound;

    // Current state
    private int curStateIndex;


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
        curStateIndex = stateIndex;

        // Set the data in the form changer
        SizeData data = SkillData.GetData(stateIndex);
        changeFormCont.CurSizeData = data;

        // This is the last skill to be called that has to do with forms. Activate the form change
        changeFormCont.ActivateFormChange();
    }

    /// <summary>Called when an avaible spot is found in change form controller.
    /// Updates the state and plays the sound</summary>
    private void OnAvailableSpotFound()
    {
        // If it is not the current state
        if (!IsCurrentState(curStateIndex))
        {
            // Update the state
            UpdateCurrentState(curStateIndex);
            // Play sound
            transformSizeSound.Play();
        }
    }
}
