using System;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{
    private const float COLLIDER_OFFSET_AMOUNT = 0.35f;
    private const float CLOSE_ENOUGH_DIST = 0.01f;


    public float speed { get => moveSpeed; set => moveSpeed = value; }
    [SerializeField] [Min(0.0f)] private float moveSpeed = 1.0f;
    public Vector2 lossySize => transform.lossyScale * size;
    public Vector2 size => collisionSize;
    [SerializeField] private Vector2 collisionSize = Vector2.one;

    private Vector2 targetPosition = Vector3.zero;

    public event Action<Collider2D> OnGridCollision;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        targetPosition = transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, lossySize.Rotate(transform.eulerAngles.z));
    }


    public bool Move(QuadDirection2D direction)
    {
        return Move(direction, out _);
    }
    public bool Move(QuadDirection2D direction, out bool isCurrenltyMoving)
    {
        //DrawPhysicsCheck(direction);
        // Don't move if we can't
        if (!CheckCanMove())
        {
            isCurrenltyMoving = true;
            return false;
        }
        isCurrenltyMoving = false;

        float tileSize = ActiveGrid.Instance.GetTileSize();
        LayerMask wallLayerMask = ActiveGrid.Instance.GetWallLayerMask();
        Vector2 dir = direction.Vector * tileSize;
        Vector2 newTargetPos = targetPosition + dir;
        Vector2 fudgedSize = new Vector2(lossySize.x - COLLIDER_OFFSET_AMOUNT, lossySize.y - COLLIDER_OFFSET_AMOUNT);
        List<RaycastHit2D> hits = new List<RaycastHit2D>(Physics2D.BoxCastAll(transform.position,
            fudgedSize, transform.eulerAngles.z, dir.normalized, dir.magnitude, wallLayerMask));
        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, MOVE_CHECK_RADIUS, dir.normalized, dir.magnitude, wallLayerMask);
        // Make sure we didn't hit ourself
        int selfHitIndex = -1;
        for (int i = 0; i < hits.Count; ++i)
        {
            if (hits[i].collider.gameObject == gameObject)
            {
                selfHitIndex = i;
                break;
            }
        }
        if (selfHitIndex >= 0)
        {
            hits.RemoveAt(selfHitIndex);
        }
        // Can't move if the collider doesn't have a grid wall to
        // potentially allow movement
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit.collider.TryGetComponent(out GridHittable hittable))
            {
                OnGridCollision?.Invoke(hit.collider);
                return false;
            }
            // Ask the grid hittable if we can move
            if (!hittable.Hit(new GridHit(transform.position, direction, speed, gameObject)))
            {
                // Hittable said no
                OnGridCollision?.Invoke(hit.collider);
                return false;
            }
        }

        // Can move if no collider, or all grid hittables said yes
        targetPosition = newTargetPos;
        return true;
    }
    /// <summary>
    /// Moves this object to a point on the grid closest to the given position.
    /// </summary>
    public void SetPosition(Vector3 position)
    {
        SetPosition(position, transform.lossyScale, transform.eulerAngles.z);
    }
    public void SetPosition(Vector3 position, Vector3 scale, float rotation, bool castToGridPos = true)
    {
        if (castToGridPos)
        {
            position = ActiveGrid.Instance.CastToGridPosition(position, scale, rotation);
        }
        transform.position = position;
        targetPosition = position;
    }


    private bool CheckCanMove()
    {
        Vector2 difference = (Vector2)transform.position - targetPosition;
        return difference.sqrMagnitude <= CLOSE_ENOUGH_DIST;
    }
    private void DrawPhysicsCheck(QuadDirection2D direction)
    {
        float tileSize = ActiveGrid.Instance.GetTileSize();
        LayerMask wallLayerMask = ActiveGrid.Instance.GetWallLayerMask();
        Vector2 dir = direction.Vector * tileSize;
        Vector2 fudgedSize = new Vector2(lossySize.x - COLLIDER_OFFSET_AMOUNT, lossySize.y - COLLIDER_OFFSET_AMOUNT);
        PhysicsDebugging.OverlapBox((Vector2)transform.position + dir, fudgedSize, transform.eulerAngles.z, wallLayerMask);
    }
}
