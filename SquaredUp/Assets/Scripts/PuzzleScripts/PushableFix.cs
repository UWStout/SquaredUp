
using UnityEngine;

public class PushableFix : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        function(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        function(collision);
    }

    private void function(Collision2D collision)
    {
        Vector2 hitPos = collision.GetContact(0).point;
        if (collision.gameObject.tag == "Player")
        {
            Vector2 dir = new Vector2(0, 0);
            if (Mathf.Abs(transform.position.x - hitPos.x) > Mathf.Abs(transform.position.y - hitPos.y))
            {
                dir.y = 0;
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
                if (Mathf.Abs(transform.position.y) > Mathf.Abs(hitPos.y))
                {
                    dir.y = 1;
                }
                else
                {
                    dir.y = -1;
                }
            }
            parentObject.GetComponent<Rigidbody2D>().velocity = dir * 12;

        }
    }
}