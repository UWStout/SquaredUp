using UnityEngine;

/// <summary>Teleports whatever walks in it to the target location.</summary>
public class TeleportTrigger : MonoBehaviour
{
    // The transform of the location to teleport to
    [SerializeField] private Transform targetLocation = null;


    // Called when a collision happens involving this trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = targetLocation.position;
    }
}
