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
        }
    }

    // If the size data has been changed since last time
    private bool wasSizeChanged = false;
    // Current size to change to
    private SizeData curSizeData = null;
    public SizeData CurSizeData {
        set {
            wasSizeChanged = curSizeData != value;
            curSizeData = value;
        }
    }

    // Events
    // Event for when an available spot is found
    public delegate void AvailableSpotFound();
    public static event AvailableSpotFound OnAvailableSpotFound; 


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
        if (wasSizeChanged || wasShapeChanged ||
            (curShapeData != null && curShapeData.DirectionAffectsScale && size != playerScaleCont.ShapeScale))
        {
            // Swap the colliders
            // If the colliders couldn't be swapped, ergo could not fit, then do not swap the player's shape
            AvailableSpot availSpot = playerColContRef.ActivateCollider(curShapeData.ColliderShape, size);
            if (availSpot.Available)
            {
                // Call the available spot found event
                OnAvailableSpotFound?.Invoke();
                // Start changing form
                StartChangeForm(size, availSpot.Position);
            }
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
        Debug.Log("StartChangeForm");
        // Don't let the player move while changing form
        playerMoveRef.AllowMovement(false);

        // If there is an ongoing coroutine, stop it
        if (!changeFormCoroutFin)
        {
            StopCoroutine(changeFormCorout);
        }
        // Start a new coroutine
        changeFormCorout = StartCoroutine(ChangeFormCoroutine(targetSize, availSpot));
    }

    /// <summary>Coroutine to smoothly change the scale of the player</summary>
    /// <param name="targetSize">Size to lerp towards</param>
    /// /// <param name="availSpot">Position that was close enough to allow for form changing</param>
    private IEnumerator ChangeFormCoroutine(Vector3 targetSize, Vector3 availSpot)
    {
        changeFormCoroutFin = false;

        // The amount of lerps that will be done
        int iterations = (int)(1 / changeSpeed);
        // Save the current size
        Vector3 startSize = playerScaleCont.ShapeScale;
        // Save the current position
        Vector3 startPos = playerMoveTrans.position;

        for (int i = 0; i < iterations; ++i)
        {
            // Step
            float t = changeSpeed * i;
            // Change the size
            playerScaleCont.ShapeScale = Vector3.Lerp(startSize, targetSize, t);
            // Lerp towards the available spot
            playerMoveTrans.position = Vector3.Lerp(startPos, availSpot, t);

            yield return null;
        }
        playerScaleCont.ShapeScale = targetSize;
        playerMoveTrans.position = availSpot;

        // Let the player move again
        playerMoveRef.AllowMovement(true);

        changeFormCoroutFin = true;
        yield return null;
    }
}
