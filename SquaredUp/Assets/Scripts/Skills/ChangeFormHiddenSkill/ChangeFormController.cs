using System.Collections;
using UnityEngine;

/// <summary>Acts as a controller for the skills that affect the scale of the player.
/// ActivateFormChange must be called by the last skill to activate</summary>
public class ChangeFormController : MonoBehaviour
{
    // References
    // Scale controller for the player
    [SerializeField] private PlayerScaleController playerScaleCont = null;
    // Reference to the player collider controller
    [SerializeField] private PlayerColliderController playerColContRef = null;
    // Refernce to the player movement script
    [SerializeField] private PlayerMovement playerMoveRef = null;
    // Moving portion of the player
    [SerializeField] private Transform playerMoveTrans = null;
    // Reference tot he test collider script for checking for walls in the way
    [SerializeField] private TestCollider colliderTest = null;

    // Coroutine variables for how fast to change the shape and when we are close enough
    [SerializeField] private float changeSpeed = 0.01f;
    // If the coroutine is finished
    private bool changeFormCoroutFin = true;
    // Refrence to the coroutine running
    private Coroutine changeFormCorout = null;

    // If the shape data has been changed since last time
    private bool wasShapeChanged = false;
    // Current shape data to change to
    private ShapeData curShapeData = null;
    public ShapeData CurShapeData {
        set {
            wasShapeChanged = curShapeData != value;
            curShapeData = value;
            if (wasShapeChanged)
            {
                shapeWasChangedTo = false;
            }
        }
    }
    // If the current shape data has actually been applied
    private bool shapeWasChangedTo = true;

    // If the size data has been changed since last time
    private bool wasSizeChanged = false;
    // Current size to change to
    private SizeData curSizeData = null;
    public SizeData CurSizeData {
        set {
            wasSizeChanged = curSizeData != value;
            curSizeData = value;
            if (wasSizeChanged)
            {
                sizeWasChangedTo = false;
            }
        }
    }
    // If the current size data has actually been applied
    private bool sizeWasChangedTo = true;

    // Events
    // Event for when an available spot is found
    public delegate void AvailableSpotFound();
    public static event AvailableSpotFound OnAvailableSpotFound;
    // Event for when the form is done changing
    public delegate void FinishChangingForm();
    public static event FinishChangingForm OnFinishChangingForm;


    /// <summary>Should be called after shape data and size data have been given to it.
    /// Changes shape and size to be the specified size and shape</summary>
    public void ActivateFormChange()
    {
        Vector2Int newFacing = playerMoveRef.GetFacingDirection();
        Vector3 facingSize = GetSize(curShapeData, newFacing);
        // Set size using facing size and size data
        Vector3 size = facingSize;
        if (curSizeData != null)
        { 
            size = facingSize * curSizeData.Size;
        }

        /*
        Debug.Log("Was Size Changed: " + wasShapeChanged);
        Debug.Log("Was Shape Changed: " + wasShapeChanged);
        Debug.Log("CurShapeData: " + curShapeData);
        Debug.Log("Does direciton affect scale: " + curShapeData.DirectionAffectsScale);
        Debug.Log("Target Size: " + size);
        Debug.Log("Current Size: " + playerScaleCont.ShapeScale);
        Debug.Log("Target=Size? " + (size != playerScaleCont.ShapeScale));
        */

        // Change if size or shape was changed or
        // if the shape's direction affects scale and the scale has changed
        if ((wasSizeChanged || sizeWasChangedTo) || (wasShapeChanged || shapeWasChangedTo) ||
            (curShapeData != null && curShapeData.DirectionAffectsScale && size != playerScaleCont.ShapeScale))
        {
            // Swap the colliders
            // If the colliders couldn't be swapped, ergo could not fit, then do not swap the player's shape
            AvailableSpot availSpot = playerColContRef.ActivateCollider(curShapeData.TypeOfShape, Vector3.Scale(size, playerScaleCont.OriginalScale));
            if (availSpot.Available)
            {
                // Call the available spot found event
                OnAvailableSpotFound?.Invoke();
                // Start changing form
                StartChangeForm(size, availSpot.Position);
            }

            // The size and shape were now updated
            sizeWasChangedTo = true;
            shapeWasChangedTo = true;
        }
    }

    /// <summary>Gets the size of the shape given by the data</summary>
    /// <param name="data">Shape of collider to turn into</param>
    /// <param name="playerFacingDirection">The direction the player is facing</param>
    private Vector3 GetSize(ShapeData data, Vector2Int playerFacingDirection)
    {
        Vector3 size = Vector3.one;
        if (data != null)
        {
            size = data.Scale;
            if (data.DirectionAffectsScale)
            {
                // If facing left or right, swap x and y
                if (playerFacingDirection.x > 0 || playerFacingDirection.x < 0)
                {
                    float temp = size.x;
                    size.x = size.y;
                    size.y = temp;
                }
            }
        }

        return size;
    }

    /// <summary>Starts smoothly changing the scale of the player</summary>
    /// <param name="targetSize">Target size</param>
    /// <param name="availSpot">Position that was close enough to allow for form changing</param>
    private void StartChangeForm(Vector3 targetSize, Vector3 availSpot)
    {
        //Debug.Log("StartChangeForm");
        // Don't let the player move while changing form
        playerMoveRef.AllowMovement(false);

        // If there is an ongoing coroutine, stop it
        if (!changeFormCoroutFin)
        {
            StopCoroutine(changeFormCorout);
        }
        // Start a new coroutine
        changeFormCorout = StartCoroutine(ShrinkCoroutine(targetSize, availSpot));
    }

    /// <summary>Coroutine to shrink the player. Calls the ChangePositionCoroutine once done.</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// <param name="availSpot">Position that was close enough to allow for form changing</param>
    /// 
    private IEnumerator ShrinkCoroutine(Vector3 targetSize, Vector3 availSpot)
    {
        changeFormCoroutFin = false;

        // Save the current size
        Vector3 startSize = playerScaleCont.ShapeScale;

        // If we are shrinking
        bool areShrinking = true;
        bool areGrowing = true;

        Vector3 shrinkTargetSize = targetSize;
        // Identify which axix the player will be growing least one
        Vector3 difference = targetSize - startSize;
        float xDiff = difference.x;
        float yDiff = difference.y;
        // X is the smaller of the growths
        if (xDiff < yDiff)
        {
            shrinkTargetSize.y = startSize.y;
        }
        // Y is the smaller of the growths
        else if (xDiff > yDiff)
        {
            shrinkTargetSize.x = startSize.x;
        }
        // Else they are the same size
        else
        {
            // Determine if we are growing or shrinking
            areShrinking = xDiff < 0;
            areGrowing = !areShrinking;
        }

        // Determine the amount of iterations each coroutine should do
        // Assume we are not shrinking or not growing
        int iterations = (int)(1 / changeSpeed) / 2;
        // If we are both growing and shrinking, then recalculate the amount of iterations
        if (areShrinking && areGrowing)
        {
            iterations = (int)(1 / changeSpeed) / 3;
        }

        // Only do this transition if we are shrinking
        if (areShrinking)
        {
            float incAm = 1f / iterations;
            float t = 0;
            for (int i = 0; i < iterations; ++i)
            {
                // Change the size
                playerScaleCont.ShapeScale = Vector3.Lerp(startSize, shrinkTargetSize, t);
                // Step
                t += incAm;

                yield return null;
            }
            playerScaleCont.ShapeScale = shrinkTargetSize;
        }

        // Call the second part of the transition
        changeFormCorout = StartCoroutine(ChangePositionCoroutine(targetSize, availSpot, areGrowing, iterations));

        yield return null;
    }

    /// <summary>Coroutine to change the position of the player. Calls the GrowCoroutine once done.</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// <param name="availSpot">Position that was close enough to allow for form changing</param>
    /// <param name="shouldGrow">If the form will also grow</param>
    /// <param name="iterations">The amount of lerps that should be done to change position.</param>
    private IEnumerator ChangePositionCoroutine(Vector3 targetSize, Vector3 availSpot, bool shouldGrow, int iterations)
    {
        // Starting position
        Vector3 startPos = playerMoveTrans.position;

        // Iteration information
        int changePosIterations = iterations;
        float incAm;
        float t;
        
        // If there is a wall in the way from the start to the target spot, use a midpoint to avoid the wall.
        WallInWayDecision wallInWay = new WallInWayDecision(colliderTest, startPos, availSpot);
        if (wallInWay.WasWallInWay)
        {
            // Since there was a wall in the way, we have to go towards the midpoint first
            // We will also have to half the iterations to make up for the extra movement
            changePosIterations /= 2;
            // Step initialization
            incAm = 1f / changePosIterations;
            t = 0;
            for (int i = 0; i < changePosIterations; ++i)
            {
                // Lerp towards the mid point
                playerMoveTrans.position = Vector3.Lerp(startPos, wallInWay.Midpoint, t);
                // Step
                t += incAm;

                yield return null;
            }
            playerMoveTrans.position = wallInWay.Midpoint;
        }

        // Update the start position in case the player moved due to a wall
        startPos = playerMoveTrans.position;
        // Step initialization
        incAm = 1f / changePosIterations;
        t = 0;
        for (int i = 0; i < changePosIterations; ++i)
        {
            // Lerp towards the available spot
            playerMoveTrans.position = Vector3.Lerp(startPos, availSpot, t);
            // Step
            t += incAm;

            yield return null;
        }
        playerMoveTrans.position = availSpot;

        // Call the last part of the transition
        changeFormCorout = StartCoroutine(GrowCoroutine(targetSize, shouldGrow, iterations));

        yield return null;
    }

    /// <summary>Coroutine to smoothly raise the scale of the player. Last coroutine for changing form</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// <param name="shouldGrow">If we should grow the player</param>
    /// <param name="iterations">The amount of lerps that should be done to grow.</param>
    private IEnumerator GrowCoroutine(Vector3 targetSize, bool shouldGrow, int iterations)
    {
        // Save the current size
        Vector3 startSize = playerScaleCont.ShapeScale;

        if (shouldGrow)
        {
            float incAm = 1f / iterations;
            float t = 0;
            for (int i = 0; i < iterations; ++i)
            {
                // Change the size
                playerScaleCont.ShapeScale = Vector3.Lerp(startSize, targetSize, t);
                // Step
                t += incAm;

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
        // Let the player move again
        playerMoveRef.AllowMovement(true);
        // End the form change
        changeFormCoroutFin = true;
        // Call the finish form change event
        OnFinishChangingForm?.Invoke();
    }
}
