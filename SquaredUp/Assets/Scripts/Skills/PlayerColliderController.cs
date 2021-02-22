using System.Collections.Generic;
using UnityEngine;

// Handles swapping the colliders that the player uses
public class PlayerColliderController : MonoBehaviour
{
    // References to the gameObjects that hold the colldiers the player needs to change on shape swap
    [SerializeField] private GameObject[] playerColliderObjs = new GameObject[2];

    // Lists of colliders
    // List of box colliders
    private List<BoxCollider2D> boxColliders = new List<BoxCollider2D>(2);
    // List of circle colliders
    private List<CircleCollider2D> circleColliders = new List<CircleCollider2D>(2);

    // Called 0th
    private void Awake()
    {
        // Get all colliders
        // Box colliders
        GetColliders(boxColliders);
        // Circle colliders
        GetColliders(circleColliders);
    }

    /// <summary>Turns on the colliders for the given type</summary>
    /// <param name="type">Type of colliders to turn on</param>
    public void ActivateCollider(ShapeData.ColliderType type)
    {
        switch (type)
        {
            // BoxCollider2D
            case ShapeData.ColliderType.BOX:
                EnableOneColliderType(boxColliders);
                break;
            // CircleCollider2D
            case ShapeData.ColliderType.CIRCLE:
                EnableOneColliderType(circleColliders);
                break;
            default:
                Debug.LogError("Unhandled ColliderType of '" + type + "' in PlayerColliderController.cs");
                break;
        }
    }

    /// <summary>Gets the colliders from the player collider objects</summary>
    /// <param name="colliderList">List of 2D colliders to populate</param>
    private void GetColliders<T>(List<T> colliderList) where T : Collider2D
    {
        colliderList.Clear();
        foreach (GameObject obj in playerColliderObjs)
        {
            T col = obj.GetComponent<T>();
            if (col == null)
            {
                col = gameObject.AddComponent<T>();
                col.enabled = false;
                colliderList.Add(col);
            }
        }
    }

    /// <summary> Turns off all colliders for the player</summary>
    private void DisableAllColliders()
    {
        foreach (BoxCollider2D box in boxColliders)
        {
            box.enabled = false;
        }
        foreach (CircleCollider2D circ in circleColliders)
        {
            circ.enabled = false;
        }
    }

    /// <summary>Turns off all colliders and turns on the given collider</summary>
    /// <param name="colliderList">List of colliders to turn on</param>
    private void EnableOneColliderType<T>(List<T> colliderList) where T : Collider2D
    {
        DisableAllColliders();
        foreach (T col in colliderList)
        {
            col.enabled = true;
        }
    }
}
