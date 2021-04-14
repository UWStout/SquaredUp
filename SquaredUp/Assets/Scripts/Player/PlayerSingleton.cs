using UnityEngine;

/// <summary>Singleton to enforce one player in the scene.</summary>
public class PlayerSingleton : MonoBehaviour
{
    // Singleton
    private static PlayerSingleton instance;
    public static PlayerSingleton Instance { get { return instance; } }

    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Cannot have multiple Players");
            Destroy(this.gameObject);
        }
    }
}
