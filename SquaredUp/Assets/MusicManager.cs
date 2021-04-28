using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Audio Sources that hold music tracks
    [SerializeField] private AudioSource jungle1;
    [SerializeField] private AudioSource jungle2;
    [SerializeField] private AudioSource village;
    [SerializeField] private AudioSource temple;
    [SerializeField] private AudioSource desert;
    [SerializeField] private AudioSource stealth;
    [SerializeField] private AudioSource castle;
    // Enum and variable to hold what level section the player is in.
    enum Sector { jungle1, jungle2, village, temple, desert, castle_start, castle_end, out_of_bounds };
    private Sector current_location; // Var holding current level Sector
    // Rigidbody2D that holds player's location
    [SerializeField] private Rigidbody2D player;
    // Vector2 that holds the last position the player was at
    private Vector2 last_position;
    // Boolean holding status of corroutine (active or inactive)
    private bool PMCorotuineIsActive;
    // Start is called before the first frame update
    void Start()
    {
        current_location = Sector.out_of_bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PMCorotuineIsActive)
        {
            StartCoroutine(PlayMusic(current_location));
        }
    }

    // Function that updates the current_location enum based on the x and y coordinates of the players Rigidbody2D
    void CheckForLocation()
    {
        //Debug.Log("X: " + (player.position.x - 25) + "y: " + (player.position.y - 150));
        //Spawn is 25, 151
        //Debug.Log(player.position);
        if (player.position.x - 25 > -60 && player.position.x - 25 < 40 && player.position.y - 150 > -5 && player.position.y - 150 < 80)
        {
            current_location = Sector.jungle1;
            //Debug.Log("Jungle1");
        }
        else if (player.position.x - 25 > -90 && player.position.x - 25 < 148 && player.position.y - 150 > 80 && player.position.y - 150 < 160)
        {
            current_location = Sector.village;
            //Debug.Log("Village");
        }
        else if (player.position.x - 25 > -180 && player.position.x - 25 < -90 && player.position.y - 150 > 4 && player.position.y - 150 < 132
           || player.position.x - 25 > -275 && player.position.x - 25 < -180 && player.position.y - 150 > 82 && player.position.y - 150 < 132)
        {
            current_location = Sector.jungle2;
            //Debug.Log("Jungle2");
        }
        else if (player.position.x - 25 > -332 && player.position.x - 25 < -180 && player.position.y - 150 > -1 && player.position.y - 150 < 82
            || player.position.x - 25 > -332 && player.position.x - 25 < -200 && player.position.y - 150 > 82 && player.position.y - 150 < 132)
        {
            current_location = Sector.temple;
            //Debug.Log("Temple");
        }
        //Level 2 was moved to the left (x-17)
        else if (player.position.x - 25 > 148 -17 && player.position.x - 25 < 363 -17 && player.position.y - 150 > 114 && player.position.y - 150 < 167)
        {
            current_location = Sector.desert;
            //Debug.Log("Desert");
        }
        else if (player.position.x - 25 > 148 && player.position.x - 25 < 463 - 17 && player.position.y - 150 > 167 && player.position.y - 150 < 375)
        {
            current_location = Sector.castle_start;
            //Debug.Log("Castle Start");
        }
        else if (player.position.x - 25 > 463 - 17 && player.position.x - 25 < 568 -17 && player.position.y - 150 > 200 && player.position.y - 150 < 450
            || player.position.x - 25 > 200-17 && player.position.x - 25 < 463 -17 && player.position.y - 150 > 417 && player.position.y - 150 < 450)
        {
            current_location = Sector.castle_end;
            //Debug.Log("Castle End");
        }
        else
        {
            current_location = Sector.out_of_bounds;
            //Debug.Log("No Sector Detected.");
        }

    }

    // Function that cheks the current_location enum, determines if the appropriate music is playing and that the player has moved to a new location,
    // and proceeds to update last_location to current_location, play the appropriate AudioSource, and stop all other AudioSources
    IEnumerator PlayMusic(Sector current_location)
    {
        PMCorotuineIsActive = true;
        CheckForLocation();
        switch (current_location)
        {
            case Sector.jungle1:
                if (!jungle1.isPlaying)
                {
                    jungle1.Play();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    castle.Stop();
                    yield return null;
                }

                break;
            case Sector.village:
                if (!village.isPlaying)
                {
                    village.Play();
                    jungle1.Stop(); 
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    castle.Stop();
                    yield return null;
                }
                break;
            case Sector.jungle2:
                if (!jungle2.isPlaying)
                {
                    jungle2.Play();
                    jungle1.Stop();
                    village.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    castle.Stop();
                    yield return null;
                }
                break;
            case Sector.temple:
                if (!temple.isPlaying)
                {
                    temple.Play();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    desert.Stop();
                    stealth.Stop();
                    castle.Stop();
                    yield return null;
                }
                break;
            case Sector.desert:
                if (!desert.isPlaying)
                {
                    desert.Play();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    stealth.Stop();
                    castle.Stop();
                    yield return null;
                }
                break;
            case Sector.castle_start:
                if (!stealth.isPlaying)
                {
                    stealth.Play();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    castle.Stop();
                    yield return null;
                }
                break;
            case Sector.castle_end:
                if (!castle.isPlaying)
                {
                    castle.Play();
                    jungle1.Stop(); 
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    yield return null;
                }
                break;
            case Sector.out_of_bounds:
                //Debug.Log("Out of bounds, no music plays");
                jungle1.Stop();
                village.Stop();
                jungle1.Stop();
                temple.Stop();
                desert.Stop();
                stealth.Stop();
                yield return null;
                break;
        }
        PMCorotuineIsActive = false;
    }
}
