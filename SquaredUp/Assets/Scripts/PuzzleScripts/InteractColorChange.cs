using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractColorChange : Interactable
{
    [SerializeField] private int color;
    //override interactable
    public override void Interact()
    {
        // Change color to specified color
        ChangeColorSkill.Instance.Use(color);
    }

    //set the object that you want to change the color of
    public void SetColorChangeObject(GameObject g)
    {
    }
}
