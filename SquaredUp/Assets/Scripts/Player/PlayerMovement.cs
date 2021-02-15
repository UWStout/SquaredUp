using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // References
    // Reference to the player's rigibbody.
    [SerializeField]
    private Rigidbody rb = null;
    // Reference to the pivot of the player's eyes.
    [SerializeField]
    private Transform eyePivot = null;

    // Speed of the player.
    [Min(0.01f)]
    [SerializeField]
    private float speed = 1f;
    // Holds the player's input axis values.
    private Vector3 rawInputMovement;

    // Smooths rotation/turn speed of eyes.
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Reference to the coroutine of the eyes moving.
    private Coroutine eyeCoroutine = null;

    // Start is called before the first frame update
    private void Start()
    {
        rawInputMovement = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if there is input.
        Vector3 direction = rawInputMovement.normalized;
        if (direction.magnitude != 0)
        {
            // Rotate the eye to fit the new player input.
            if (eyeCoroutine != null)
            {
                StopCoroutine(eyeCoroutine);
            }
            eyeCoroutine = StartCoroutine(MoveEyes(direction));

            // Movement
            //transform.position += direction * Time.deltaTime * speed;
            rb.velocity = direction * speed;
        }
        else
        {
            // Cancel out rigidbody velocity if there is no movement, because the physics system hates player controllers.
            rb.velocity = Vector3.zero;
        }
    }

    // Called when the player inputs movement.
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    /// <summary>
    /// Coroutine to rotate the eyes to which direction the player is moving.
    /// </summary>
    /// <param name="direction">Direction to put the eyes in.</param>
    private IEnumerator MoveEyes(Vector3 direction)
    {
        // Eye rotation
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        while (targetAngle != eyePivot.eulerAngles.y)
        {
            float angle = Mathf.SmoothDampAngle(eyePivot.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 newAngles = eyePivot.eulerAngles;
            newAngles.y = angle;
            eyePivot.rotation = Quaternion.Euler(newAngles);

            yield return null;
        }
        yield return null;
    }
}