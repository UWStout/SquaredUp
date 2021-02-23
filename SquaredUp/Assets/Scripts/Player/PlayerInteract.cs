using UnityEngine;

// Does physics checks to see if the player is interacting with anything.
public class PlayerInteract : MonoBehaviour
{
    // The interactable the player is currently focused on.
    private Interactable currentInteract;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.InteractEvent += OnInteract;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.InteractEvent -= OnInteract;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentInteract = null;
    }

    // Called when an object enters the trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        SwapInteractable(other.GetComponent<Interactable>());
    }

    // Called when an object leaves the trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable leaveInteract = other.GetComponent<Interactable>();
        if (currentInteract == leaveInteract)
        {
            currentInteract.HideAlert();
            currentInteract = null;
        }
    }

    // Called when an object stays in the trigger.
    private void OnTriggerStay2D(Collider2D other)
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
    private void OnInteract()
    {
        if (currentInteract != null)
        {
            currentInteract.Interact();
        }
    }
}
