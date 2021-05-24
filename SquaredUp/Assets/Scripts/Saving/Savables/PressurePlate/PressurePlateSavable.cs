using UnityEngine;

/// <summary>
/// Saves and loads some data in the pressure plate script in order to avoid the noise playing on saving.
/// </summary>
public class PressurePlateSavable : SavableMonoBehav<PressurePlateSavable>
{
    // Pressure plate whose data to save.
    [SerializeField] private PressurePlate pressurePlate = null;


    /// <summary>
    /// Load the pressure plate's save data from the serialized object and
    /// reapply the loaded data to the active pressure plate.
    /// </summary>
    /// <param name="serializedObj">object with the pressure plate's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        PressurePlateSaveData data = serializedObj as PressurePlateSaveData;
        
        // Load the saved amount back in
        pressurePlate.SetPreviousAmountIn(data.GetAmountIn());
    }


    /// <summary>
    /// Creates and returns PressurePlateSaveData holding select information about the pressure plate.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        return new PressurePlateSaveData(pressurePlate.GetAmountIn());
    }
}
