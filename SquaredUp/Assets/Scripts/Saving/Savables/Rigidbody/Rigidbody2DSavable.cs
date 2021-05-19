using UnityEngine;

/// <summary>
/// Saves and loads some data for a RigidBody2D.
/// </summary>
public class Rigidbody2DSavable : SavableMonoBehav<Rigidbody2DSavable>
{
    // RigidBody2D to save and load data for.
    [SerializeField] private Rigidbody2D rb = null;


    /// <summary>
    /// Load the rigidbody2D save data from the serialized object and
    /// reapply the loaded data to the active rigidbody2D.
    /// </summary>
    /// <param name="serializedObj">object with the rigidbody's saved data</param>
    public override void Load(object serializedObj)
    {
        // Cast the data
        Rigidbody2DSaveData data = serializedObj as Rigidbody2DSaveData;

        // Velocity and constraints
        rb.velocity = data.GetVelocity();
        rb.constraints = data.GetConstraints();
    }

    /// <summary>
    /// Creates and returns RigidBody2DSaveData holding select information about the rigidbody.
    /// </summary>
    /// <returns></returns>
    public override object Save()
    {
        return new Rigidbody2DSaveData(rb);
    }
}
