using System.Collections;
using UnityEngine;

// For Player Movement
[RequireComponent(typeof(GridMover))]
public class PlayerMovement : SingletonMonoBehav<PlayerMovement>
{
    private enum eDirectionPriority { Horizontal, Vertical };

    // References
    // Reference to the pivot of the player's eyes.
    [SerializeField] private Transform eyePivot = null;
    // Reference to the player's grid mover.
    private GridMover gridMover = null;

    // Speed of the player.
    [SerializeField] [Min(0.01f)] private float speed = 1f;
    // Slow walk speed of the player
    [SerializeField] [Min(0.01f)] private float slowWalkSpeed = 0.5f;
    // Sprint speed of the player
    [SerializeField] [Min(0.01f)] private float sprintSpeed = 0.5f;

    // Smooths rotation/turn speed of eyes.
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Variables for the MoveEyes coroutine
    private bool eyeCoroutineActive = false;
    private float targetAngle = 0;

    // If the player is allowed to move
    private bool allowMove = false;

    // What direction the player is currently moving
    private QuadDirection2D moveDir = QuadDirection2D.none;
    // If the player is walking or moving at normal speed
    private bool isSlowWalking = false;
    // If the player is sprinting or not
    private bool isSprinting = false;

    private eDirectionPriority lastMovementPriority = eDirectionPriority.Horizontal;


    // Called 0th
    // Set references
    protected override void Awake()
    {
        base.Awake();

        gridMover = GetComponent<GridMover>();
    }
    // Called 1st
    // Foreign Initialization
    private void Start()
    {
        SetPlayerPosition(transform.position);
    }
    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        // Sub to Movement
        AllowMovement(true);

        InputEvents.SlowWalkEvent += OnSlowWalk;
        InputEvents.SprintEvent += OnSprint;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        // Unsub from Movement
        AllowMovement(false);

        InputEvents.SlowWalkEvent -= OnSlowWalk;
        InputEvents.SprintEvent -= OnSprint;
    }
    // Called once every frame
    private void Update()
    {
        Move();
    }


    /// <summary>
    /// Tries to move the player in their current move direction.
    /// </summary>
    private void Move()
    {
        // Don't even try moving if we have no input direction (compares enums, saves performance)
        if (moveDir == QuadDirection2D.none)
        {
            return;
        }
        // Try to move the player
        if (gridMover.Move(moveDir, out bool isCurrentlyMoving))
        {
            return;
        }
        if (isCurrentlyMoving)
        {
            return;
        }

        // If player couldn't move then try moving them in x or y separately
        Vector2 moveDirVector = moveDir.Vector;
        // If they were moving only in the x or y, the player just can't move
        if (moveDirVector.x == 0 || moveDirVector.y == 0)
        {
            return;
        }

        QuadDirection2D horiMoveDir = new Vector2(moveDirVector.x, 0).GetDirection2D().ToQuadDirection2D();
        QuadDirection2D vertMoveDir = new Vector2(0, moveDirVector.y).GetDirection2D().ToQuadDirection2D();
        // Try vertical first if we tried horizontal first last time
        if (lastMovementPriority == eDirectionPriority.Horizontal)
        {
            // Vertical move try
            if (gridMover.Move(vertMoveDir))
            {
                lastMovementPriority = eDirectionPriority.Vertical;
                return;
            }
        }
        // Horizontal move try
        if (gridMover.Move(horiMoveDir))
        {
            lastMovementPriority = eDirectionPriority.Horizontal;
            return;
        }
        // Try vertical second if we tried vertical first last time
        if (lastMovementPriority == eDirectionPriority.Vertical)
        {
            // Vertical move try
            if (gridMover.Move(vertMoveDir))
            {
                lastMovementPriority = eDirectionPriority.Vertical;
                return;
            }
        }
    }


    // Called when the player inputs movement.
    private void OnMovement(Vector2 rawInputVector)
    {
        // Check for input
        Vector2 direction = rawInputVector.normalized;
        if (direction.magnitude == 0.0f)
        {
            moveDir = QuadDirection2D.none;
            return;
        }
        targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        // Rotate the eye to fit the new player input.
        if (!eyeCoroutineActive)
        {
            StartCoroutine(MoveEyes());
        }

        // Movement
        // Adjust speed
        float moveSpeed = isSlowWalking ? slowWalkSpeed : speed;
        moveSpeed = isSprinting ? sprintSpeed : moveSpeed;
        gridMover.speed = moveSpeed;
        // Try to move in the right direction
        Direction2D horDir = new Vector2(direction.x, 0).GetDirection2D();
        Direction2D vertDir = new Vector2(0, direction.y).GetDirection2D();
        moveDir = horDir.Add(vertDir);
    }
    // Called when the player inputs slow walk.
    private void OnSlowWalk(bool shouldSlowWalk)
    {
        isSlowWalking = shouldSlowWalk;
        OnMovement(moveDir.Vector);
    }
    // Called when the player inputs sprint.
    private void OnSprint(bool shouldSprint)
    {
        isSprinting = shouldSprint;
        OnMovement(moveDir.Vector);
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
                moveDir = QuadDirection2D.none;
                allowMove = false;
            }
        }
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
    /// Sets the player's position to the given position.
    /// Also upadtes the player's grid move target to that position.
    /// </summary>
    /// <param name="position"></param>
    public void SetPlayerPosition(Vector2 position)
    {
        gridMover.SetPosition(position);
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