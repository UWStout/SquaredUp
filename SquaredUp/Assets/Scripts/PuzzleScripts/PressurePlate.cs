using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Obstacle being controlled by pressure plate
    [SerializeField] private GameObject wall = null;
    // Amount of objects in the pressure plate's trigger
    private int amountIn = 0;


    // Called when this object's trigger collides with another physics object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Increment how many objects are in the trigger and hide the wall
        ++amountIn;
        wall.SetActive(false);
    }
    // Called when this object's trigger stops colliding with another physics object
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Decreemnt how many objects are in the trigger and show the wall if all objects have left
        --amountIn;
        if (amountIn <= 0)
        {
            wall.SetActive(true);
            amountIn = 0;
        }
    }
}
