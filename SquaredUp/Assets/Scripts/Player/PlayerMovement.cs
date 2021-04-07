using System.Collections;
using UnityEngine;
[RequireComponent(typeof(TileMovement))]
public class PlayerMovement : MonoBehaviour
{
    // References
    // Reference to the pivot of the player's eyes.
    [SerializeField] private Transform eyePivot = null;
    // Smooths rotation/turn speed of eyes.
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;
    // Reference for TileMovement
    private TileMovement tm;
    // Variables for the MoveEyes coroutine
    private bool eyeCoroutineActive = false;
    private float targetAngle = 0;
    // Input from player
    private Vector2 inputDirection;

    // Called 0th
    // Set references

    private void Awake()
    {
        tm = GetComponent<TileMovement>();
    }
    private void GetMovement(Vector2 Input)
    {
        // Check for input
        Vector2 direction = Input.normalized;
        if (direction.magnitude != 0)
        {
            targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            // Rotate the eye to fit the new player input.
            if (!eyeCoroutineActive)
            {
                StartCoroutine(MoveEyes());
            }
        }
        inputDirection = direction;
        if(inputDirection.x != 0)
        {
            inputDirection.y = 0;
        }
        HandlePlayerMovement();
        
    }
    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {  
        InputEvents.MovementEvent += GetMovement;
    }
    
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.MovementEvent -= GetMovement;
    }

    private void HandlePlayerMovement()
    {
        tm.SetProjectedDirection(inputDirection);
    }

    public void AllowMovement(bool shouldAllow)
    {
        tm.AllowMovement(shouldAllow);
    }

    /// <summary>
    /// Coroutine to rotate the eyes to which direction the player is moving.
    /// </summary>
    /// <param name="direction">Direction to put the eyes in.</param>
    private IEnumerator MoveEyes()
    {
        eyeCoroutineActive = true;
        // Eye rotation
        while (targetAngle != eyePivot.eulerAngles.z)
        {
            float angle = Mathf.SmoothDampAngle(eyePivot.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 newAngles = eyePivot.eulerAngles;
            newAngles.z = angle;
            eyePivot.rotation = Quaternion.Euler(newAngles);

            yield return null;
        }
        eyeCoroutineActive = false;
        yield return null;
    }

    /// <summary>Lets the player move if given true. Keeps the player from moving if given false.
    /// Subscribes and unsubscribes the movement function from the Movement Input Event</summary>
   

    /// <summary>Returns the facing direction of the player</summary>
    public Vector2Int GetFacingDirection()
    {
        Vector2 rawForward = eyePivot.up;

        if (Mathf.Abs(rawForward.x) > Mathf.Abs(rawForward.y))
        {
            if (rawForward.x > 0)
            {
                return new Vector2Int(1, 0);
            }
            else
            {
                return new Vector2Int(-1, 0);
            }
        }
        else
        {
            if (rawForward.y > 0)
            {
                return new Vector2Int(0, 1);
            }
            else
            {
                return new Vector2Int(0, -1);
            }
        }
    }
}