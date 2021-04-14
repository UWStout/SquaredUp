using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractColorChange : Interactable
{
    [SerializeField] private GameObject colorChangeObject;
    [SerializeField] private int color;
    //override interactable
    public override void Interact()
    {
        //change character to blue
        colorChangeObject.GetComponent<ChangeColorSkill>().Use(color);
    }

    //set the object that you want to change the color of
    public void SetColorChangeObject(GameObject g)
    {
        colorChangeObject = g;
    }
}
