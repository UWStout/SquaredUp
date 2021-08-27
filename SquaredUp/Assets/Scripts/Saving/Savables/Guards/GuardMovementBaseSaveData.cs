using System;

/// <summary>
/// Save data for the base guard movement class.
/// </summary>
[Serializable]
public class GuardMovementBaseSaveData
{
    // If the guard was allowed to move.
    public bool CheckShouldMove() => shouldMove;
    private bool shouldMove;
    // Index in path.
    public int GetCurIndex() => curIndex;
    private int curIndex;
    // Current time waited.
    public float GetWaitTimer() => waitTimer;
    private float waitTimer;
    // Time that must be waited.
    public float GetTimeToWait() => timeToWait;
    private float timeToWait;


    public GuardMovementBaseSaveData(GuardMovementBase guardMoveBase)
    {
        shouldMove = guardMoveBase.CheckShouldMove();
        curIndex = guardMoveBase.GetCurIndex();
        waitTimer = guardMoveBase.GetWaitTimer();
        timeToWait = guardMoveBase.GetTimeToWait();
    }
}
