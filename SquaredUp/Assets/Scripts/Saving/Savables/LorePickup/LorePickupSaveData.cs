
/// <summary>
/// Save data for the LoreTextPickup script.
/// </summary>
[System.Serializable]
public class LorePickupSaveData
{
    // If the lore pickup has been collected yet
    private bool wasCollected = false;


    /// <summary>
    /// Create save data for the lore pickup.
    /// </summary>
    /// <param name="lorePickup">Lore pickup to save data for.</param>
    public LorePickupSaveData(LoreTextPickup lorePickup)
    {
        wasCollected = lorePickup.GetCollected();
    }


    /// <summary>
    /// Get if the saved lore pickup had been collected.
    /// </summary>
    /// <returns>If the saved lore pickup had been collected.</returns>
    public bool GetWasCollected()
    {
        return wasCollected;
    }
}
