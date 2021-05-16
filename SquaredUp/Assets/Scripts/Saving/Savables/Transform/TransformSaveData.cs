using UnityEngine;

/// <summary>
/// Save data for a transform.
/// </summary>
[System.Serializable]
public class TransformSaveData
{
    // Player's position vector
    private float[] pos = new float[3];
    // Player's scale vector
    private float[] scale = new float[3];

    /// <summary>
    /// Create the save data for the transform.
    /// </summary>
    /// <param name="trans">Transform to create data for.</param>
    public TransformSaveData(Transform trans)
    {
        // Position
        pos[0] = trans.position.x;
        pos[1] = trans.position.y;
        pos[2] = trans.position.z;
        // Scale
        scale[0] = trans.localScale.x;
        scale[1] = trans.localScale.y;
        scale[2] = trans.localScale.z;
    }


    /// <summary>
    /// Gets the transforms's position saved in the data.
    /// </summary>
    /// <returns>Saved position.</returns>
    public Vector3 GetPosition()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }
    /// <summary>
    /// Gets the transform's localScale saved in the data.
    /// </summary>
    /// <returns>Saved localScale.</returns>
    public Vector3 GetScale()
    {
        return new Vector3(scale[0], scale[1], scale[2]);
    }
}
