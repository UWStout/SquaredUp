using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractColorChange : Interactable
{
    private GameObject colorChangeObject;

    public override void Interact()
    {
        colorChangeObject.GetComponent<ChangeColorSkill>().Use(3);
    }

    public void SetColorChangeObject(GameObject g)
    {
        colorChangeObject = g;
    }
}
