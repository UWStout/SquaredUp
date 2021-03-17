using UnityEngine;

/// <summary>Helper for the shape change skill to quickly update the direction of the shape</summary>
[RequireComponent(typeof(ChangeShapeSkill))]
public class UpdateShapeDirection : MonoBehaviour
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // Skill to change shape
    private ChangeShapeSkill shapeSkill = null;
    // If the player is allowed to update their shape right now
    private bool allowInput = true;

    // Called when the component is enabled
    // Subscribe to events
    private void OnEnable()
    {
        InputEvents.ShapeUpdateEvent += UpdateShapeSkill;
        // Don't let the user update direction while changing
        ChangeFormController.OnAvailableSpotFound += DenyInput;
        ChangeFormController.OnFinishChangingForm += AllowInput;
    }
    // Called when the component is disabled
    // Unsubscribe from events
    private void OnDisable()
    {
        InputEvents.ShapeUpdateEvent -= UpdateShapeSkill;
        ChangeFormController.OnAvailableSpotFound -= DenyInput;
        ChangeFormController.OnFinishChangingForm -= AllowInput;
    }

    // Called 0th
    // Set references
    private void Awake()
    {
        shapeSkill = GetComponent<ChangeShapeSkill>();
    }


    // Called when the player tries to update the direction of their shape
    private void UpdateShapeSkill()
    {
        // Only update if the current state has directio affect scale
        if (shapeSkill.GetCurrentState().DirectionAffectsScale)
        {
            // Use the shape and activate the form change
            shapeSkill.Use(shapeSkill.GetCurrentStateIndex());
            changeFormCont.ActivateFormChange();
        }
    }

    /// <summary>Allows the user to try and change direction. Called when the player is done changing shape.</summary>
    private void AllowInput()
    {
        if (!allowInput)
        {
            allowInput = true;
            InputEvents.ShapeUpdateEvent += UpdateShapeSkill;
        }
    }

    /// <summary>Denys the user from trying to change direction. Called when the a space is found the player can fit in.</summary>
    private void DenyInput()
    {
        if (allowInput)
        {
            allowInput = false;
            InputEvents.ShapeUpdateEvent -= UpdateShapeSkill;
        }
    }
}
