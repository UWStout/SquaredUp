using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCollision : MonoBehaviour
{
   
    [SerializeField] float slideSpeed = 1f;
    [SerializeField] private Rigidbody2D rb2d;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Pushable collision");
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 moveDirection = CastDirectionToCardinal(transform.position - contactPoint);
        rb2d.velocity = (moveDirection * slideSpeed);
    }
    private Vector3 CastDirectionToCardinal(Vector3 direction)
    {
        float rightDifference = (direction - Vector3.right).sqrMagnitude;
        float leftDifference = (direction - Vector3.left).sqrMagnitude;
        float upDifference = (direction - Vector3.up).sqrMagnitude;
        float downDifference = (direction - Vector3.down).sqrMagnitude;
        if (rightDifference < leftDifference && rightDifference < upDifference && rightDifference < downDifference)
        {
            return Vector3.right;
        }
        else if (leftDifference < upDifference && leftDifference < downDifference)
        {
            return Vector3.left;
        }
        else if (upDifference < downDifference)
        {
            return Vector3.up;
        }
        else
        {
            return Vector3.down;
        }
    }
}
