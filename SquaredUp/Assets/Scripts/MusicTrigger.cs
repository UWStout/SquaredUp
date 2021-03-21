using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    // References for music tracks
    public AudioSource entering; // The track for the zone the player is entering
    public AudioSource exiting; // The track for the zone the player is exiting
    private bool isIn = false; // Boolean for checking if the player is in the trigger
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize other as the Player object for detecting collision
        other = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player is still inside the trigger, if not set isIn to true, stop exiting music track, play entering music track
        if (!isIn)
        {
            isIn = true;
            exiting.Stop();
            // Check that the music for the next zone isn't already playing (avoids restarting track when returning through trigger)
            if (!entering.isPlaying)
            {
                entering.Play();
            }
            // Debug.Log("entering trigger");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Set isIn to false so the trigger will reset
        isIn = false;
        // Debug.Log("leaving trigger");
        
    }

}
