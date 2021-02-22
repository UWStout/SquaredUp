using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [SerializeField] private GameObject[] playerColliderObjs = new GameObject[2];

    private List<BoxCollider2D> boxColliders = new List<BoxCollider2D>(2);
    private List<CircleCollider2D> circleColliders = new List<CircleCollider2D>(2);

    private void Awake()
    {
        // Box colliders
        GetColliders(boxColliders);
        // Circle colliders
        GetColliders(circleColliders);
    }

    public void ActivateCollider(ShapeData.ColliderType type)
    {
        switch (type)
        {
            case ShapeData.ColliderType.BOX:
                EnableOneColliderType(boxColliders);
                break;
            case ShapeData.ColliderType.CIRCLE:
                EnableOneColliderType(circleColliders);
                break;
            default:
                Debug.LogError("Unhandled ColliderType of '" + type + "' in PlayerColliderController.cs");
                break;
        }
    }

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

    private void EnableOneColliderType<T>(List<T> colliderList) where T : Collider2D
    {
        DisableAllColliders();
        foreach (T col in colliderList)
        {
            col.enabled = true;
        }
    }
}
