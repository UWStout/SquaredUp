using UnityEngine;

/// <summary>Rotates the object its attached to</summary>
public class Rotate : MonoBehaviour
{
    // Speed of rotation
    [SerializeField] private Vector3 rotationSpeed = Vector3.one;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
