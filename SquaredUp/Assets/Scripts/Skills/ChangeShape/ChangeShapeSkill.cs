using System.Collections;
using UnityEngine;

/// <summary>Skill that allows the player to change their shape</summary>
public class ChangeShapeSkill : SkillBase<ShapeData>
{
    // References
    // Controller for changing the player's form
    [SerializeField] private ChangeFormController changeFormCont = null;
    // Mesh Filters that will have their mesh changed to the shape being changed to
    [SerializeField] private MeshFilter[] playerMeshFilterRefs = null;
    // Refernce to the player movement script
    [SerializeField] private PlayerMovement playerMoveRef = null;
    // SFX for shape transformation
    [SerializeField] private AudioSource transformShapeSound;

    // Coroutine variables for how fast to change the shape and when we are close enough
    [SerializeField] private float changeSpeed = 0.01f;
    // If the coroutine is finished
    private bool changeShapeCoroutFin = true;
    // Refrence to the coroutine running
    private Coroutine changeShapeCorout = null;

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
        foreach (MeshFilter filter in playerMeshFilterRefs)
        {
            filter.mesh.vertices = squareData.ShapeVertices;
        }
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
            StartChangeShape(data.ShapeVertices);
        }
    }

    /// <summary>Starts smoothly changing the shape of the mesh to have the given vertices</summary>
    /// <param name="targetVertices">Target vertices</param>
    private void StartChangeShape(Vector3[] targetVertices)
    {
        // If there is an ongoing coroutine, stop it
        if (!changeShapeCoroutFin)
        {
            StopCoroutine(changeShapeCorout);
        }
        // Start a new coroutine
        changeShapeCorout = StartCoroutine(ChangeShapeCoroutine(targetVertices));
    }

    /// <summary>Coroutine to smoothly change the shape of the mesh</summary>
    /// <param name="targetVertices">Vertices to transition mesh towards</param>
    private IEnumerator ChangeShapeCoroutine(Vector3[] targetVertices)
    {
        changeShapeCoroutFin = false;

        // The amount of lerps that will be done
        int iterations = (int) (1 / changeSpeed);
        // Create a transition helper for changing the meshes
        MeshTransitioner transitioner = new MeshTransitioner(playerMeshFilterRefs[0].mesh.vertices);
        for (int i = 0; i < iterations; ++i)
        {
            // Step
            float t = changeSpeed * i;

            // Lerp for the transition for each mesh
            for (int k = 0; k < playerMeshFilterRefs.Length; ++k) {
                Vector3[] vertices = transitioner.LerpMeshPoints(targetVertices, t);
                playerMeshFilterRefs[k].mesh.vertices = vertices;
            }

            yield return null;
        }
        // Set the variables without lerping now that we are done
        for (int k = 0; k < playerMeshFilterRefs.Length; ++k)
        {
            playerMeshFilterRefs[k].mesh.vertices = targetVertices;
        }

        // We no longer do this here, we do it in change form controller
        // Let the player move again
        //playerMoveRef.AllowMovement(true);

        changeShapeCoroutFin = true;
        yield return null;
    }
}
