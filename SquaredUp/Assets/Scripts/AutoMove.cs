using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] private Vector3 _movementSpeed = Vector3.zero;


    private void Update()
    {
        transform.position += _movementSpeed * Time.deltaTime;
    }
}
