using UnityEngine;

/// <summary>
/// Handles collisions for the ice pushable to force it to continously move in the
/// direction it is pushed in.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class IcePushable : MonoBehaviour
{
    // Speed to move the ice pushable at when pushed
    [SerializeField] private float slideSpeed = 12.0f;

    // Reference to the rigidbody2d attached to the ice pushable
    private Rigidbody2D parentPhys = null;
    // The direction the ice pushable was moving in previously
    private Direction2D previousDirection = Direction2D.none;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        parentPhys = GetComponent<Rigidbody2D>();
    }
    // Called when a collisions occurs with this gameobject
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }
    // Called when a collision occurs with this gameobject and a gameobject that
    // has collided with this game object previously, and has not stopped yet.
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Previously, this was only called in OnCollisionEnter2D, but had to be added here
        // because the ice can be colliding with a wall, and then if it hits the corner of
        // another tile (on the same wall) it would not call it, and would instead allow
        // the ice to move diagonally.
        HandleCollision(collision);
    }


    private void HandleCollision(Collision2D collision)
    {
        Vector2 moveDirection;
        // If the ice collided with the player, the ice should move away from the player
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ice"))
        {
            Direction2D hitFromDir = collision.GetDirectionHitCameFrom();
            moveDirection = hitFromDir.Vector;
        }
        // If the ice collided with something else like a wall
        else
        {
            Direction2D curMoveDirection = parentPhys.velocity.GetDirection2D();
            // If the direction changed to the opposite direction we were previously moving in,
            // the ice cube probably had a direct collision and should stop moving
            if (curMoveDirection.IsOppositeDirection(previousDirection))
            {
                moveDirection = Vector2.zero;
            }
            // If the direction only changed slightly and is not in the opposite direction,
            // the ice cube probably just brushed against something and should keep moving as it was
            else
            {
                moveDirection = previousDirection.Vector;
            }
        }

        // Set the ice's velocity to be fully in its movement direction
        parentPhys.velocity = moveDirection * slideSpeed;
        // Update the previous direction
        previousDirection = parentPhys.velocity.GetDirection2D();
    }


    /*
    private void function(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            parentPhys.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            Vector2 hitPos = (collision.GetContact(0).point + collision.GetContact(collision.contactCount - 1).point) / 2;
            Vector2 dir = new Vector2(0, 0);
            if (Mathf.Abs(transform.position.x - hitPos.x) > Mathf.Abs(transform.position.y - hitPos.y))
            {
                dir.y = 0;
                parentPhys.constraints = RigidbodyConstraints2D.FreezePositionY | ~RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                if (Mathf.Abs(transform.position.x) > Mathf.Abs(hitPos.x))
                {
                    dir.x = 1;
                }
                else
                {
                    dir.x = -1;
                }
            }
            else
            {
                dir.x = 0;
                parentPhys.constraints = ~RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                if (Mathf.Abs(transform.position.y) > Mathf.Abs(hitPos.y))
                {
                    dir.y = 1;
                }
                else
                {
                    dir.y = -1;
                }
            }
            parentPhys.velocity = dir * 12;

        }
    }
    */
}