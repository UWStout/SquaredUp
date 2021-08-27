using UnityEngine;

/// <summary>
/// Saves and loads data for the loop guard NPC_MovementLoop script.
/// </summary>
public class LoopGuardSavable : SavableMonoBehav<LoopGuardSavable>
{
    // Loop guard movement script to save data for
    [SerializeField] private NPC_MovementLoop guardMovement = null;

    /// <summary>
    /// Load the loop guard's data from the serialized object and
    /// reapply the loaded data to the active loop guard's NPC_MovementLoop script.
    /// </summary>
    /// <param name="serializedObj">object with the loop guard's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast data
        GuardSaveData data = serializedObj as GuardSaveData;

        // Load the variables in that had changed
        guardMovement.isWalking = data.GetWasWalking();
        guardMovement.WalkingCounter = data.GetWalkingCounter();
        guardMovement.StopCounter = data.GetStopCounter();
        guardMovement.NumSpot = data.GetNumSpot();
        guardMovement.GuardStop = data.GetGuardStop();
    }

    /// <summary>
    /// Creates and returns the loop guard's save data.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Create and return the data
        return new GuardSaveData(guardMovement);
    }
}
