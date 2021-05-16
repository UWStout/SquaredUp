using UnityEngine;

/// <summary>
/// Saves and loads the player's rotating visuals (the player's shape and color renderer).
/// </summary>
public class PlayerRotVisualsSavable : SavableMonoBehav<PlayerRotVisualsSavable>
{
    // Reference to the blend transitioner that affects the blend shapes
    [SerializeField] private BlendTransitioner blendTransitioner = null;
    // Reference to the Renderer with the current player material
    [SerializeField] private SkinnedMeshRenderer meshRenderer = null;


    /// <summary>
    /// Load the skill data from the serialized object and
    /// reapply the loaded data to the active player's renderer.
    /// </summary>
    /// <param name="serializedObj">object with the player's visual's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        PlayerRotVisualsSaveData data = serializedObj as PlayerRotVisualsSaveData;

        // Change the shape of the player
        ShapeData.ShapeType shapeType = data.GetShapeType();
        blendTransitioner.ChangeShapeInstant(shapeType);
        // Change the color of the player
        Color color = data.GetColor();
        meshRenderer.material = new Material(meshRenderer.material);
        meshRenderer.material.color = color;
    }

    /// <summary>
    /// Creates and returns PlayerRotVisualsSaveData holding the current shape/color renderer's data.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Type of shape the player is right now
        ShapeData.ShapeType shapeType = blendTransitioner.CurrentShapeType;
        // Color the player is right now
        Color color = meshRenderer.material.color;

        // Return the data
        return new PlayerRotVisualsSaveData(shapeType, color);
    }
}
