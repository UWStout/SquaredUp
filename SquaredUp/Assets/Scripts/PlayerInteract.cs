using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // The interactable the player is currently focused on.
    private Interactable currentInteract;

    // Start is called before the first frame update
    void Start()
    {
        currentInteract = null;
    }

    // Called when an object enters the trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (currentInteract == null)
        {
            currentInteract = other.GetComponent<Interactable>();
        }
        else
        {
            float distToCur = (currentInteract.transform.position - this.transform.position).magnitude;
            float distToNew = (other.transform.position - this.transform.position).magnitude;
            if (distToNew > distToCur)
            {
                currentInteract = other.GetComponent<Interactable>();
            }
        }
    }

    // Called when an object leaves the trigger.
    private void OnTriggerExit(Collider other)
    {
        Interactable leaveInteract = other.GetComponent<Interactable>();
        if (currentInteract == leaveInteract)
        {
            currentInteract = null;
        }
    }

    // Called when an object stays in the trigger.
    private void OnTriggerStay(Collider other)
    {
        if (currentInteract == null)
        {
            currentInteract = other.GetComponent<Interactable>();
        } else
        {
            float distToCur = (currentInteract.transform.position - this.transform.position).magnitude;
            float distToNew = (other.transform.position - this.transform.position).magnitude;
            if (distToNew > distToCur)
            {
                currentInteract = other.GetComponent<Interactable>();
            }
        }
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
