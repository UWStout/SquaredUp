
[System.Serializable]
public class NormalGuardSaveData
{
    private bool wasWalking = false;
    private float walkingCounter = 0.0f;
    private float stopCounter = 0.0f;
    private int numSpot = 0;
    private bool guardStop = false;


    public NormalGuardSaveData(NPC_Movement guardMovement)
    {
        wasWalking = guardMovement.isWalking;
        walkingCounter = guardMovement.WalkingCounter;
        stopCounter = guardMovement.StopCounter;
        numSpot = guardMovement.NumSpot;
        guardStop = guardMovement.GuardStop;
    }

    public bool GetWasWalking()
    {
        return wasWalking;
    }
    public float GetWalkingCounter()
    {
        return walkingCounter;
    }
    public float GetStopCounter()
    {
        return stopCounter;
    }
    public int GetNumSpot()
    {
        return numSpot;
    }
    public bool GetGuardStop()
    {
        return guardStop;
    }
}
