using System.Collections;
using UnityEngine;

// For Player Movement
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : SingletonMonoBehav<PlayerMovement>
{
    // References
    // Reference to the pivot of the player's eyes.
    [SerializeField] private Transform eyePivot = null;
    // Reference to the player's rigibbody.
    private Rigidbody2D rb = null;

    // Speed of the player.
    [SerializeField] [Min(0.01f)] private float speed = 1f;
    // Slow walk speed of the player
    [SerializeField] [Min(0.01f)] private float slowWalkSpeed = 0.5f;

    // Smooths rotation/turn speed of eyes.
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Variables for the MoveEyes coroutine
    private bool eyeCoroutineActive = false;
    private float targetAngle = 0;

    // If the player is allowed to move
    private bool allowMove = false;
    private Vector3 moveVel = Vector3.zero;

    // If the player is walking or moving at normal speed
    private bool isSlowWalking = false;


    // Called 0th
    // Set references
    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
    }
    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        // Sub to Movement
        AllowMovement(true);

        InputEvents.SlowWalkEvent += OnSlowWalk;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        // Unsub from Movement
        AllowMovement(false);

        InputEvents.SlowWalkEvent -= OnSlowWalk;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // This fixes a bug where if you hold down the movement keys
        // you continue moving in whatever direction you push off the other thing of
        if (allowMove)
        {
           rb.velocity = moveVel;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = new Vector2(Mathf.Round(rb.velocity.x), Mathf.Round(rb.velocity.y));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        rb.velocity = moveVel;
    }

    // Called when the player inputs movement.
    private void OnMovement(Vector2 rawInputVector)
    {
        // Check for input
        Vector2 direction = rawInputVector.normalized;
        if (direction.magnitude != 0)
        {
            targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            // Rotate the eye to fit the new player input.
            if (!eyeCoroutineActive)
            {
                StartCoroutine(MoveEyes());
            }

            // Movement
            float moveSpeed = isSlowWalking ? slowWalkSpeed : speed;
            moveVel = direction * moveSpeed;
        }
        else
        {
            // Cancel out rigidbody velocity if there is no movement, because the physics system hates player controllers.
            moveVel = Vector3.zero;
        }
        rb.velocity = moveVel;
    }
    // Called when the player inputs slow walk.
    private void OnSlowWalk(bool shouldSlowWalk)
    {
        isSlowWalking = shouldSlowWalk;
        OnMovement(moveVel);
    }


    /// <summary>Lets the player move if given true. Keeps the player from moving if given false.
    /// Subscribes and unsubscribes the movement function from the Movement Input Event</summary>
    public void AllowMovement(bool shouldAllow)
    {
        if (shouldAllow)
        {
            if (allowMove == false)
            {
                InputEvents.MovementEvent += OnMovement;
                allowMove = true;
            }
        }
        else
        {
            if (allowMove)
            {
                InputEvents.MovementEvent -= OnMovement;
                allowMove = false;
            }
        }
        // Clear velocity to be safe
        rb.velocity = Vector2.zero;
    }

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
}