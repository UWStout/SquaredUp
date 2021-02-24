using UnityEngine;

// Base class for an interactable
public abstract class Interactable : MonoBehaviour
{
    /// <summary>
    /// Called when the player interacts with the Interactable.
    /// </summary>
    public abstract void Interact();

    /// <summary>
    /// Called when the player focuses on the Interactable.
    /// </summary>
    public abstract void DisplayAlert();

    /// <summary>
    /// Called when the player stops focusing on the interactable.
    /// </summary>
    public abstract void HideAlert();
}
