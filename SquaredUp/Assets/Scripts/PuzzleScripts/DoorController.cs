using UnityEngine;
using System.Collections;

public class DoorController : Interactable
{

    //variables
    [HideInInspector] [SerializeField] private GameObject targetDoor = null;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject interactableObject;
    private float distance = 100f;

    //Start Method
    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        //This is a coroutine
        yield return new WaitForEndOfFrame();    //Wait a small amount

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
        interactableObject.transform.Rotate(0, 0, 90);
        UpdateTarget();
    }

    /// <summary>Updates the door controller's target and hides it. Shows the previous target again if this was its only door controller.</summary>
    public void UpdateTarget()
    {
        //check if there is a saved target door
        if (targetDoor != null)
        {
            //call the out of sight for the target door
            targetDoor.GetComponent<DoorState>().OutOfSight();
        }

        //raycast out a distance of 100f and only on the layerMask
        RaycastHit hit;
        if (Physics.Raycast(interactableObject.transform.position, interactableObject.transform.TransformDirection(Vector3.up), out hit, distance, layerMask))
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
        if (targetDoor != null)
        {
            //call within sight on the target door
            targetDoor.GetComponent<DoorState>().WithinSight();
        }
    }
}
