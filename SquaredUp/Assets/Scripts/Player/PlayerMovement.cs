﻿using System.Collections;
using UnityEngine;

// For Player Movement
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // References
    // Reference to the pivot of the player's eyes.
    [SerializeField] private Transform eyePivot = null;
    // Reference to the player's rigibbody.
    private Rigidbody2D rb = null;

    // Speed of the player.
    [SerializeField] [Min(0.01f)] private float speed = 1f;

    // Smooths rotation/turn speed of eyes.
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Variables for the MoveEyes coroutine
    private bool eyeCoroutineActive = false;
    private float targetAngle = 0;

    // If the player is allowed to move
    private bool allowMove;


    // Called 0th
    // Set references
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        // Sub to Movement
        AllowMovement(true);
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        // Unsub from Movement
        AllowMovement(false);
    }


    // Called when the player inputs movement.
    public void OnMovement(Vector2 rawInputVector)
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
            rb.velocity = direction * speed;
        }
        else
        {
            // Cancel out rigidbody velocity if there is no movement, because the physics system hates player controllers.
            rb.velocity = Vector3.zero;
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
}