using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMirrorInteract : Interactable
{
    [SerializeField]
    GameObject rotateObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        rotateObject.transform.Rotate(0f, 0f, 90f);
    }
}
