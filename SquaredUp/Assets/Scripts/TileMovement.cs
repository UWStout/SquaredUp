using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For Player Movement
[RequireComponent(typeof(Rigidbody2D))]
public class TileMovement : MonoBehaviour
{
    // Movement Speed
    [SerializeField] private float moveSpeed = 30f;
    // Tile Width
    [SerializeField] private float tileWidth = .5f;
    // Reference to the player's rigibbody.
    private Rigidbody2D rb = null;
    // Variables for the MovePlayer coroutine
    private Coroutine activeMovementCoroutine;
    private bool movementCoroutineActive = false;
    private bool allowMovement = true;
    private Vector3 startMovementPosition = Vector3.zero;
    private Vector2 projectedDirection = Vector2.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
        Vector2 contactPoint = Vector2.zero;
        if(collision.contactCount > 0)
        {
            contactPoint = collision.GetContact(0).point;
        }
        else
        {
            Debug.LogError("Collision had 0 contact points.");
        }
        Vector2 rawDirection = new Vector2(rb.position.x - contactPoint.x, rb.position.y - contactPoint.y);
        
        rb.transform.position = SnapToTile(rb.position + rawDirection.normalized * (tileWidth));
        if (movementCoroutineActive)
        {
            StopCoroutine(activeMovementCoroutine);
            movementCoroutineActive = false;
        }
    }
 
    // Setter for projectedDirection that also calls HandleMovement with the inputted Vector2
    public void SetProjectedDirection(Vector2 velocity)
    {
        this.projectedDirection = velocity;
        HandleMovement(this.projectedDirection);
    }
    // Function for starting the MovePlayer coroutine if it is not already active
    private void StartMovePlayer(Vector2 inputDirection)
    {
        if (movementCoroutineActive)
        {
            StopCoroutine(activeMovementCoroutine);
        }
        activeMovementCoroutine = StartCoroutine(MovePlayer(inputDirection));
    }


    private IEnumerator MovePlayer(Vector2 inputDirection)
    {
        //Debug.Log("MovePlayer started");
        movementCoroutineActive = true;
        Vector3 inputDirection3D = new Vector3(inputDirection.x, inputDirection.y);
        startMovementPosition = rb.transform.position;
        Vector3 destination = GetDestination(inputDirection3D);
        Vector3 initialMovementDirection = (destination - startMovementPosition);
        // Check that the direction the object is going in is not zero so it can be normalized
        if(initialMovementDirection == Vector3.zero)
        {
            Debug.LogError("Initial Movement Direction was 0");
        }
        initialMovementDirection = initialMovementDirection.normalized;
        Vector3 currentMovementDirection = initialMovementDirection;
        // While the object is still going in the initial direction
        while (initialMovementDirection == currentMovementDirection.normalized)
        {
            // 
            rb.velocity = initialMovementDirection * moveSpeed;
            currentMovementDirection = (destination - transform.position);
            if (currentMovementDirection == Vector3.zero)
            {
                Debug.LogError("Current Movement Direction was 0");
            }
            yield return null;
        }
        //  Zero out velocity, transform to destination, set movementCoroutineActive to false, and call HandleMovement
        if(projectedDirection != inputDirection)
        {
            rb.velocity = Vector2.zero;
            rb.transform.position = destination;
        }
        movementCoroutineActive = false;
        HandleMovement(projectedDirection);
        //Debug.Log("MovePlayer ended");
        yield return null;
    }

    // Getter for destination object will be moving towards (adjusts for grid movement by snapping to grid on x and y)
    private Vector3 GetDestination(Vector3 direction)
    {
        Vector3 destination = startMovementPosition + (direction* tileWidth);
        return SnapToTile(destination);
    }
    private Vector3 SnapToTile(Vector3 destination)
    {
        destination.x = Mathf.Round(destination.x / tileWidth) * tileWidth;
        destination.y = Mathf.Round(destination.y / tileWidth) * tileWidth;
        return destination;
    }

    private void HandleMovement(Vector2 moveDirection)
    {
        Debug.Log("HandleMovement");
        if (allowMovement)
        {
            // check for input
            //check if input is still going
            if (!movementCoroutineActive)
            {
                // if the projected direction is not 0, recursively call StartMovePlayer
                if (moveDirection != Vector2.zero)
                {
                    StartMovePlayer(moveDirection);
                }
            }
        }
    }

    public void AllowMovement(bool shouldAllow)
    {
        if (shouldAllow)
        {
            if (allowMovement == false)
            {
                allowMovement = true;
            }
        }
        else
        {
            if (allowMovement)
            {
                allowMovement = false;
            }
        }
        // Clear velocity to be safe
        rb.velocity = Vector2.zero;
    }
}
