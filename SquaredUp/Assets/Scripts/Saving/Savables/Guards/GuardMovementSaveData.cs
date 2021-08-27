using System;
using UnityEngine;

/// <summary>
/// Save data for a guard who can move.
/// </summary>
[Serializable]
public class GuardMovementSaveData : GuardMovementBaseSaveData
{
    public Vector2 GetLastMoveDirection() => new Vector2(lastMoveDir[0], lastMoveDir[1]);
    private float[] lastMoveDir = new float[2];
    public bool GetIsFirstMove() => isFirstMove;
    private bool isFirstMove = true;


    public GuardMovementSaveData(GuardMovement guardMovement) : base(guardMovement)
    {
        Vector2 lastMoveDirV2 = guardMovement.GetLastMoveDirection();
        lastMoveDir = new float[2] { lastMoveDirV2.x, lastMoveDirV2.y };
        isFirstMove = guardMovement.CheckIsFirstMove();
    }
}
