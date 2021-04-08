using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerInitialization : MonoBehaviour
{
    // Transform of the physics parent of the door controller.
    [SerializeField] private Transform physicsTransform = null;

    // Start is called before the first frame update
    private void Start()
    {
        // Transfer the rotation from this object to the physics transform.
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        physicsTransform.rotation = rotation;
    }
}
