
/// <summary>
/// Save data for a guard.
/// </summary>
[System.Serializable]
public class GuardSaveData
{
    // If the guard was walking
    private bool wasWalking = false;
    // How far into walking the guard was
    private float walkingCounter = 0.0f;
    // How long the guard needs to still stop for
    private float stopCounter = 0.0f;
    // Index of the movement direction of the guard
    private int numSpot = 0;
    // If the guard is supposed to be stopped
    private bool guardStop = false;


    /// <summary>
    /// Creates save data for a normal guard.
    /// </summary>
    /// <param name="guardMovement"></param>
    public GuardSaveData(NPC_Movement guardMovement)
    {
        wasWalking = guardMovement.isWalking;
        walkingCounter = guardMovement.WalkingCounter;
        stopCounter = guardMovement.StopCounter;
        numSpot = guardMovement.NumSpot;
        guardStop = guardMovement.GuardStop;
    }
    /// <summary>
    /// Creates save data for a loop guard.
    /// </summary>
    /// <param name="guardMovement"></param>
    public GuardSaveData(NPC_MovementLoop guardMovement)
    {
        wasWalking = guardMovement.isWalking;
        walkingCounter = guardMovement.WalkingCounter;
        stopCounter = guardMovement.StopCounter;
        numSpot = guardMovement.NumSpot;
        guardStop = guardMovement.GuardStop;
    }

    /// <summary>
    /// Gets if the guard was walking when it was saved.
    /// </summary>
    /// <returns>Saved walking state.</returns>
    public bool GetWasWalking()
    {
        return wasWalking;
    }
    /// <summary>
    /// Gets the time left that the guard has to continue walking.
    /// </summary>
    /// <returns>Saved amount of time the guard has to walk still.</returns>
    public float GetWalkingCounter()
    {
        return walkingCounter;
    }
    /// <summary>
    /// Gets the time left that the guard has to be still for.
    /// </summary>
    /// <returns>Saved amount of time the guard has to stall for.</returns>
    public float GetStopCounter()
    {
        return stopCounter;
    }
    /// <summary>
    /// Gets the index of the walking direction that the guard should be walking in.
    /// </summary>
    /// <returns>Saved index of the direction to walk.</returns>
    public int GetNumSpot()
    {
        return numSpot;
    }
    /// <summary>
    /// Gets if the guard should be stopped.
    /// </summary>
    /// <returns>Saved stopped state.</returns>
    public bool GetGuardStop()
    {
        return guardStop;
    }
}
