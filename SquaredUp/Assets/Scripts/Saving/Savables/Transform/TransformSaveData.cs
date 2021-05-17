using UnityEngine;

/// <summary>
/// Save data for a transform.
/// </summary>
[System.Serializable]
public class TransformSaveData
{
    // Position vector
    private float[] pos = new float[3];
    // Scale vector
    private float[] scale = new float[3];
    // Rotation as a 4D vector
    private float[] rot = new float[4];

    /// <summary>
    /// Create the save data for the transform.
    /// </summary>
    /// <param name="trans">Transform to create data for.</param>
    public TransformSaveData(Transform trans)
    {
        // Position
        pos[0] = trans.localPosition.x;
        pos[1] = trans.localPosition.y;
        pos[2] = trans.localPosition.z;
        // Scale
        scale[0] = trans.localScale.x;
        scale[1] = trans.localScale.y;
        scale[2] = trans.localScale.z;
        // Rotation
        rot[0] = trans.localRotation.x;
        rot[1] = trans.localRotation.y;
        rot[2] = trans.localRotation.z;
        rot[3] = trans.localRotation.w;
    }


    /// <summary>
    /// Gets the transforms's position saved in the data.
    /// </summary>
    /// <returns>Saved position.</returns>
    public Vector3 GetLocalPosition()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }
    /// <summary>
    /// Gets the transform's localScale saved in the data.
    /// </summary>
    /// <returns>Saved localScale.</returns>
    public Vector3 GetLocalScale()
    {
        return new Vector3(scale[0], scale[1], scale[2]);
    }
    /// <summary>
    /// Gets the transform's rotation saved in the data.
    /// </summary>
    /// <returns>Saved rotation.</returns>
    public Quaternion GetLocalRotation()
    {
        return new Quaternion(rot[0], rot[1], rot[2], rot[3]);
    }
}
