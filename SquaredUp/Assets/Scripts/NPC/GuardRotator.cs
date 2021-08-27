/// <summary>
/// Rotates the guard to face the current direction based on the specified units.
/// </summary>
public class GuardRotator : GuardMovementBase
{
    protected override void Move(GuardMovementUnit curUnit)
    {
        FaceDirection(curUnit.GetPoint());
        IncrementNextUnit(curUnit);
    }


    /// <summary>
    /// Loads in the given runtime variables.
    /// </summary>
    public new void Load(bool lShouldMove, int lCurIndex, float lWaitTimer, float lTimeToWait)
    {
        base.Load(lShouldMove, lCurIndex, lWaitTimer, lTimeToWait);
    }
}
