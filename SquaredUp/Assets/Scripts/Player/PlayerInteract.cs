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
        if (other.TryGetComponent(out Interactable interactable))
        {
            SwapInteractable(interactable);
        }
        else
        {
            Debug.LogWarning($"{other.name} is on the interactable layer, but has no interactable");
        }
    }

    // Called when an object leaves the trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            if (currentInteract == interactable)
            {
                currentInteract.HideAlert();
                currentInteract = null;
            }
        }
        else
        {
            Debug.LogWarning($"{other.name} is on the interactable layer, but has no interactable");
        }
    }

    // Called when an object stays in the trigger.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (currentInteract == null)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                SwapInteractable(interactable);
            }
            else
            {
                Debug.LogWarning($"{other.name} is on the interactable layer, but has no interactable");
            }
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
