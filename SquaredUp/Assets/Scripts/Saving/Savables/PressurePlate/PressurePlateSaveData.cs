
/// <summary>
/// Save data for select data in the pressure plate script in order to avoid the noise playing on saving.
/// </summary>
[System.Serializable]
public class PressurePlateSaveData
{
    // The amount of things the pressure plate had holding it down.
    private int amountIn = 0;


    /// <summary>
    /// Creates save data a pressure plate.
    /// </summary>
    /// <param name="amountThingsIn">Amount of things the pressure plate had.</param>
    public PressurePlateSaveData(int amountThingsIn)
    {
        amountIn = amountThingsIn;
    }


    /// <summary>
    /// Gets the amount of things that the saved pressure plate had on it.
    /// </summary>
    /// <returns>Amount of things that were on the pressure plate.</returns>
    public int GetAmountIn()
    {
        return amountIn;
    }
}
