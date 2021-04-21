
using UnityEngine;

public class PushableFix : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.tag == "Player")
        {
            Debug.Log("DIR");
            Vector2 dir=new Vector2(0,0);
            if (Mathf.Abs(transform.position.x - other.transform.position.x)>Mathf.Abs(transform.position.y - other.transform.position.y)){
                dir.y = 0;
                if (Mathf.Abs(transform.position.x) > Mathf.Abs(other.transform.position.x)){
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
                if (Mathf.Abs(transform.position.y) > Mathf.Abs(other.transform.position.y))
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
