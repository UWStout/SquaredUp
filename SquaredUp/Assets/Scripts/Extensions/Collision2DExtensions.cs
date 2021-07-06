using UnityEngine;

/// <summary>
/// Extension functions for the unity Collision2D class.
/// </summary>
public static class Collision2DExtensions
{
    /// <summary>
    /// Determines what direction the hit came from.
    /// </summary>
    /// <param name="collision">Reference to self for extension.</param>
    /// <returns>Direction2D the hit came from.</returns>
    public static Direction2D GetDirectionHitCameFrom(this Collision2D collision)
    {
        // Determine the center point of the hits
        Vector2 contactsCenter = collision.GetContactsCenterPoint();

        // Get the difference from the contacts center the center of the
        // transform of the object that got hit
        Transform thisTrans = collision.otherCollider.gameObject.transform;
        Vector2 pos2D = new Vector2(thisTrans.position.x, thisTrans.position.y);
        Vector2 rawHitDiff = pos2D - contactsCenter;

        // Get the direction from the difference
        return rawHitDiff.GetDirection2D();
    }
    /// <summary>
    /// Determines the center of all the contact points.
    /// </summary>
    /// <param name="collision">Reference to self for extension.</param>
    /// <returns>Center of the contact points.</returns>
    public static Vector2 GetContactsCenterPoint(this Collision2D collision)
    {
        Vector2 collisionCenter = Vector2.zero;
        foreach (ContactPoint2D collisionPoint in collision.contacts)
        {
            collisionCenter += collisionPoint.point;
        }
        // Avoid dividing by 0
        if (collision.contactCount > 0)
        {
            collisionCenter /= collision.contactCount;
        }
        else
        {
            Debug.LogError("Collision had no contacts");
        }
        return collisionCenter;
    }
}
