using UnityEngine;

public class DoorController : Interactable
{

    //variables
    private GameObject targetDoor;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject interactableObject;
    private float distance = 100f;

    //Start Method
    void Start()
    {
        //raycast out a distance of 100f and only on the layerMask
        RaycastHit hit;
        if (Physics.Raycast(interactableObject.transform.position, interactableObject.transform.TransformDirection(Vector3.up), out hit,distance, layerMask))
        {
            //get the gameobject of the collider that gets hit by raycast and save it as the target
            targetDoor = hit.collider.gameObject;
        }
        //if there is a target door
        if (targetDoor != null)
        {
            //call within sight on the target door
            targetDoor.GetComponent<DoorState>().WithinSight();
        }
    }

    public override void Interact()
    {
        //check if there is a saved target door
        if (targetDoor!=null)
        {
            //call the out of sight for the target door
            targetDoor.GetComponent<DoorState>().OutOfSight();
        }
        interactableObject.transform.Rotate(0, 0, 90);
        //raycast out a distance of 100f and only on the layerMask
        RaycastHit hit;
        if(Physics.Raycast(interactableObject.transform.position,interactableObject.transform.TransformDirection(Vector3.up),out hit,distance, layerMask))
        {
            //get the gameobject of the collider that gets hit by raycast and save it as the target
            targetDoor = hit.collider.gameObject;
        }
        else
        {
            //set target door to null
            targetDoor = null;
        }
        //if there is a target door
        if (targetDoor!=null)
        {
            //call within sight on the target door
            targetDoor.GetComponent<DoorState>().WithinSight();
        }
    }
}
