using UnityEngine;

public class LoopGuardSavable : SavableMonoBehav<LoopGuardSavable>
{
    [SerializeField] private NPC_MovementLoop guardMovement = null;

    public override void Load(object serializedObj)
    {
        // Cast data
        LoopGuardSaveData data = serializedObj as LoopGuardSaveData;

        // Load the variables in that had changed
        guardMovement.isWalking = data.GetWasWalking();
        guardMovement.WalkingCounter = data.GetWalkingCounter();
        guardMovement.StopCounter = data.GetStopCounter();
        guardMovement.NumSpot = data.GetNumSpot();
        guardMovement.GuardStop = data.GetGuardStop();
    }

    public override object Save()
    {
        return new LoopGuardSaveData(guardMovement);
    }
}
