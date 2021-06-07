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

    // Which collider type we currently have active
    private ShapeData.ShapeType activeColliderType = ShapeData.ShapeType.BOX;


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
    /// Returns an AvailableSpot to hold if the player was able to fit in the current location and what location the spot was found.
    /// If a spot was found, the colliders were updated.
    /// If a spot was not found, they cannot fit in their current location and their colliders were not changed.</summary>
    /// <param name="colliderType">Type of collider to activate</param>
    /// <param name="size">The actual size of the collider to test</param>
    public AvailableSpot ActivateCollider(ShapeData.ShapeType colliderType, Vector3 size)
    {
        AvailableSpot availSpot = TestColliderChange(colliderType, size);
        if (availSpot.Available)
        {
            ChangeColliderType(colliderType);
        }
        return availSpot;
    }

    /// <summary>Tests if the given type of shape will fit in the player's current position.
    /// Returns an AvailableSpot to hold if a spot was found (player can fit) and
    /// where that spot is</summary>
    /// <param name="colliderType">Shape of the collider to test</param>
    /// <param name="size">The actual size of the collider to test</param>
    public AvailableSpot TestColliderChange(ShapeData.ShapeType colliderType, Vector3 size)
    {
        return testColliderRef.CheckIfColliderWillHitWall(colliderType, size);
    }

    /// <summary>
    /// Actives the collider associated with the given shape type.
    /// Should only be used internally and by the save/load system.
    /// </summary>
    /// <param name="colliderType">Shape type to change the collider to.</param>
    public void ChangeColliderType(ShapeData.ShapeType colliderType)
    {
        activeColliderType = colliderType;
        switch (colliderType)
        {
            // BoxCollider2D
            case ShapeData.ShapeType.BOX:
                EnableOneColliderType(boxColliders);
                break;
            // CircleCollider2D
            case ShapeData.ShapeType.CIRCLE:
                EnableOneColliderType(circleColliders);
                break;
            // Triangle needs to be a specific kind of polygon collider
            case ShapeData.ShapeType.TRIANGLE:
                MorphPolygonToShape(ShapeData.TRIANGLE_POINTS);
                EnableOneColliderType(polygonColliders);
                break;
            default:
                Debug.LogError("Unhandled ColliderType of '" + colliderType + "' in PlayerColliderController.cs");
                break;
        }
    }

    /// <summary>
    /// Gets the type of collider is currently active.
    /// </summary>
    /// <returns>Type of collider that is currently active.</returns>
    public ShapeData.ShapeType GetActiveColliderType()
    {
        return activeColliderType;
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
        foreach (PolygonCollider2D poly in polygonColliders)
        {
            poly.enabled = false;
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
