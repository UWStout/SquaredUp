using System;
using System.Collections;
using UnityEngine;

/// <summary>Acts as a controller for the skills that affect the scale of the player.
/// ActivateFormChange must be called by the last skill to activate</summary>
public class ChangeFormController : MonoBehaviour
{
    // Constants
    // Size of the cube to shrink to
    private static readonly Vector2 SHRINK_SIZE = new Vector2(1.0f / 3.0f, 1.0f / 3.0f);

    // References
    // Scale controller for the player
    [SerializeField] private ScaleController playerScaleCont = null;
    // Reference to the player collider controller
    [SerializeField] private PlayerColliderController playerColContRef = null;
    // Reference to the player movement script
    [SerializeField] private PlayerMovement playerMoveRef = null;
    // Moving portion of the player
    [SerializeField] private Transform playerMoveTrans = null;
    // Reference tot he test collider script for checking for walls in the way
    [SerializeField] private TestCollider colliderTest = null;
    // SFX for size transformation
    [SerializeField] private AudioSource transformSizeSound = null;
    // Reference to the CannotFitVisual
    [SerializeField] private CannotFitVisualController cannotFitCont = null;
    // SFX for failing transformation
    [SerializeField] private AudioSource failedTransformSound = null;

    // Coroutine variables for how fast to change the shape and when we are close enough
    [SerializeField] [Min(0.0001f)] private float changeSpeed = 1f;
    // If the coroutine is finished
    private bool changeFormCoroutFin = true;
    // Refrence to the coroutine running
    private Coroutine changeFormCorout = null;

    // If the shape data has been changed since last time
    private bool wasShapeChanged = false;
    // Rotation the shape had last (in degrees)
    private float shapeRotation = 0.0f;
    // Previous shape data
    private ShapeData prevShapeData = null;
    // Current shape data to change to
    private ShapeData curShapeData = null;
    public ShapeData CurShapeData {
        set {
            if (curShapeData != null)
            {
                wasShapeChanged = curShapeData != value;
                // Reset shape rotation if the shape was changed
                if (wasShapeChanged)
                {
                    shapeRotation = 0.0f;
                }
            }
            prevShapeData = curShapeData;
            curShapeData = value;
        }
    }

    // If the size data has been changed since last time
    private bool wasSizeChanged = false;
    // Previous size data
    private SizeData prevSizeData = null;
    // Current size to change to
    private SizeData curSizeData = null;
    public SizeData CurSizeData {
        set {
            if (curSizeData != null)
            {
                wasSizeChanged = curSizeData != value;
            }
            prevSizeData = curSizeData;
            curSizeData = value;
        }
    }

    // Events
    // Event for when an available spot is found
    public static event Action OnAvailableSpotFound;
    // Event for when the form is done changing
    public static event Action OnFinishChangingForm;


    /// <summary>Should be called after shape data and size data have been given to it.
    /// Changes shape and size to be the specified size and shape</summary>
    public void ActivateFormChange()
    {
        Vector2Int facingDir = playerMoveRef.GetFacingDirection();
        float shapeRot = GetShapeRotation(curShapeData, facingDir, wasShapeChanged);
        // Set size using facing size and size data
        Vector2Int size = curShapeData.Scale;
        if (curSizeData != null)
        { 
            size *= curSizeData.Size;
        }

        //Debug.Log("Was Size Changed: " + wasSizeChanged);
        //Debug.Log("Was Shape Changed: " + wasShapeChanged);
        //Debug.Log("PreviousShapeData: " + prevShapeData);
        //Debug.Log("CurShapeData: " + curShapeData);
        //Debug.Log("Does direciton affect scale: " + curShapeData.DirectionAffectsScale);
        //Debug.Log("Target Size: " + size);
        //Debug.Log("Current Size: " + playerScaleCont.ShapeScale);
        //Debug.Log("Target!=Size? " + (size != playerScaleCont.ShapeScale));
        //Debug.Log("ShapeRot: " + shapeRot);

        // Change if size or shape was changed or
        // if the shape's direction affects scale and the rotation has been changed because of it
        if (wasSizeChanged || wasShapeChanged ||
            (curShapeData != null && curShapeData.DirectionAffectsScale && shapeRot != shapeRotation))
        {
            // Swap the colliders
            // If the colliders couldn't be swapped, ergo could not fit, then do not swap the player's shape
            Vector2Int scaledSize = Vector2Int.Scale(size, playerScaleCont.OriginalScale);
            AvailableSpot availSpot = playerColContRef.ActivateCollider(curShapeData.TypeOfShape, scaledSize, shapeRot);
            if (availSpot.Available)
            {
                // Call the available spot found event
                OnAvailableSpotFound?.Invoke();
                // Set the shape rotation
                shapeRotation = shapeRot;
                // Start changing form
                StartChangeForm(size, availSpot.Position, curShapeData.DirectionAffectsScale);

                // They are now the current, so reset them
                wasSizeChanged = false;
                wasShapeChanged = false;
            }
            // Player could not change here, so display the error that could not change here
            else
            {
                FailToChange(curShapeData);
            }
        }
    }

    /// <summary>Display the error and sound error that the player cannot change here.</summary>
    public void FailToChange(ShapeData shapeData)
    {
        // Get the rotation from the facing direction and the current shape direction
        Vector2Int facingDir = playerMoveRef.GetFacingDirection();
        float shapeRot = GetShapeRotation(shapeData, facingDir, shapeData != prevShapeData);
        // Set size
        Vector2Int size = shapeData.Scale;
        if (curSizeData != null)
        {
            size *= curSizeData.Size;
        }

        // Display cannot fit here error
        ShowCannotFitHere(shapeData.TypeOfShape, size, shapeRot);
        // Play cannot fit sound
        failedTransformSound.Play();

        // Revert shape and size data to their previous states
        curShapeData = prevShapeData;
        curSizeData = prevSizeData;
    }

    /// <summary>
    /// Gets the rotation of the given shape with the specified data.
    /// </summary>
    /// <param name="data">Shape of collider to turn into</param>
    /// <param name="playerFacingDirection">The direction the player is facing</param>
    /// <param name="isDifferentShape">If the shape has changed from last time</param>
    /// <returns>Rotation in degrees that the shape should be.</returns>
    private float GetShapeRotation(ShapeData data, Vector2Int playerFacingDirection, bool isDifferentShape)
    {
        float rot = 0.0f;
        if (data != null)
        {
            // If the direction affects scale
            if (data.DirectionAffectsScale)
            {
                // If this is the first time we are swapping to this shape,
                // then base the player's rotation on their facing direction.
                if (isDifferentShape)
                {
                    // Facing up's rotation is 0
                    if (playerFacingDirection.y > 0)
                    {
                        rot = 0.0f;
                    }
                    // Facing left's rotation is 90
                    else if (playerFacingDirection.x < 0)
                    {
                        rot = 90.0f;
                    }
                    // Facing down's rotation is 180
                    else if (playerFacingDirection.y < 0)
                    {
                        rot = 180.0f;
                    }
                    // Facing right's rotation is 270
                    else if (playerFacingDirection.x > 0)
                    {
                        rot = 270.0f;
                    }
                }
                // If we are just updating the current scale, just spin based off the last rotation
                else
                {
                    rot = (shapeRotation + 90.0f) % 360.0f;
                }
            }
        }

        return rot;
    }

    /// <summary>Starts smoothly changing the scale of the player.</summary>
    /// <param name="targetSize">Target size.</param>
    /// <param name="availSpot">Position that was close enough to allow for form changing.</param>
    /// <param name="directionMatters">If direction matters for the shape.</param>
    private void StartChangeForm(Vector2Int targetSize, Vector3 availSpot, bool directionMatters)
    {
        //Debug.Log("StartChangeForm");
        // Don't let the player move while changing form
        playerMoveRef.AllowMovement(false);
        // Play the sfx
        transformSizeSound.Play();

        Vector3 targetSizeV3 = new Vector3(targetSize.x, targetSize.y, 1);

        // If there is an ongoing coroutine, stop it
        if (!changeFormCoroutFin)
        {
            StopCoroutine(changeFormCorout);
        }
        // Start a new coroutine
        changeFormCorout = StartCoroutine(ShrinkCoroutine(targetSizeV3, availSpot, directionMatters));
    }

    /// <summary>Coroutine to shrink the player. Calls the ChangePositionCoroutine once done.</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// <param name="availSpot">Position that was close enough to allow for form changing</param>
    /// <param name="directionMatters">If direction matters for the shape.</param>
    private IEnumerator ShrinkCoroutine(Vector3 targetSize, Vector3 availSpot, bool directionMatters)
    {
        changeFormCoroutFin = false;

        // Save the current size
        Vector3 startSize = playerScaleCont.ShapeScale;

        // If we are shrinking and growing
        bool areShrinking = true;
        bool areGrowing = true;

        // Get the size to shrink to by taking the smallest value of the target and start size
        Vector3 shrinkTargetSize = new Vector3(SHRINK_SIZE.x, SHRINK_SIZE.y, 1);
        // No need to grow or shrink if the target and start size are the same length are the same
        if (!directionMatters && (targetSize - startSize).sqrMagnitude == 0)
        {
            areShrinking = false;
            areGrowing = false;
        }

        // Determine the speed of lerping for each coroutine
        // Assume we are not shrinking or not growing
        float lerpSpeed = changeSpeed / 2;
        // If we are both growing and shrinking, then recalculate the amount of iterations
        if (areShrinking && areGrowing)
        {
            lerpSpeed = changeSpeed / 3;
        }

        // Only do this transition if we are shrinking
        if (areShrinking)
        {
            float t = 0;
            while (t < 1)
            {
                // Change the size
                playerScaleCont.ShapeScale = Vector3.Lerp(startSize, shrinkTargetSize, t);
                // Step
                t += lerpSpeed * Time.deltaTime;

                yield return null;
            }
            playerScaleCont.ShapeScale = shrinkTargetSize;
        }

        // Call the second part of the transition
        changeFormCorout = StartCoroutine(ChangePositionCoroutine(targetSize, availSpot, areGrowing, lerpSpeed));

        yield return null;
    }

    /// <summary>Coroutine to change the position of the player. Calls the GrowCoroutine once done.</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// <param name="availSpot">Position that was close enough to allow for form changing</param>
    /// <param name="shouldGrow">If the form will also grow</param>
    /// <param name="lerpSpeed">The speed of the lerps to be done that change position.</param>
    private IEnumerator ChangePositionCoroutine(Vector3 targetSize, Vector3 availSpot, bool shouldGrow, float lerpSpeed)
    {
        // Starting position
        Vector3 startPos = playerMoveTrans.position;
        // Starting rotation
        float startRot = playerMoveTrans.eulerAngles.z;
        // Target rotation
        float targetRot = shapeRotation - ((shapeRotation - startRot) / 2);
        // Will hold the angle changes
        Vector3 angles;

        // Iteration information
        float changePosSpeed = lerpSpeed;
        float t;
        
        // If there is a wall in the way from the start to the target spot, use a midpoint to avoid the wall.
        WallInWayDecision wallInWay = new WallInWayDecision(colliderTest, startPos, availSpot);
        if (wallInWay.WasWallInWay)
        {
            // Since there was a wall in the way, we have to go towards the midpoint first
            // We will also have to half the iterations to make up for the extra movement
            changePosSpeed /= 2;
            // Step initialization
            t = 0;
            while (t < 1)
            {
                // Lerp towards the mid point
                playerMoveTrans.position = Vector3.Lerp(startPos, wallInWay.Midpoint, t);
                // Rotate towards desired rotation
                float newRot = Mathf.LerpAngle(startRot, targetRot, t);
                angles = playerMoveTrans.eulerAngles;
                angles.z = newRot;
                playerMoveTrans.eulerAngles = angles;
                // Step
                t += changePosSpeed * Time.deltaTime;

                yield return null;
            }
            // Set player position and rotation to get exact
            playerMoveTrans.position = wallInWay.Midpoint;
            angles = playerMoveTrans.eulerAngles;
            angles.z = targetRot;
            playerMoveTrans.eulerAngles = angles;
        }

        // Update the start position in case the player moved due to a wall
        startPos = playerMoveTrans.position;
        // Update the start and target rotations
        startRot = playerMoveTrans.eulerAngles.z;
        targetRot = shapeRotation;
        t = 0;
        while (t < 1)
        {
            // Lerp towards the available spot
            playerMoveTrans.position = Vector3.Lerp(startPos, availSpot, t);
            // Rotate towards desired rotation
            float newRot = Mathf.LerpAngle(startRot, targetRot, t);
            angles = playerMoveTrans.eulerAngles;
            angles.z = newRot;
            playerMoveTrans.eulerAngles = angles;
            // Step
            t += changePosSpeed * Time.deltaTime;

            yield return null;
        }
        // Set player position and rotation to get exact
        //playerMoveTrans.position = availSpot;
        angles = playerMoveTrans.eulerAngles;
        angles.z = targetRot;
        playerMoveTrans.eulerAngles = angles;

        // Call the last part of the transition
        changeFormCorout = StartCoroutine(GrowCoroutine(targetSize, shouldGrow, lerpSpeed));

        yield return null;
    }

    /// <summary>Coroutine to smoothly raise the scale of the player. Last coroutine for changing form</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// <param name="shouldGrow">If we should grow the player</param>
    /// <param name="lerpSpeed">The speed of the lerps done to grow.</param>
    private IEnumerator GrowCoroutine(Vector3 targetSize, bool shouldGrow, float lerpSpeed)
    {
        // Save the current size
        Vector3 startSize = playerScaleCont.ShapeScale;

        if (shouldGrow)
        {
            float t = 0;
            while (t < 1)
            {
                // Change the size
                playerScaleCont.ShapeScale = Vector3.Lerp(startSize, targetSize, t);
                // Step
                t += lerpSpeed * Time.deltaTime;

                yield return null;
            }
            playerScaleCont.ShapeScale = targetSize;
        }

        // Finish the form change
        FinishFormChange();

        yield return null;
    }

    /// <summary>Called by the last coroutine for changing forms.
    /// Allows the player to move, marks the form change as done, and calls the form change finish event.</summary>
    private void FinishFormChange()
    {
        playerMoveRef.GetComponent<GridMover>().RecenterOnTile();
        // Let the player move again
        playerMoveRef.AllowMovement(true);
        // End the form change
        changeFormCoroutFin = true;
        // Call the finish form change event
        OnFinishChangingForm?.Invoke();
    }

    /// <summary>Shows the visual for the player not fitting somewhere.</summary>
    /// <param name="shapeType">Type of shape the player tried to change to.</param>
    /// <param name="size">Size the player tried to change to.</param>
    /// <param name="rotation">Rotation of the shape to display.</param>
    private void ShowCannotFitHere(ShapeData.ShapeType shapeType, Vector2Int size, float rotation)
    {
        // Activate the cannot fit controller to show itself
        Vector3 pos = playerMoveTrans.position;
        pos.z = cannotFitCont.transform.position.z;
        Vector3 sizeV3 = new Vector3(size.x, size.y);
        cannotFitCont.Activate(pos, sizeV3, rotation, shapeType);
    }

}
