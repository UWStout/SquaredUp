using UnityEngine;

/// <summary>
/// Saves and loads some data in the LoreTextPickup script to keep track of which have been tracked already.
/// </summary>
[RequireComponent(typeof(LoreTextPickup))]
public class LorePickupSavable : SavableMonoBehav<LorePickupSavable>
{
    // Lore pickup whose data to save
    private LoreTextPickup lorePickupRef = null;


    // Called 0th
    // Set references
    protected override void Awake()
    {
        base.Awake();
        lorePickupRef = this.GetComponent<LoreTextPickup>();
    }


    /// <summary>
    /// Load the lore pickup's save data from the serialized object and
    /// reapply the loaded data to the active lore pickup.
    /// </summary>
    /// <param name="serializedObj">object with the lore pickup's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        LorePickupSaveData data = serializedObj as LorePickupSaveData;

        // Load the data into the lore pickup
        lorePickupRef.LoadSave(data);
    }

    /// <summary>
    /// Creates and returns LorePickupSaveData holding select information about the lore pickup.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Create and return the lore pickup save data
        return new LorePickupSaveData(lorePickupRef);
    }
}
