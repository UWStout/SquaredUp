using UnityEngine;

/// <summary>Teleports whatever walks in it to the target location.</summary>
public class TeleportTrigger : MonoBehaviour
{
    // The transform of the location to teleport to
    [SerializeField] private Transform targetLocation = null;
    [SerializeField] private bool changeX = true;
    [SerializeField] private bool changeY = false;


    // Called when a collision happens involving this trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = collision.gameObject.transform.position;
        if (changeX)
        {
            pos.x = targetLocation.position.x;
        }
        if (changeY)
        {
            pos.y = targetLocation.position.y;
        }
        collision.transform.position = pos;
    }
}
