using UnityEngine;

/// <summary>
/// Saves and loads data for the normal guard NPC_Movement script.
/// </summary>
public class NormalGuardSavable : SavableMonoBehav<NormalGuardSavable>
{
    // Normal guard movement script to save data for
    [SerializeField] private NPC_Movement guardMovement = null;

    /// <summary>
    /// Load the normal guard's data from the serialized object and
    /// reapply the loaded data to the active normal guard's NPC_Movement script.
    /// </summary>
    /// <param name="serializedObj">object with the normal guard's saved data</param>
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
    /// Creates and returns the normal guard's save data.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Create and return the data
        return new GuardSaveData(guardMovement);
    }
}
