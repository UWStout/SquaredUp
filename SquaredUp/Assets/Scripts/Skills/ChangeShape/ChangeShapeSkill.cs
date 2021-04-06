using UnityEngine;

/// <summary>Skill that allows the player to change their shape</summary>
public class ChangeShapeSkill : SkillBase<ShapeData>
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // Refernce to the player movement script
    [SerializeField] private PlayerMovement playerMoveRef = null;
    // Mesh transitioner to change the meshes shapes
    [SerializeField] private BlendTransitioner meshTransitioner = null;

    // Current state
    private int curAttemptedStateIndex;
    // Current shape
    private ShapeData.ShapeType curShape = ShapeData.ShapeType.BOX;


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

    /// <summary>Changes the player to become the shape corresponding to the given index.
    /// Index matches what is specified in the editor. If index is unknown, consider using Use(ShapeData) instead.</summary>
    public override void Use(int stateIndex)
    {
        ActivateChangeShape(stateIndex);
    }

    /// <summary>Activates the shape change to the given state</summary>
    private void ActivateChangeShape(int stateIndex)
    {
        ShapeData data = SkillData.GetData(stateIndex);
        curAttemptedStateIndex = stateIndex;
        changeFormCont.CurShapeData = data;
    }

    /// <summary>Called when an avaible spot is found in change form controller.
    /// Updates the state and plays the sound.
    /// Starts chaning the mesh</summary>
    private void OnAvailableSpotFound()
    {
        ShapeData data = SkillData.GetData(curAttemptedStateIndex);

        // If the direction affects how the shape scales or we are swapping to a new state
        if (data.DirectionAffectsScale || !IsCurrentState(curAttemptedStateIndex))
        {
            // Update the state
            UpdateCurrentState(curAttemptedStateIndex);

            // Start changing shape if we need to
            if (data.TypeOfShape != curShape)
            {
                meshTransitioner.StartChangeShape(data.TypeOfShape);
            }
            // Set the current shape to the new one
            curShape = data.TypeOfShape;
        }
    }
}
