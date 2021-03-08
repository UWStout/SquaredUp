using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles swapping the colliders that the player uses</summary>
public class PlayerColliderController : MonoBehaviour
{
    // Reference to the TestCollider script
    [SerializeField] private TestCollider testColliderRef = null;
    // References to the gameObjects that hold the colldiers the player needs to change on shape swap
    [SerializeField] private GameObject[] playerColliderObjs = new GameObject[2];

    // Lists of colliders
    // List of box colliders
    private List<BoxCollider2D> boxColliders = new List<BoxCollider2D>();
    // List of circle colliders
    private List<CircleCollider2D> circleColliders = new List<CircleCollider2D>();
    // List of polygon colliders
    private List<PolygonCollider2D> polygonColliders = new List<PolygonCollider2D>();


    // Called 0th
    // Set references
    private void Awake()
    {
        // Get references to colliders
        // Box colliders
        GetColliders(boxColliders);
        // Circle colliders
        GetColliders(circleColliders);
        // Polygon colliders
        GetColliders(polygonColliders);
    }

    /// <summary>Turns on the colliders for the given shape's type.
    /// Returns true if the player was able to fit in the current location and their colliders were changed.
    /// Returns False if they cannot fit in their current location and their colliders were not changed.</summary>
    /// <param name="data">Data of the shape to test</param>
    /// <param name="size">The actual size of the collider to test</param>
    public bool ActivateCollider(ShapeData data, Vector3 size)
    {
        bool check = TestColliderChange(data, size);
        if (check)
        {
            switch (data.ColliderShape)
            {
                // BoxCollider2D
                case ShapeData.ColliderType.BOX:
                    EnableOneColliderType(boxColliders);
                    break;
                // CircleCollider2D
                case ShapeData.ColliderType.CIRCLE:
                    EnableOneColliderType(circleColliders);
                    break;
                // Triangle needs to be a specific kind of polygon collider
                case ShapeData.ColliderType.TRIANGLE:
                    MorphPolygonToShape(ShapeData.TRIANGLE_POINTS);
                    EnableOneColliderType(polygonColliders);
                    break;
                default:
                    Debug.LogError("Unhandled ColliderType of '" + data.ColliderShape + "' in PlayerColliderController.cs");
                    break;
            }
            return true;
        }
        return false;
    }

    /// <summary>Tests if the given type of shape will fit in the player's current position.
    /// Returns true if the player can fit. False if they cannot</summary>
    /// <param name="data">Data for the shape of collider to test</param>
    /// <param name="size">The actual size of the collider to test</param>
    public bool TestColliderChange(ShapeData data, Vector3 size)
    {
        return !testColliderRef.CheckIfColliderWillHitWall(data, size);
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
                col = obj.AddComponent<T>();
                col.enabled = false;
            }
            colliderList.Add(col);
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

    /// <summary>Changes the polygon collider to have the given points</summary>
    private void MorphPolygonToShape(Vector2[] points)
    {
        foreach (PolygonCollider2D col in polygonColliders)
        {
            col.points = points;
        }
    }
}
