using System;
using UnityEngine;

/// <summary>
/// Save data for a guard who can move.
/// </summary>
[Serializable]
public class GuardMovementSaveData : GuardMovementBaseSaveData
{
    public Vector2 GetLastMoveDirection() => lastMoveDir;
    private Vector2 lastMoveDir = Vector2.zero;
    public bool GetIsFirstMove() => isFirstMove;
    private bool isFirstMove = true;


    public GuardMovementSaveData(GuardMovement guardMovement) : base(guardMovement)
    {
        lastMoveDir = guardMovement.GetLastMoveDirection();
        isFirstMove = guardMovement.CheckIsFirstMove();
    }
}
