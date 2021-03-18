using UnityEngine;

public class DoorController : Interactable
{
    private GameObject targetDoor;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject interactableObject;

    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(interactableObject.transform.position, interactableObject.transform.TransformDirection(Vector3.up), out hit,100f, layerMask))
        {
            targetDoor = hit.collider.gameObject;
            Debug.Log(targetDoor);
        }
        if (targetDoor != null)
        {
            targetDoor.GetComponent<DoorState>().WithinSight();
        }
    }

    public override void Interact()
    {
        if (targetDoor!=null)
        {
            targetDoor.GetComponent<DoorState>().OutOfSight();
        }
        interactableObject.transform.Rotate(0, 0, 90);
        RaycastHit hit;
        if(Physics.Raycast(interactableObject.transform.position,interactableObject.transform.TransformDirection(Vector3.up),out hit,100f, layerMask))
        {
            targetDoor = hit.collider.gameObject;
            Debug.Log(targetDoor);
        }
        else
        {
            targetDoor = null;
        }
        if (targetDoor!=null)
        {
            targetDoor.GetComponent<DoorState>().WithinSight();
        }
    }
}
