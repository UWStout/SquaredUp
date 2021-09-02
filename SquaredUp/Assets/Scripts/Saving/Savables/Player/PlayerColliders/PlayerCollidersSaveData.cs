
/// <summary>
/// Save data for the player's collider controller.
/// </summary>
[System.Serializable]
public class PlayerCollidersSaveData
{
    // Type of collider that was active
    private ShapeData.ShapeType colliderType = ShapeData.ShapeType.BOX;


    /// <summary>
    /// Creates save data for the player's collider controller.
    /// </summary>
    /// <param name="shapeType">Type of collider that is currently active.</param>
    public PlayerCollidersSaveData(ShapeData.ShapeType shapeType)
    {
        colliderType = shapeType;
    }

    /// <summary>
    /// Gets the collider type that was saved as active.
    /// </summary>
    /// <returns>ShapeType of the collider that was active.</returns>
    public ShapeData.ShapeType GetColliderType()
    {
        return colliderType;
    }
}
