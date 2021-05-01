
using UnityEngine;

public class PushableFix : MonoBehaviour
{
    [SerializeField] private Rigidbody2D parentPhys;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        function(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //function(collision);
    }

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
}