using UnityEngine;

/// <summary>Skill that allows the player to change their shape</summary>
public class ChangeShapeSkill : SkillBase<ShapeData>
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // Mesh transitioner to change the meshes shapes
    [SerializeField] private BlendTransitioner meshTransitioner = null;

    public static ChangeShapeSkill instance { get; private set; }

    // Current state attempted
    private int curAttemptedStateIndex;
    // Current shape
    private ShapeData.ShapeType curShape = ShapeData.ShapeType.BOX;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Called 1st
    // Initialization
    private void Start()
    {
        Use(0);
    }
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
        ShapeData prevData = GetCurrentState();
        bool allowChange = true;
        // Check if we are currently restricting the change of shape
        if (prevData.HasShapeChangeBehavior)
        {
            if (prevData.ShapeChangeBehave.RestrictChange)
            {
                allowChange = false;
            }
        }
        ShapeData data = SkillData.GetData(stateIndex);
        curAttemptedStateIndex = stateIndex;
        // Change shape
        if (allowChange)
        {
            changeFormCont.CurShapeData = data;
        }
        // Fail to change shape if the shape is not the previous data
        else if (data != prevData)
        {
            changeFormCont.FailToChange(data, transform.position);
        }
    }

    /// <summary>Sets the active state to the given state without using the skill.
    /// Should only be used for loading save data.
    /// Also sets the attempted state and current shape type to the
    /// state information associated with the specified state.</summary>
    public override void FakeUse(int stateIndex)
    {
        base.FakeUse(stateIndex);
        ShapeData data = SkillData.GetData(stateIndex);
        curAttemptedStateIndex = stateIndex;
        curShape = data.TypeOfShape;
        changeFormCont.CurShapeData = data;
    }

    /// <summary>Called when an avaible spot is found in change form controller.
    /// Updates the state and plays the sound.
    /// Starts chaning the mesh</summary>
    private void OnAvailableSpotFound()
    {
        ShapeData prevData = GetCurrentState();
        ShapeData data = SkillData.GetData(curAttemptedStateIndex);

        // If the direction affects how the shape scales or we are swapping to a new state
        if (data.DirectionAffectsScale || !IsCurrentState(curAttemptedStateIndex))
        {
            // Update the state
            UpdateCurrentState(curAttemptedStateIndex);

            // Start changing shape if we need to
            if (data.TypeOfShape != curShape)
            {
                // Start updating the mesh
                meshTransitioner.StartChangeShape(data.TypeOfShape);

                // Check if we need to invoke the shape change behaviors
                if (prevData.HasShapeChangeBehavior)
                {
                    // Call change from on the old data
                    prevData.ShapeChangeBehave.ChangeFrom();
                }
                if (data.HasShapeChangeBehavior)
                {
                    // Call change to on the new data
                    data.ShapeChangeBehave.ChangeTo();
                }
            }
            // Set the current shape to the new one
            curShape = data.TypeOfShape;
        }
    }
}
