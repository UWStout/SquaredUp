
/// <summary>
/// Saves and loads the attached transform's data.
/// </summary>
public class TransformSavable : SavableMonoBehav<TransformSavable>
{
    /// <summary>
    /// Load the transform data from the serialized object and
    /// reapply the loaded data to the active tranform.
    /// </summary>
    /// <param name="serializedObj">object with the transform's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast to the correct data type
        TransformSaveData data = serializedObj as TransformSaveData;

        // Put the transform back to their previous position
        transform.position = data.GetPosition();
        // Rescale the transform
        transform.localScale = data.GetScale();
    }

    /// <summary>
    /// Creates and returns transform save data.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        // Create the data
        TransformSaveData data = new TransformSaveData(transform);
        return data;
    }
}
