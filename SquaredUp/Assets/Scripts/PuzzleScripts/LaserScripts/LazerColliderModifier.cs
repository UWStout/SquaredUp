using UnityEngine;

public abstract class LazerColliderModifier : MonoBehaviour
{
    public abstract void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection);
    public abstract void LazerRemoved(Lazer lazer);
}
