using UnityEngine;

public class NormalGuardSavable : SavableMonoBehav<NormalGuardSavable>
{
    [SerializeField] private NPC_Movement guardMovement = null;

    public override void Load(object serializedObj)
    {
        // Cast data
        NormalGuardSaveData data = serializedObj as NormalGuardSaveData;

        // Load the variables in that had changed
        guardMovement.isWalking = data.GetWasWalking();
        guardMovement.WalkingCounter = data.GetWalkingCounter();
        guardMovement.StopCounter = data.GetStopCounter();
        guardMovement.NumSpot = data.GetNumSpot();
        guardMovement.GuardStop = data.GetGuardStop();
    }

    public override object Save()
    {
        return new NormalGuardSaveData(guardMovement);
    }
}
