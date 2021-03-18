using System.Collections;
using UnityEngine;

/// <summary>Skill that allows the player to change their shape</summary>
public class ChangeShapeSkill : SkillBase<ShapeData>
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // Refernce to the player movement script
    [SerializeField] private PlayerMovement playerMoveRef = null;
    // SFX for shape transformation
    [SerializeField] private AudioSource transformShapeSound;
    // Mesh transitioner to change the meshes shapes
    [SerializeField] private MeshTransitioner meshTransitioner = null;

    // Where the player is currently facing
    private Vector2Int currentFacing = Vector2Int.up;
    // Current state
    private int curAttemptedStateIndex;


    // Called 1st
    // Initialize
    private void Start()
    {
        currentFacing = playerMoveRef.GetFacingDirection();
        Initialize();
        // Start off as a square
        ShapeData squareData = SkillData.GetData(0);
        meshTransitioner.SetMeshVertices(squareData.ShapeVertices);
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


    /// <summary>Initializes each ShapeData state</summary>
    private void Initialize()
    {
        for (int i = 0; i < SkillData.GetAmountStates(); ++i)
        {
            ShapeData shapeData = SkillData.GetData(i);
            shapeData.Initialize();
        }
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
        Vector2Int newFacing = playerMoveRef.GetFacingDirection();
        newFacing = new Vector2Int(Mathf.Abs(newFacing.x), Mathf.Abs(newFacing.y));

        if ((currentFacing != newFacing && data.DirectionAffectsScale) || !IsCurrentState(curAttemptedStateIndex))
        {
            // Update the state
            UpdateCurrentState(curAttemptedStateIndex);
            // Play sound
            transformShapeSound.Play();

            // Update the player's facing direction
            currentFacing = playerMoveRef.GetFacingDirection();

            // Start changing mesh
            meshTransitioner.StartChangeMesh(data.ShapeVertices);
        }
    }
}
