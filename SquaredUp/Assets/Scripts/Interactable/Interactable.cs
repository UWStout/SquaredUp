using UnityEngine;

/// <summary>Base class for an interactable</summary>
public abstract class Interactable : MonoBehaviour
{
    // Reference to the alert for the npc
    [SerializeField] private GameObject alertObj = null;

    /// <summary>
    /// Called when the player interacts with the Interactable.
    /// </summary>
    public abstract void Interact();

    /// <summary>
    /// Called when the player focuses on the Interactable.
    /// </summary>
    public void DisplayAlert()
    {
        alertObj.SetActive(true);
    }

    /// <summary>
    /// Called when the player stops focusing on the interactable.
    /// </summary>
    public void HideAlert()
    {
        alertObj.SetActive(false);
    }
}
