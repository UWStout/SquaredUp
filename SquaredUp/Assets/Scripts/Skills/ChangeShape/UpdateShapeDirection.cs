using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeShapeSkill))]
public class UpdateShapeDirection : MonoBehaviour
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // Skill to change shape
    private ChangeShapeSkill shapeSkill = null;

    // Called when the component is enabled
    // Subscribe to events
    private void OnEnable()
    {
        InputEvents.ShapeUpdateEvent += UpdateShapeSkill;
    }
    // Called when the component is disabled
    // Unsubscribe from events
    private void OnDisable()
    {
        InputEvents.ShapeUpdateEvent -= UpdateShapeSkill;
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
        // Use the shape and activate the form change
        shapeSkill.Use(shapeSkill.CurrentStateIndex);
        changeFormCont.ActivateFormChange();
    }
}
