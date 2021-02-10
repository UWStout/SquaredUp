using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private Vector3 rawInputMovement;

    [SerializeField]
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rawInputMovement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = rawInputMovement.normalized;
        if (direction.magnitude != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 newAngles = transform.eulerAngles;
            newAngles.y = angle;
            transform.rotation = Quaternion.Euler(newAngles);
            // Movement.
            transform.position += direction * Time.deltaTime * speed;
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
}