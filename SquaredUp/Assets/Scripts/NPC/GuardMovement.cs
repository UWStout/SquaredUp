using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the guard to the specified points.
/// </summary>
public class GuardMovement : GuardMovementBase
{
    [SerializeField] private float moveSpeed = 1.0f;

    private Vector2 startPosition = Vector2.negativeInfinity;
    private bool isRuntime = false;

    public Vector2 GetLastMoveDirection() => lastMoveDir;
    private Vector2 lastMoveDir = Vector2.zero;
    public bool CheckIsFirstMove() => isFirstMove;
    private bool isFirstMove = true;


    #region UnityMessages
    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        startPosition = transform.position;
        isRuntime = true;
    }
    // Debugging Gizmos
    private void OnDrawGizmosSelected()
    {
        Vector2 origin = transform.position;
        if (isRuntime)
        {
            origin = startPosition;
        }

        Gizmos.color = Color.red;
        IReadOnlyList<GuardMovementUnit> moveUnits = GetMovementUnits();
        foreach (GuardMovementUnit unit in moveUnits)
        {
            if (unit.GetWaitTime() > 0)
            {
                float size = unit.GetWaitTime() * 0.05f;
                size = Mathf.Clamp(size, 0.0f, 0.5f);
                Gizmos.DrawSphere(origin + unit.GetPoint(), size);
            }
        }
        Gizmos.color = Color.yellow;
        if (moveUnits.Count > 0)
        {
            Gizmos.DrawLine(origin, origin + moveUnits[0].GetPoint());
        }
        Gizmos.color = Color.blue;
        for (int i = 1; i < moveUnits.Count; ++i)
        {
            Gizmos.DrawLine(origin + moveUnits[i - 1].GetPoint(), origin + moveUnits[i].GetPoint());
        }
        if (moveUnits.Count > 1)
        {
            Gizmos.DrawLine(origin + moveUnits[moveUnits.Count - 1].GetPoint(), origin + moveUnits[0].GetPoint());
        }
    }
    #endregion UnityMessages


    public void Load(bool lShouldMove, int lCurIndex, float lWaitTimer, float lTimeToWait,
        Vector2 lLastMoveDir, bool lIsFirstMove)
    {
        base.Load(lShouldMove, lCurIndex, lWaitTimer, lTimeToWait);

        lastMoveDir = lLastMoveDir;
        isFirstMove = lIsFirstMove;
    }


    protected override void Move(GuardMovementUnit curUnit)
    {
        // Get the current move point;
        Vector2 curTargetPos = startPosition + curUnit.GetPoint();

        Vector2 curMoveDir = (curTargetPos - (Vector2)transform.position).normalized;
        // Once we reach our destination
        if (!isFirstMove && curMoveDir != lastMoveDir)
        {
            OnUnitReached(curUnit);
            return;
        }

        isFirstMove = false;
        lastMoveDir = curMoveDir;
        FaceDirection(curMoveDir);
        IncrementPosition(curMoveDir);
    }
    private void OnUnitReached(GuardMovementUnit unitReached)
    {
        transform.position = startPosition + unitReached.GetPoint();
        isFirstMove = true;

        IncrementNextUnit(unitReached);
    }
    
    private void IncrementPosition(Vector2 moveDir)
    {
        transform.position += (Vector3)(moveDir * Time.deltaTime * moveSpeed);
    }
}