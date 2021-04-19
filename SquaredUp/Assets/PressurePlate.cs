using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject wall; // obstacle being controlled by pressure plate
    private bool isIn = false; // Boolean for checking if the player is in the trigger
    public GameObject other;
    // Start is called before

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player is still inside the trigger, if not set isIn to true, stop exiting music track, play entering music track
        if (!isIn)
        {
            wall.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Set isIn to false so the trigger will reset
        isIn = false;
        wall.SetActive(true);
    }
}
