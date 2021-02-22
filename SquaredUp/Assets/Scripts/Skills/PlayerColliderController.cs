using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [SerializeField] private GameObject playerColliderObj = null;

    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        boxCollider = playerColliderObj.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }
        circleCollider = playerColliderObj.GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.enabled = false;
        }
    }

    public void ActivateCollider(ShapeData.ColliderType type)
    {
        switch (type)
        {
            case ShapeData.ColliderType.BOX:
                EnableOneCollider(boxCollider);
                break;
            case ShapeData.ColliderType.CIRCLE:
                EnableOneCollider(circleCollider);
                break;
            default:
                Debug.LogError("Unhandled ColliderType of '" + type + "' in PlayerColliderController.cs");
                break;
        }
    }

    private void DisableAllColliders()
    {
        boxCollider.enabled = false;
        circleCollider.enabled = false;
    }

    private void EnableOneCollider(Collider2D col)
    {
        DisableAllColliders();
        col.enabled = true;
    }
}
