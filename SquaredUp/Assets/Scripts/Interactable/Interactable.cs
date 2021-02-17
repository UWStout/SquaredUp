using UnityEngine;

// Base class for an interactable
public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Called when the player interacts with the Interactable.
    /// </summary>
    public virtual void Interact() { Debug.Log("Interact with " + this.name); }

    /// <summary>
    /// Called when the player focuses on the Interactable.
    /// </summary>
    public virtual void DisplayAlert() { }

    /// <summary>
    /// Called when the player stops focusing on the interactable.
    /// </summary>
    public virtual void HideAlert() { }
}
