using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // The interactable the player is currently focused on.
    private Interactable currentInteract;

    // Start is called before the first frame update
    private void Start()
    {
        currentInteract = null;
    }

    // Called when an object enters the trigger.
    private void OnTriggerEnter(Collider other)
    {
        SwapInteractable(other.GetComponent<Interactable>());
    }

    // Called when an object leaves the trigger.
    private void OnTriggerExit(Collider other)
    {
        Interactable leaveInteract = other.GetComponent<Interactable>();
        if (currentInteract == leaveInteract)
        {
            currentInteract.HideAlert();
            currentInteract = null;
        }
    }

    // Called when an object stays in the trigger.
    private void OnTriggerStay(Collider other)
    {
        if (currentInteract == null)
        {
            SwapInteractable(other.GetComponent<Interactable>());
        }
    }

    /// <summary>
    /// Swaps the interactable to the new interactable.
    /// Hides the last interactable's alert and shows the new interactable's alert.
    /// </summary>
    /// <param name="newInteractable">Interactable that is the new focus.</param>
    private void SwapInteractable(Interactable newInteractable)
    {
        if (currentInteract != null)
        {
            currentInteract.HideAlert();
        }
        currentInteract = newInteractable;
        newInteractable.DisplayAlert();
    }

    // Called when the player tries to interact with something from the input system.
    public void OnInteract(InputAction.CallbackContext value)
    {
        if (currentInteract != null && value.started)
        {
            currentInteract.Interact();
        }
    }
}
