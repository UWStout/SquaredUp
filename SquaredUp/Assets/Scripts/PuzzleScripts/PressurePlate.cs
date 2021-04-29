using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Obstacle being controlled by pressure plate
    [SerializeField] private GameObject wall = null;
    // AudioSource to play the click sound for pressing down on the pressure plate
    [SerializeField] private AudioSource audioSource = null;
    // Reference to the pressure plate sprite renderer
    [SerializeField] private SpriteRenderer pressurePlateSpriteRend = null;
    // Color to change the pressure plate when it is triggered
    [SerializeField] private Color pressedColor = Color.white;
    // Amount of objects in the pressure plate's trigger
    private int amountIn = 0;
    // Starting color of the pressure plate
    private Color defaultColor = Color.white;


    // Called 1st
    // Initialization
    private void Start()
    {
        defaultColor = pressurePlateSpriteRend.color;
    }
    // Called when this object's trigger collides with another physics object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Increment how many objects are in the trigger and hide the wall
        ++amountIn;
        if (amountIn == 1)
        {
            wall.SetActive(false);
            pressurePlateSpriteRend.color = pressedColor;
            audioSource.Play();
        }
    }
    // Called when this object's trigger stops colliding with another physics object
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Decreemnt how many objects are in the trigger and show the wall if all objects have left
        --amountIn;
        if (amountIn <= 0)
        {
            wall.SetActive(true);
            pressurePlateSpriteRend.color = defaultColor;
            amountIn = 0;
        }
    }
}
