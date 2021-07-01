using UnityEngine;

public class InteractColorChange : Interactable
{
    // Color to change to
    [SerializeField] private ColorEnum color = ColorEnum.Green;


    /// <summary>
    /// Overrides interact to change the player's color
    /// </summary>
    public override void Interact()
    {
        // Change color to specified color
        ChangeColorSkill.Instance.Use((int)color);
    }

    //set the object that you want to change the color of
    public void SetColorChangeObject(GameObject g)
    {
    }
}
