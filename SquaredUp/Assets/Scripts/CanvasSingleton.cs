using UnityEngine;
using UnityEngine.UI;

public class CanvasSingleton : MonoBehaviour
{
    // Singleton
    private static CanvasSingleton instance;
    public static CanvasSingleton Instance { get { return instance; } }

    // Completely black covering the canvas
    [SerializeField] private Image blackImage = null;
    public Image BlackImage { get { return blackImage; } }


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
            Debug.LogError("Cannot have multiple CanvasSingletons");
            Destroy(this.gameObject);
        }
    }
}
