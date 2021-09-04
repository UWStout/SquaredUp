using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Audio Sources that hold music tracks
    // Level 1
    [SerializeField] private AudioSource jungle1;
    [SerializeField] private AudioSource jungle2;
    [SerializeField] private AudioSource village;
    // Level 2
    [SerializeField] private AudioSource temple;
    [SerializeField] private AudioSource desert;
    [SerializeField] private AudioSource stealth;
    [SerializeField] private AudioSource castle;
    // Level 3
    [SerializeField] private AudioSource mountainEntrance;
    [SerializeField] private AudioSource mountainPath;
    [SerializeField] private AudioSource mountainVillage;
    [SerializeField] private AudioSource mountainPass;

    // Enum and variable to hold what level section the player is in.
    enum Sector { jungle1, jungle2, village, temple, desert, castle_start, castle_end, 
        mountainEntrance, mountainPath, mountainVillage, mountainPass, out_of_bounds };
    private Sector current_location; // Var holding current level Sector
    // Rigidbody2D that holds player's location
    [SerializeField] private Rigidbody2D player;
    // Vector2 that holds the last position the player was at
    private Vector2 last_position;
    // Boolean holding status of corroutine (active or inactive)
    private bool PMCorotuineIsActive;

    // Singleton
    private static MusicManager instance = null;
    public static MusicManager Instance { get { return instance; } }


    // Called 0th
    // Set references
    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Cannot have multiple MusicManagers");
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
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

    private const int XSPAWN = 25; // Where the player initial x is
    private const int YSPAWN = 151; // Where the player initial y is
    private const int XLVL2 = 17; // How far Level 2 is offset in the x direction
    // Function that updates the current_location enum based on the x and y coordinates of the players Rigidbody2D
    void CheckForLocation()
    {
        //Debug.Log("X: " + (player.position.x - 25) + "y: " + (player.position.y - YSPAWN));
        //Spawn is XSPAWN, 151
        //Debug.Log(player.position);
        //Debug.Log(current_location);
        if (player.position.x - XSPAWN > -60 && player.position.x - XSPAWN < 40 && player.position.y - YSPAWN > -5 && player.position.y - YSPAWN < 80)
        {
            current_location = Sector.jungle1;
            //Debug.Log("Jungle1");
        }
        else if (player.position.x - XSPAWN > -90 && player.position.x - XSPAWN < 148 && player.position.y - YSPAWN > 80 && player.position.y - YSPAWN < 160)
        {
            current_location = Sector.village;
            //Debug.Log("Village");
        }
        else if (player.position.x - XSPAWN > -180 && player.position.x - XSPAWN < -90 && player.position.y - YSPAWN > 4 && player.position.y - YSPAWN < 132
           || player.position.x - XSPAWN > -275 && player.position.x - XSPAWN < -180 && player.position.y - YSPAWN > 82 && player.position.y - YSPAWN < 132)
        {
            current_location = Sector.jungle2;
            //Debug.Log("Jungle2");
        }
        else if (player.position.x - XSPAWN > -332 && player.position.x - XSPAWN < -180 && player.position.y - YSPAWN > -1 && player.position.y - YSPAWN < 82
            || player.position.x - XSPAWN > -332 && player.position.x - XSPAWN < -200 && player.position.y - YSPAWN > 82 && player.position.y - YSPAWN < 132)
        {
            current_location = Sector.temple;
            //Debug.Log("Temple");
        }
        //Level 2 was moved to the left (x-17)
        else if (player.position.x - XSPAWN > 148 -17 && player.position.x - XSPAWN < 363 -17 && player.position.y - YSPAWN > 114 && player.position.y - YSPAWN < 167)
        {
            current_location = Sector.desert;
            //Debug.Log("Desert");
        }
        else if (player.position.x - XSPAWN > 148 && player.position.x - XSPAWN < 463 -XLVL2 && player.position.y - YSPAWN > 167 && player.position.y - YSPAWN < 375)
        {
            current_location = Sector.castle_start;
            //Debug.Log("Castle Start");
        }
        else if (player.position.x - XSPAWN > 463 -XLVL2 && player.position.x - XSPAWN < 568 -17 && player.position.y - YSPAWN > 200 && player.position.y - YSPAWN < 450
            || player.position.x - XSPAWN > 200-17 && player.position.x - XSPAWN < 463 -17 && player.position.y - YSPAWN > 417 && player.position.y - YSPAWN < 450)
        {
            current_location = Sector.castle_end;
            //Debug.Log("Castle End");
        } 
        else if (player.position.x - XSPAWN > 252 -XLVL2 && player.position.x - XSPAWN < 369 -XLVL2 && player.position.y - YSPAWN > -97 && player.position.y - YSPAWN < 114)
        {
            current_location = Sector.mountainEntrance;
        }
        else if (player.position.x - XSPAWN > 369 -XLVL2 && player.position.x - XSPAWN < 447 -XLVL2 && player.position.y - YSPAWN > -100 && player.position.y - YSPAWN < 9
            || player.position.x - XSPAWN > 447 -XLVL2 && player.position.x - XSPAWN < 529 -XLVL2 && player.position.y - YSPAWN > -68 && player.position.y - YSPAWN < 9)
        {
            current_location = Sector.mountainPath;
        }
        else if (player.position.x - XSPAWN > 465 -XLVL2 && player.position.x - XSPAWN < 648 -XLVL2 && player.position.y - YSPAWN > -251 && player.position.y - YSPAWN < -68)
        {
            current_location = Sector.mountainVillage;
        }
        else if (player.position.x - XSPAWN > 648 -XLVL2 && player.position.x - XSPAWN < 853 -XLVL2 && player.position.y - YSPAWN > -212 && player.position.y - YSPAWN < 112)
        {
            current_location = Sector.mountainPass;
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
                    yield return null;
                }
                break;
            case Sector.mountainEntrance:
                if (!mountainEntrance.isPlaying)
                {
                    castle.Stop();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    mountainEntrance.Play();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Stop();
                    yield return null;
                }
                break;
            case Sector.mountainPath:
                if (!mountainPath.isPlaying)
                {
                    castle.Stop();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Play();
                    mountainVillage.Stop();
                    yield return null;
                }
                break;
            case Sector.mountainVillage:
                if (!mountainVillage.isPlaying)
                {
                    castle.Stop();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    mountainEntrance.Stop();
                    mountainPass.Stop();
                    mountainPath.Stop();
                    mountainVillage.Play();
                    yield return null;
                }
                break;
            case Sector.mountainPass:
                if (!mountainPass.isPlaying)
                {
                    castle.Stop();
                    jungle1.Stop();
                    village.Stop();
                    jungle2.Stop();
                    temple.Stop();
                    desert.Stop();
                    stealth.Stop();
                    mountainEntrance.Stop();
                    mountainPass.Play();
                    mountainPath.Stop();
                    mountainVillage.Stop();
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
                mountainEntrance.Stop();
                mountainPass.Stop();
                mountainPath.Stop();
                mountainVillage.Stop();
                yield return null;
                break;
        }
        PMCorotuineIsActive = false;
    }
}
