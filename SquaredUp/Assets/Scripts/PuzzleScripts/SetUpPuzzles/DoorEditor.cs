using UnityEngine;

/// <summary>Script to assist in editing the doors.</summary>
[ExecuteInEditMode]
public class DoorEditor : MonoBehaviour
{
    // Refernece to the physics transform
    [SerializeField] private Transform physicsTransform = null;
    // Reference to the tiled sprite renderer
    [SerializeField] private SpriteRenderer spriteRend = null;
    // The size of the door
    [SerializeField] private Vector2Int doorSize = Vector2Int.one;

#if UNITY_EDITOR
    private void Update()
    {
        physicsTransform.transform.localScale = new Vector3(doorSize.x, doorSize.y, 1);
        spriteRend.size = doorSize;
    }
#endif
}
