using UnityEngine;

/// <summary>
/// Save data for select items in a rigidbody2D.
/// </summary>
[System.Serializable]
public class Rigidbody2DSaveData
{
    // Velocity of the rigidbody2D
    private float[] velocity = new float[2];
    // Constraints for moving the rigidbody2D
    private int constraintEnumVal = 0;


    /// <summary>
    /// Creates save data for the given rigidbody2D.
    /// </summary>
    /// <param name="rb">Rigidbody2D whose data to save.</param>
    public Rigidbody2DSaveData(Rigidbody2D rb)
    {
        // Velocity
        velocity[0] = rb.velocity.x;
        velocity[1] = rb.velocity.y;
        // Costraints
        constraintEnumVal = (int)rb.constraints;
    }

    /// <summary>
    /// Gets the saved velocity of the rigidbody2D.
    /// </summary>
    /// <returns>Vector2 velocity for the rigidbody2D.</returns>
    public Vector2 GetVelocity()
    {
        return new Vector2(velocity[0], velocity[1]);
    }
    /// <summary>
    /// Gets the saved constraints of the rigidbody2D.
    /// </summary>
    /// <returns>RigidbodyConstraints2D that were saved for the rigidbody2D.</returns>
    public RigidbodyConstraints2D GetConstraints()
    {
        return (RigidbodyConstraints2D)constraintEnumVal;
    }
}
