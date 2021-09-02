using UnityEngine;

/// <summary>
/// Saves and loads data for the player's collider controller.
/// </summary>
public class PlayerCollidersSavable : SavableMonoBehav<PlayerCollidersSavable>
{
    // Reference to the player's collider controller
    [SerializeField] private PlayerColliderController colliderCont = null;


    /// <summary>
    /// Load the player's colliders data from the serialized object and
    /// reapplies the loaded data to the collider's controller.
    /// </summary>
    /// <param name="serializedObj">object with the collider controller's active collider saved as data</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        PlayerCollidersSaveData data = serializedObj as PlayerCollidersSaveData;

        // Change the collider type to what it was previously
        colliderCont.ChangeColliderType(data.GetColliderType());
    }

    /// <summary>
    /// Creates and returns PlayerCollidersSaveData holding the data for the 
    /// player's collider controller.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Get the current active collider type
        ShapeData.ShapeType colliderType = colliderCont.GetActiveColliderType();

        // Return the new data
        return new PlayerCollidersSaveData(colliderType);
    }
}
