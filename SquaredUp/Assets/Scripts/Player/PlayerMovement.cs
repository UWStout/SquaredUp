using System.Collections;
using UnityEngine;

// For Player Movement
public class PlayerMovement : MonoBehaviour
{
    // References
    // Reference to the player's rigibbody.
    [SerializeField]
    private Rigidbody2D rb = null;
    // Reference to the pivot of the player's eyes.
    [SerializeField]
    private Transform eyePivot = null;

    // Speed of the player.
    [Min(0.01f)]
    [SerializeField]
    private float speed = 1f;
    // Holds the player's input axis values.
    private Vector2 rawInputMovement;

    // Smooths rotation/turn speed of eyes.
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Reference to the coroutine of the eyes moving.
    private Coroutine eyeCoroutine = null;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.MovementEvent += OnMovement;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.MovementEvent -= OnMovement;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rawInputMovement = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if there is input.
        Vector2 direction = rawInputMovement.normalized;
        if (direction.magnitude != 0)
        {
            // Rotate the eye to fit the new player input.
            if (eyeCoroutine != null)
            {
                StopCoroutine(eyeCoroutine);
            }
            eyeCoroutine = StartCoroutine(MoveEyes(direction));

            // Movement
            rb.velocity = direction * speed;
        }
        else
        {
            // Cancel out rigidbody velocity if there is no movement, because the physics system hates player controllers.
            rb.velocity = Vector3.zero;
        }
    }

    // Called when the player inputs movement.
    public void OnMovement(Vector2 rawInputVector)
    {
        rawInputMovement = rawInputVector;
    }

    /// <summary>
    /// Coroutine to rotate the eyes to which direction the player is moving.
    /// </summary>
    /// <param name="direction">Direction to put the eyes in.</param>
    private IEnumerator MoveEyes(Vector2 direction)
    {
        // Eye rotation
        float targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        while (targetAngle != eyePivot.eulerAngles.z)
        {
            float angle = Mathf.SmoothDampAngle(eyePivot.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 newAngles = eyePivot.eulerAngles;
            newAngles.z = angle;
            eyePivot.rotation = Quaternion.Euler(newAngles);

            yield return null;
        }
        yield return null;
    }
}